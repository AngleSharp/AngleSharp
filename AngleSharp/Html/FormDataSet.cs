namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Bundles information stored in HTML forms.
    /// </summary>
    sealed class FormDataSet : IEnumerable<String>
    {
        #region Fields

        readonly List<FormDataSetEntry> _entries;
        String _boundary;

        static readonly String[] NewLines = new[] { "\r\n", "\r", "\n" };

        #endregion

        #region ctor

        public FormDataSet()
        {
            _boundary = Guid.NewGuid().ToString();
            _entries = new List<FormDataSetEntry>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the chosen boundary.
        /// </summary>
        public String Boundary
        {
            get { return _boundary; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies the multipart/form-data algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#multipart/form-data-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsMultipart(Encoding encoding = null)
        {
            return Build(encoding, stream =>
            {
                var enc = stream.Encoding;
                var entryWriters = _entries.Select(m => m.AsMultipart(enc)).
                                            Where(m => m != null);

                foreach (var entryWriter in entryWriters)
                {
                    stream.Write("--");
                    stream.WriteLine(_boundary);
                    entryWriter(stream);
                }

                stream.Write("--");
                stream.Write(_boundary);
                stream.Write("--");
            });
        }

        /// <summary>
        /// Applies the urlencoded algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsUrlEncoded(Encoding encoding = null)
        {
            return Build(encoding, stream =>
            {
                var offset = 0;
                var enc = stream.Encoding;

                if (offset < _entries.Count &&
                    _entries[offset].HasName &&
                    _entries[offset].Name.Equals(Tags.IsIndex) &&
                    _entries[offset].Type.Equals(InputTypeNames.Text, StringComparison.OrdinalIgnoreCase))
                {
                    stream.Write(((TextDataSetEntry)_entries[offset]).Value);
                    offset++;
                }

                var list = _entries.Skip(offset).
                                    Select(m => m.AsUrlEncoded(enc)).
                                    Where(m => m != null).
                                    ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    if (i > 0)
                        stream.Write('&');

                    stream.Write(list[i].Item1);
                    stream.Write('=');
                    stream.Write(list[i].Item2);
                }
            });
        }

        /// <summary>
        /// Applies the plain encoding algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#text/plain-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsPlaintext(Encoding encoding = null)
        {
            return Build(encoding, stream =>
            {
                var list = _entries.Select(m => m.AsPlaintext()).
                                    Where(m => m != null).
                                    ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    if (i > 0)
                        stream.Write("\r\n");

                    stream.Write(list[i].Item1);
                    stream.Write('=');
                    stream.Write(list[i].Item2);
                }
            });
        }

        /// <summary>
        /// Applies the application json encoding algorithm.
        /// https://darobin.github.io/formic/specs/json/#the-application-json-encoding-algorithm
        /// </summary>
        /// <returns>A stream containing the body.</returns>
        public Stream AsJson()
        {
            //spec dictates utf8
            return Build(TextEncoding.Utf8, stream =>
            {
                var resultingObject = new JsonObject();

                foreach (var entry in _entries)
                {
                    entry.AsJson(resultingObject);
                }

                resultingObject.WriteTo(stream);
            });
        }

        public void Append(String name, String value, String type)
        {
            if (String.Compare(type, Tags.Textarea, StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);
                value = Normalize(value);
            }

            _entries.Add(new TextDataSetEntry(name, value, type));
        }

        public void Append(String name, IFile value, String type)
        {
            if (String.Compare(type, InputTypeNames.File, StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);
            }

            _entries.Add(new FileDataSetEntry(name, value, type));
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Builds the specific request body / url.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="process">The action to generate the content.</param>
        /// <returns>The constructed stream.</returns>
        Stream Build(Encoding encoding, Action<StreamWriter> process)
        {
            encoding = encoding ?? TextEncoding.Utf8;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);
            var tw = new StreamWriter(ms, encoding);
            process(tw);
            tw.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Replaces a charset field (if any) that is hidden with the given
        /// character encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        void ReplaceCharset(Encoding encoding)
        {
            for (int i = 0; i < _entries.Count; i++ )
            {
                var entry = _entries[i];

                if (!String.IsNullOrEmpty(entry.Name) && entry.Name.Equals("_charset_") &&
                    entry.Type.Equals(InputTypeNames.Hidden, StringComparison.OrdinalIgnoreCase))
                {
                    _entries[i] = new TextDataSetEntry(entry.Name, encoding.WebName, entry.Type);
                }
            }
        }

        /// <summary>
        /// Checks the entries for boundary collisions. If a collision is
        /// detected, then a new boundary string is generated. This algorithm
        /// will produce a boundary string that satisfies all requirements.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        void CheckBoundaries(Encoding encoding)
        {
            var found = false;

            do
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    if (found = _entries[i].Contains(_boundary, encoding))
                    {
                        _boundary = Guid.NewGuid().ToString();
                        break;
                    }
                }
            } while (found);
        }

        /// <summary>
        /// Replaces every occurrence of a "CR" (U+000D) character not followed
        /// by a "LF" (U+000A) character, and every occurrence of a "LF"
        /// (U+000A) character not preceded by a "CR" (U+000D) character, by a
        /// two-character string consisting of a U+000D CARRIAGE RETURN "CRLF"
        /// (U+000A) character pair.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized string.</returns>
        static String Normalize(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var lines = value.Split(NewLines, StringSplitOptions.None);
                return String.Join("\r\n", lines);
            }

            return value;
        }

        #endregion

        #region Entry Class

        /// <summary>
        /// Encapsulates the data contained in an entry.
        /// </summary>
        abstract class FormDataSetEntry
        {
            readonly String _name;
            readonly String _type;

            public FormDataSetEntry(String name, String type)
            {
                _name = name;
                _type = type;
            }

            /// <summary>
            /// Gets if the name has been given.
            /// </summary>
            public Boolean HasName
            {
                get { return _name != null; }
            }

            /// <summary>
            /// Gets the entry's name.
            /// </summary>
            public String Name
            {
                get { return _name ?? String.Empty; }
            }

            /// <summary>
            /// Gets the entry's type.
            /// </summary>
            public String Type
            {
                get { return _type ?? InputTypeNames.Text; }
            }

            public abstract Action<StreamWriter> AsMultipart(Encoding encoding);

            public abstract Tuple<String, String> AsPlaintext();

            public abstract Tuple<String, String> AsUrlEncoded(Encoding encoding);

            public abstract void AsJson(JsonElement context);

            public abstract Boolean Contains(String boundary, Encoding encoding);

            protected static List<Step> ParseJSONPath(string path)
            {
                //1. Let path be the path we are to parse.

                //2. Let original be a copy of path.
                string original = path;

                try
                {
                    //3. Let steps be an empty list of steps.
                    List<Step> steps = new List<Step>();

                    //4. Let first key be the result of collecting a sequence of characters that are not U + 005B LEFT SQUARE BRACKET ("[") from the path.
                    StringBuilder firstKey = new StringBuilder();
                    for (int i = 0; i < path.Length; i++)
                    {
                        var currentChar = path[i];
                        if (currentChar != '[')
                        {
                            firstKey.Append(currentChar);
                        }
                        else
                        {
                            break;
                        }
                    }

                    //5. If first key is empty, jump to the step labelled failure below.
                    if (firstKey.Length == 0)
                    {
                        goto failure;
                    }

                    //6. Otherwise remove the collected characters from path and push a step onto steps with its type set to "object", its key set to the collected characters, and its last flag unset.
                    path = path.Substring(firstKey.Length);
                    Step lastStep = new Step();
                    lastStep.Type = StepType.Object;
                    lastStep.Key = firstKey.ToString();
                    steps.Add(lastStep);

                    //7. If the path is empty, set the last flag on the last step in steps and return steps.
                    if (path.Length == 0)
                    {
                        lastStep.Last = true;
                        return steps;
                    }

                    //8. Loop: While path is not an empty string, run these substeps: 
                    while (path.Length != 0)
                    {
                        //8.4. If this point in the loop is reached, jump to the step labelled failure below. 
                        if (path.Length <= 1 || path[0] != '[')
                        {
                            goto failure;
                        }

                        //8.1. If the first two characters in path are U+005B LEFT SQUARE BRACKET ("[") followed by U+005D RIGHT SQUARE BRACKET ("]"), run these subsubsteps: 
                        if (path[1] == ']')
                        {
                            //8.1.1. Set the append flag on the last step in steps.
                            lastStep.Append = true;

                            //8.1.2. Remove those two characters from path.
                            path = path.Substring(2);

                            //8.1.3. If there are characters left in path, jump to the step labelled failure below.
                            if (path.Length != 0)
                            {
                                goto failure;
                            }

                            //8.1.4. Otherwise jump to the step labelled loop above.
                            continue;
                        }

                        //8.2. If the first character in path is U+005B LEFT SQUARE BRACKET ("["), followed by one or more ASCII digits, followed by U+005D RIGHT SQUARE BRACKET ("]"), run these subsubsteps:
                        if (Char.IsDigit(path[1]))
                        {
                            //8.2.1. Remove the first character from path.
                            path = path.Substring(1);

                            //8.2.2. Collect a sequence of characters being ASCII digits, remove them from path, and let numeric key be the result of interpreting them as a base-ten integer. 
                            StringBuilder numericKey = new StringBuilder();
                            for (int i = 0; i < path.Length; i++)
                            {
                                var currentChar = path[i];
                                if (Char.IsDigit(currentChar))
                                {
                                    numericKey.Append(currentChar);
                                }
                                else if (currentChar == ']')
                                {
                                    break;
                                }
                                else
                                {
                                    goto failure;
                                }
                            }
                            int intKey = Int32.Parse(numericKey.ToString());

                            //8.2.3. Remove the following character from path.
                            path = path.Substring(numericKey.Length + 1);

                            //8.2.4. Push a step onto steps with its type set to "array", its key set to the numeric key, and its last flag unset. 
                            lastStep = new Step();
                            lastStep.Type = StepType.Array;
                            lastStep.Key = intKey;
                            steps.Add(lastStep);

                            //8.2.5. Jump to the step labelled loop above.
                            continue;
                        }

                        //8.3. If the first character in path is U+005B LEFT SQUARE BRACKET ("["), followed by one or more characters that are not U+005D RIGHT SQUARE BRACKET, followed by U+005D RIGHT SQUARE BRACKET ("]"), run these subsubsteps:
                        if (path[1] != ']')
                        {
                            //8.3.1. Remove the first character from path.
                            path = path.Substring(1);

                            //8.3.2. Collect a sequence of characters that are not U+005D RIGHT SQUARE BRACKET, remove them from path, and let object key be the result. 
                            StringBuilder objectKey = new StringBuilder();
                            for (int i = 0; i < path.Length; i++)
                            {
                                var currentChar = path[i];
                                if (currentChar != ']')
                                {
                                    objectKey.Append(currentChar);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            //8.3.3. Remove the following character from path.
                            path = path.Substring(objectKey.Length + 1);

                            //8.3.4. Push a step onto steps with its type set to "object", its key set to the object key, and its last flag unset. 
                            lastStep = new Step();
                            lastStep.Type = StepType.Object;
                            lastStep.Key = objectKey.ToString();
                            steps.Add(lastStep);

                            //8.3.5. Jump to the step labelled loop above.
                            continue;
                        }
                    }

                    //9. For each step in steps, run the following substeps:
                    for (int i = 0; i < steps.Count; i++)
                    {
                        //9.1. If the step is the last step, set its last flag.
                        if (i == steps.Count - 1)
                        {
                            steps[i].Last = true;
                        }
                        //9.2. Otherwise, set its next type to the type of the next step in steps.
                        else
                        {
                            steps[i].NextType = steps[i + 1].Type;
                        }
                    }

                    //10. Return steps.
                    return steps;
                }
                catch { }

                //11. Failure: return a list of steps containing a single step with its type set to "object", its key set to original, and its last flag set.
                failure:
                return new List<Step> { new Step { Key = original, Last = true, Type = StepType.Object } };
            }

            protected static JsonElement JsonEncodeValue(JsonElement context, Step step, JsonElement currentValue, JsonElement entryValue, bool isFile)
            {
                //1. Let context be the context this algorithm is called with.
                //2. Let step be the step of the path this algorithm is called with.
                //3. Let current value be the current value this algorithm is called with.
                //4. Let entry value be the entry value this algorithm is called with.
                //5. Let is file be the is file flag this algorithm is called with.

                //7. If step has its last flag set, run the following substeps:
                if (step.Last)
                {
                    //7.1. If current value is undefined, run the following subsubsteps:
                    if (currentValue == null) //undefined
                    {
                        //7.1.1. If step's append flag is set, set the context's property named by the step's key to a new Array containing entry value as its only member. 
                        if (step.Append)
                        {
                            var arr = new JsonArray();
                            arr.Elements.Add(entryValue);
                            context[step.Key] = arr;
                        }
                        //7.1.2. Otherwise, set the context's property named by the step's key to entry value. 
                        else
                        {
                            context[step.Key] = entryValue;
                        }
                    }
                    //7.2. Else if current value is an Array, then get the context's property named by the step's key and push entry value onto it. 
                    else if (currentValue is JsonArray)
                    {
                        (context[step.Key] as JsonArray).Elements.Add(entryValue);
                    }
                    //7.3. Else if current value is an Object and the is file flag is not set, then run the steps to set a JSON encoding value with
                    //context set to the current value;
                    //a step with its type set to "object", its key set to the empty string, and its last flag set;
                    //current value set to the current value's property named by the empty string;
                    //the entry value;
                    //and the is file flag.
                    //Return the result. 
                    else if (currentValue is JsonObject && !isFile)
                    {
                        return JsonEncodeValue(currentValue, new Step { Type = StepType.Object, Key = "", Last = true }, currentValue[""], entryValue, true);
                    }
                    //7.4. Otherwise, set the context's property named by the step's key to an Array containing current value and entry value, in this order. 
                    else
                    {
                        JsonArray arr = new JsonArray();
                        arr.Elements.Add(currentValue);
                        arr.Elements.Add(entryValue);
                        context[step.Key] = arr;
                    }
                    //7.5. Return context.
                    return context;
                }
                //8. Otherwise, run the following substeps:
                else
                {
                    //8.1. If current value is undefined, run the following subsubsteps:
                    if (currentValue == null)
                    {
                        //8.1.1. If step's next type is "array", set the context's property named by the step's key to a new empty Array and return it. 
                        if (step.NextType == StepType.Array)
                        {
                            return context[step.Key] = new JsonArray();
                        }
                        //8.2.2. Otherwise,set the context's property named by the step's key to a new empty Object and return it. 
                        else
                        {
                            return context[step.Key] = new JsonObject();
                        }
                    }
                    //8.2. Else if current value is an Object, then return the value of the context's property named by the step's key. 
                    else if (currentValue is JsonObject)
                    {
                        return context[step.Key];
                    }
                    //8.3. Else if current value is an Array, then run the following subsubsteps:
                    else if (currentValue is JsonArray)
                    {
                        //8.3.1. If step's next type is "array", return current value.
                        if (step.NextType == StepType.Array)
                        {
                            return currentValue;
                        }
                        //8.3.2. Otherwise, run the following subsubsubsteps:
                        else
                        {
                            //8.3.2.1. Let object be a new empty Object.
                            var @object = new JsonObject();

                            //8.3.2.2. For each item and zero-based index i in current value, if item is not undefined then set a property of object named i to item. 
                            int i = 0;
                            foreach (var item in (currentValue as JsonArray).Elements)
                            {
                                if (item != null)
                                {
                                    @object[i] = item;
                                }
                                i++;
                            }

                            //8.3.2.3. Otherwise, set the context's property named by the step's key to object. 
                            context[step.Key] = @object;

                            //8.3.2.4. Return object.
                            return @object;
                        }
                    }
                    //8.4. Otherwise, run the following subsubsteps:
                    else
                    {
                        //8.4.1. Let object be a new Object with a property named by the empty string set to current value.
                        var @object = new JsonObject();
                        @object[""] = currentValue;

                        //8.4.2. Set the context's property named by the step's key to object. 
                        context[step.Key] = @object;

                        //8.4.3. Return object.
                        return @object;
                    }
                }
            }
        }

        sealed class TextDataSetEntry : FormDataSetEntry
        {
            readonly String _value;

            public TextDataSetEntry(String name, String value, String type)
                : base(name, type)
            {
                _value = value;
            }

            /// <summary>
            /// Gets if the value has been given.
            /// </summary>
            public Boolean HasValue
            {
                get { return _value != null; }
            }

            /// <summary>
            /// Gets the entry's value.
            /// </summary>
            public String Value
            {
                get { return _value; }
            }

            public override Boolean Contains(String boundary, Encoding encoding)
            {
                if (_value == null)
                    return false;

                return _value.Contains(boundary);
            }

            public override Action<StreamWriter> AsMultipart(Encoding encoding)
            {
                if (HasName && HasValue)
                {
                    return stream =>
                    {
                        stream.WriteLine(String.Concat("Content-Disposition: form-data; name=\"",
                            Name.HtmlEncode(encoding), "\""));
                        stream.WriteLine();
                        stream.WriteLine(_value.HtmlEncode(encoding));
                    };
                }

                return null;
            }

            public override Tuple<String, String> AsPlaintext()
            {
                if (HasName && HasValue)
                    return Tuple.Create(Name, _value);

                return null;
            }

            public override Tuple<String, String> AsUrlEncoded(Encoding encoding)
            {
                if (HasName && HasValue)
                {
                    var name = encoding.GetBytes(Name);
                    var value = encoding.GetBytes(_value);
                    return Tuple.Create(name.UrlEncode(), value.UrlEncode());
                }

                return null;
            }

            public override void AsJson(JsonElement context)
            {
                JsonValue entryValue = new JsonValue(Value);

                //2.2. Let steps be the result of running the steps to parse a JSON encoding path on the entry's name. 
                List<Step> steps = ParseJSONPath(Name);

                //2.4. For each step in the list of steps, run the following subsubsteps:
                foreach (var step in steps)
                {
                    //2.4.1. Let the current value be the value obtained by getting the step's key from the current context. 
                    JsonElement currentValue = context[step.Key];

                    //2.4.2. Run the steps to set a JSON encoding value with the current context, the step, the current value, the entry's value, and the is file flag. 
                    //2.4.3. Update context to be the value returned by the steps to set a JSON encoding value ran above.
                    context = JsonEncodeValue(context, step, currentValue, entryValue, isFile: false);
                }
            }
        }

        sealed class FileDataSetEntry : FormDataSetEntry
        {
            readonly IFile _value;

            public FileDataSetEntry(String name, IFile value, String type)
                : base(name, type)
            {
                _value = value;
            }

            /// <summary>
            /// Gets if the value has been given.
            /// </summary>
            public Boolean HasValue
            {
                get { return _value != null && _value.Name != null; }
            }

            /// <summary>
            /// Gets if the value has a body and type.
            /// </summary>
            public Boolean HasValueBody
            {
                get { return _value != null && _value.Body != null && _value.Type != null; }
            }

            /// <summary>
            /// Gets the entry's value.
            /// </summary>
            public IFile Value
            {
                get { return _value; }
            }

            public String FileName
            {
                get { return _value != null ? _value.Name : String.Empty; }
            }

            public String ContentType
            {
                get { return _value != null ? _value.Type : MimeTypes.Binary; }
            }

            public override Boolean Contains(String boundary, Encoding encoding)
            {
                if (_value == null || _value.Body == null)
                    return false;

                //TODO boundary check required?
                return false;
            }

            public override Action<StreamWriter> AsMultipart(Encoding encoding)
            {
                if (HasName)
                {
                    return stream =>
                    {
                        var hasContent = HasValue && HasValueBody;

                        stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"",
                            Name.HtmlEncode(encoding), FileName.HtmlEncode(encoding));

                        stream.WriteLine("Content-Type: " + ContentType);
                        stream.WriteLine();

                        if (hasContent)
                        {
                            stream.Flush();
                            _value.Body.CopyTo(stream.BaseStream);
                        }

                        stream.WriteLine();
                    };
                }

                return null;
            }

            public override Tuple<String, String> AsPlaintext()
            {
                if (HasName && HasValue)
                    return Tuple.Create(Name, _value.Name);

                return null;
            }

            public override Tuple<String, String> AsUrlEncoded(Encoding encoding)
            {
                if (HasName && HasValue)
                {
                    var name = encoding.GetBytes(Name);
                    var value = encoding.GetBytes(_value.Name);
                    return Tuple.Create(name.UrlEncode(), value.UrlEncode());
                }

                return null;
            }

            public override void AsJson(JsonElement context)
            {
                byte[] data;
                var stream = Value.Body;
                MemoryStream ms = stream as MemoryStream;
                if (ms != null)
                {
                    data = ms.ToArray();
                }
                else
                {
                    data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                }

                //6. If is file is set then replace entry value with an Object have its "name" property set to the file's name, its "type" property set to the file's type, and its "body" property set to the Base64 encoding of the file's body. [RFC2045]  
                JsonObject entryValue = new JsonObject();
                entryValue["type"] = new JsonValue(ContentType);
                entryValue["name"] = new JsonValue(FileName);
                entryValue["body"] = new JsonValue(Convert.ToBase64String(data));

                //2.2. Let steps be the result of running the steps to parse a JSON encoding path on the entry's name. 
                List<Step> steps = ParseJSONPath(Name);

                //2.4. For each step in the list of steps, run the following subsubsteps:
                foreach (var step in steps)
                {
                    //2.4.1. Let the current value be the value obtained by getting the step's key from the current context. 
                    JsonElement currentValue = context[step.Key];

                    //2.4.2. Run the steps to set a JSON encoding value with the current context, the step, the current value, the entry's value, and the is file flag. 
                    //2.4.3. Update context to be the value returned by the steps to set a JSON encoding value ran above.
                    context = JsonEncodeValue(context, step, currentValue, entryValue, isFile: true);
                }
            }
        }

        #endregion

        #region Form Submission Classes
        abstract class JsonElement
        {
            public abstract JsonElement this[object key] { get; set; }
            public abstract void WriteTo(StreamWriter writer);
        }

        class JsonValue : JsonElement
        {
            public object Value { get; set; }

            public override JsonElement this[object key]
            {
                get { throw new NotSupportedException(); }

                set { throw new NotSupportedException(); }
            }

            public JsonValue(object value)
            {
                Value = value;
            }

            public override void WriteTo(StreamWriter writer)
            {
                writer.Write(Value is string ? "\"" + Value + "\"" : Value.ToString());
            }

            public override string ToString()
            {
                return Value is string ? "\"" + Value + "\"" : Value.ToString();
            }
        }

        class JsonObject : JsonElement
        {
            public Dictionary<string, JsonElement> Properties { get; set; } = new Dictionary<string, JsonElement>();

            public override JsonElement this[object key]
            {
                get { JsonElement tmp; Properties.TryGetValue(key.ToString(), out tmp); return tmp; }

                set { Properties[key.ToString()] = value; }
            }

            public override void WriteTo(StreamWriter writer)
            {
                writer.Write('{');
                bool needsComma = false;
                foreach (KeyValuePair<string, JsonElement> property in Properties)
                {
                    if (needsComma)
                    {
                        writer.Write(',');
                    }
                    else
                    {
                        needsComma = true;
                    }

                    writer.Write("\"");
                    writer.Write(property.Key);
                    writer.Write('"');
                    writer.Write(':');
                    writer.Write(property.Value.ToString());
                }
                writer.Write('}');
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder("{");
                foreach (KeyValuePair<string, JsonElement> property in Properties)
                {
                    sb.Append("\"").Append(property.Key).Append("\"").Append(":").Append(property.Value.ToString()).Append(",");
                }

                if (sb.Length > 1)
                    sb.Length -= 1;

                sb.Append("}");
                return sb.ToString();
            }
        }

        class JsonArray : JsonElement
        {
            public List<JsonElement> Elements { get; set; } = new List<JsonElement>();

            public override JsonElement this[object key]
            {
                get { return Elements.ElementAtOrDefault((int)key); }

                set
                {
                    var index = (int)key;
                    for (int i = Elements.Count; i <= index; i++)
                    {
                        Elements.Add(null);
                    }
                    Elements[index] = value;
                }
            }

            public override void WriteTo(StreamWriter writer)
            {
                writer.Write('[');
                bool needsComma = false;
                foreach (JsonElement element in Elements)
                {
                    if (needsComma)
                    {
                        writer.Write(',');
                    }
                    else
                    {
                        needsComma = true;
                    }

                    writer.Write(element?.ToString() ?? "null");
                }
                writer.Write(']');
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder("[");
                foreach (JsonElement element in Elements)
                {
                    sb.Append(element?.ToString() ?? "null").Append(",");
                }

                if (sb.Length > 1)
                    sb.Length -= 1;

                sb.Append("]");
                return sb.ToString();
            }
        }

        class Step
        {
            public bool Append { get; internal set; }
            public object Key { get; internal set; }
            public bool Last { get; internal set; }
            public StepType NextType { get; internal set; }
            public StepType Type { get; internal set; }

            public override string ToString()
            {
                return String.Format("{0} [{1}]", Key, Type);
            }
        }

        enum StepType
        {
            Object,
            Array
        }
        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over all entry names.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return _entries.Select(m => m.Name).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
