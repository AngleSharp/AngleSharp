namespace AngleSharp.DOM.Html
{
    using AngleSharp.Infrastructure;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    sealed class HTMLScriptElement : HTMLElement, IHtmlScriptElement
    {
        #region Fields

        Boolean _started;
        Boolean _parserInserted;
        Boolean _wasParserInserted;
        Boolean _forceAsync;
        Boolean _readyToBeExecuted;
        Task<Stream> _load;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        internal HTMLScriptElement()
        {
            _name = Tags.Script;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the language of the script.
        /// </summary>
        public String ScriptLanguage
        {
            get
            {
                var type = Type ?? (GetAttribute(AttributeNames.Language) != null ? "text/" + GetAttribute(AttributeNames.Language) : null);

                if (String.IsNullOrEmpty(type))
                    return MimeTypes.DefaultJavaScript;

                return type;
            }
        }

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        public String CharacterSet
        {
            get { return GetAttribute(AttributeNames.Charset); }
            set { SetAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the text in the script element.
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets or sets how the element handles crossorigin requests.
        /// </summary>
        public String CrossOrigin
        {
            get { return GetAttribute(AttributeNames.CrossOrigin); }
            set { SetAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        public Boolean IsDeferred
        {
            get { return GetAttribute(AttributeNames.Defer) != null; }
            set { SetAttribute(AttributeNames.Defer, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return GetAttribute(AttributeNames.Async) != null; }
            set { SetAttribute(AttributeNames.Async, value ? String.Empty : null); }
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
        /// </summary>
        internal void Run()
        {
            if (_load == null)
                return;

            if (_load.Exception != null || _load.IsFaulted)
            {
                FireSimpleEvent(EventNames.Error);
                return;
            }

            if (FireSimpleEvent(EventNames.BeforeScriptExecute, cancelable: true))
                return;

            Owner.Options.RunScript(_load.Result, CreateOptions(), ScriptLanguage);
            FireSimpleEvent(EventNames.AfterScriptExecute, bubble: true);

            if (Source != null)
                FireSimpleEvent(EventNames.Load);
            else
                Owner.QueueTask(() => FireSimpleEvent(EventNames.Load));
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/webappapis.html#create-a-script
        /// </summary>
        void CreateScript()
        {
            //TODO
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal void Prepare()
        {
            if (_started)
                return;

            _wasParserInserted = _parserInserted;
            _parserInserted = false;

            _forceAsync = _wasParserInserted && !IsAsync;

            if ((String.IsNullOrEmpty(Source) && String.IsNullOrEmpty(Text)) || Owner == null)
                return;

            if (Owner.Options.GetScriptEngine(ScriptLanguage) == null)
                return;

            if (_wasParserInserted)
            {
                _parserInserted = true;
                _forceAsync = false;
            }

            _started = true;

            if (!Owner.Options.IsScripting)
                return;

            var @event = GetAttribute(AttributeNames.Event);
            var @for = GetAttribute(AttributeNames.For);

            if (!String.IsNullOrEmpty(@event) && !String.IsNullOrEmpty(@for))
            {
                @event = @event.Trim();
                @for = @for.Trim();

                if (@event.EndsWith("()"))
                    @event = @event.Substring(0, @event.Length - 2);

                if (!@for.Equals("window", StringComparison.OrdinalIgnoreCase) || !@event.Equals("onload", StringComparison.OrdinalIgnoreCase))
                    return;
            }

            var src = Source;

            if (src != null)
            {
                if (src == String.Empty)
                {
                    Owner.QueueTask(() => FireSimpleEvent(EventNames.Error));
                    return;
                }

                src = HyperRef(src);

                //TODO
                //Do a potentially CORS-enabled fetch of the resulting absolute URL, with the mode being the current state
                //of the element's crossorigin content attribute, the origin being the origin of the script element's Document,
                //and the default origin behaviour set to taint.

                if (_parserInserted && !IsAsync)
                {
                    if (IsDeferred)
                    {
                        //The element must be added to the end of the list of scripts that will execute when the document has finished
                        //parsing associated with the Document of the parser that created the element.
                    }
                    else
                    {
                        //The element is the pending parsing-blocking script of the Document of the parser that created the element.
                        //(There can only be one such script per Document at a time.)
                    }

                    PlaceNetworkTask(src);
                }
                else if (!IsAsync && !_forceAsync)
                {
                    //The element must be added to the end of the list of scripts that will execute in order as soon as possible associated
                    //with the Document of the script element at the time the prepare a script algorithm started.
                }
                else
                {
                    //The element must be added to the set of scripts that will execute as soon as possible of the Document of the
                    //script element at the time the prepare a script algorithm started.
                }
            }
            else if (_parserInserted) //and either the parser that created the script is an XML parser or it's an HTML parser whose script nesting level is not greater than one, and the Document of the HTML parser or XML parser that created the script element has a style sheet that is blocking scripts
            {
                //The element is the pending parsing-blocking script of the Document of the parser that created the element.
                //(There can only be one such script per Document at a time.)
                _readyToBeExecuted = true;
            }
            else
            {
                Owner.Options.RunScript(Text, CreateOptions(), ScriptLanguage);
            }
        }

        ScriptOptions CreateOptions()
        {
            return new ScriptOptions
            {
                Context = null, //TODO
                Document = Owner,
                Element = this
            };
        }

        void PlaceNetworkTask(String url)
        {
            //The task that the networking task source places on the task queue once the fetching algorithm has completed must
            //set the element's "ready to be parser-executed" flag. The parser will handle executing the script.
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public override String ToText()
        {
            return String.Empty;
        }

        #endregion
    }
}
