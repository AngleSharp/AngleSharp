namespace AngleSharp.Html.Submitters.Json
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    abstract class JsonStep
    {
        public Boolean Append { get; set; }

        public JsonStep Next { get; set; }

        public static IEnumerable<JsonStep> Parse(String path)
        {
            var steps = new List<JsonStep>();
            var index = 0;

            while (index < path.Length && path[index] != Symbols.SquareBracketOpen)
            {
                index++;
            }

            if (index == 0)
            {
                return FailedJsonSteps(path);
            }

            steps.Add(new ObjectStep(path.Substring(0, index)));

            while (index < path.Length)
            {
                if (index + 1 >= path.Length || path[index] != Symbols.SquareBracketOpen)
                {
                    return FailedJsonSteps(path);
                }
                else if (path[index + 1] == Symbols.SquareBracketClose)
                {
                    steps[steps.Count - 1].Append = true;
                    index += 2;

                    if (index < path.Length)
                        return FailedJsonSteps(path);
                }
                else if (path[index + 1].IsDigit())
                {
                    var start = ++index;

                    while (index < path.Length && path[index] != Symbols.SquareBracketClose)
                    {
                        if (!path[index].IsDigit())
                            return FailedJsonSteps(path);

                        index++;
                    }

                    if (index == path.Length)
                        return FailedJsonSteps(path);

                    steps.Add(new ArrayStep(path.Substring(start, index - start).ToInteger(0)));
                    index++;
                }
                else
                {
                    var start = ++index;

                    while (index < path.Length && path[index] != Symbols.SquareBracketClose)
                    {
                        index++;
                    }

                    if (index == path.Length)
                        return FailedJsonSteps(path);

                    steps.Add(new ObjectStep(path.Substring(start, index - start)));
                    index++;
                }
            }

            for (int i = 0, n = steps.Count - 1; i < n; i++)
            {
                steps[i].Next = steps[i + 1];
            }

            return steps;
        }

        static IEnumerable<JsonStep> FailedJsonSteps(String original)
        {
            return new[] { new ObjectStep(original) };
        }

        protected abstract JsonElement CreateElement();

        protected abstract JsonElement SetValue(JsonElement context, JsonElement value);

        protected abstract JsonElement GetValue(JsonElement context);

        protected abstract JsonElement ConvertArray(JsonArray value);

        public JsonElement Run(JsonElement context, JsonElement value, Boolean file = false)
        {
            if (Next == null)
            {
                return JsonEncodeLastValue(context, value, file);
            }
            else
            {
                return JsonEncodeValue(context, value, file);
            }
        }

        JsonElement JsonEncodeValue(JsonElement context, JsonElement value, Boolean file)
        {
            var currentValue = GetValue(context);

            if (currentValue == null)
            {
                var newValue = Next.CreateElement();
                return SetValue(context, newValue);
            }
            else if (currentValue is JsonObject)
            {
                return currentValue;
            }
            else if (currentValue is JsonArray)
            {
                return SetValue(context, Next.ConvertArray((JsonArray)currentValue));
            }
            else
            {
                var obj = new JsonObject { [String.Empty] = currentValue };
                return SetValue(context, obj);
            }
        }

        JsonElement JsonEncodeLastValue(JsonElement context, JsonElement value, Boolean file)
        {
            var currentValue = GetValue(context);

            //undefined
            if (currentValue == null)
            {
                if (Append)
                {
                    var arr = new JsonArray();
                    arr.Push(value);
                    value = arr;
                }

                SetValue(context, value);
            }
            else if (currentValue is JsonArray)
            {
                ((JsonArray)currentValue).Push(value);
            }
            else if (currentValue is JsonObject && !file)
            {
                var step = new ObjectStep(String.Empty);
                return step.JsonEncodeLastValue(currentValue, value, file: true);
            }
            else
            {
                var arr = new JsonArray();
                arr.Push(currentValue);
                arr.Push(value);
                SetValue(context, arr);
            }

            return context;
        }

        sealed class ObjectStep : JsonStep
        {
            public ObjectStep(String key)
            {
                Key = key;
            }

            public String Key { get; private set; }

            protected override JsonElement GetValue(JsonElement context)
            {
                return context[Key];
            }

            protected override JsonElement SetValue(JsonElement context, JsonElement value)
            {
                context[Key] = value;
                return value;
            }

            protected override JsonElement CreateElement()
            {
                return new JsonObject();
            }

            protected override JsonElement ConvertArray(JsonArray values)
            {
                var obj = new JsonObject();

                for (var i = 0; i < values.Length; i++)
                {
                    var item = values[i];

                    if (item != null)
                    {
                        obj[i.ToString()] = item;
                    }
                }

                return obj;
            }
        }

        sealed class ArrayStep : JsonStep
        {
            public ArrayStep(Int32 key)
            {
                Key = key;
            }

            public Int32 Key { get; private set; }

            protected override JsonElement GetValue(JsonElement context)
            {
                var array = context as JsonArray;

                if (array == null)
                {
                    return context[Key.ToString()];
                }

                return array[Key];
            }

            protected override JsonElement SetValue(JsonElement context, JsonElement value)
            {
                var array = context as JsonArray;

                if (array != null)
                {
                    array[Key] = value;
                }
                else
                {
                    context[Key.ToString()] = value;
                }

                return value;
            }

            protected override JsonElement CreateElement()
            {
                return new JsonArray();
            }

            protected override JsonElement ConvertArray(JsonArray value)
            {
                return value;
            }
        }
    }
}
