using AngleSharp.DOM;
using AngleSharp.DOM.Xml;
using AngleSharp.DTD;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The XML validator for document validation.
    /// </summary>
    [DebuggerStepThrough]
    sealed class XmlValidator
    {
        #region Members

        DtdContainer _dtd;
        Dictionary<String, Int32> _elements;

        #endregion

        #region ctor

        public XmlValidator()
        {
            _elements = new Dictionary<String, Int32>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Document Type Definition if any.
        /// </summary>
        public DtdContainer Definition
        {
            get { return _dtd; }
            set { _dtd = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Runs the Validation with the given document.
        /// </summary>
        /// <param name="doc">The document to inspect.</param>
        /// <returns>True if the validation has been successful, otherwise false.</returns>
        public static Boolean Run(XMLDocument doc)
        {
            if (doc.Doctype == null || doc.Doctype.TypeDefinitions == null)
                return false;

            var validator = new XmlValidator();
            validator.Definition = doc.Doctype.TypeDefinitions;

            if (!validator.Definition.IsInvalid && doc.DocumentElement.NodeName == doc.Doctype.Name)
                return validator.Inspect(doc.DocumentElement);

            return false;
        }

        /// <summary>
        /// Validates the given element and its children (if any).
        /// </summary>
        /// <param name="element">The element to validate.</param>
        /// <returns>True if the given element and its children are valid, otherwise false.</returns>
        public Boolean Validate(Element element)
        {
            if (_dtd == null)
                return false;

            return ValidateElement(element) && ValidateAttribute(element);
        }

        #endregion

        #region Helpers

        Boolean ValidateElement(Element element)
        {
            foreach (var def in _dtd.Elements)
            {
                if (def.Name == element.NodeName)
                {
                    if (!def.Check(element))
                        break;

                    IncreaseCounter(def.Name);
                    return true;
                }
            }

            return false;
        }

        Boolean ValidateAttribute(Element element)
        {
            foreach (var def in _dtd.Attributes)
            {
                if (def.Name == element.NodeName)
                    return def.Check(element);
            }

            return element.Attributes.Length == 0;
        }

        void IncreaseCounter(String elementName)
        {
            if (_elements.ContainsKey(elementName))
                _elements[elementName] += 1;
            else
                _elements[elementName] = 1;
        }

        Boolean Inspect(Element element)
        {
            if (Validate(element))
            {
                foreach (var child in element.Children)
                {
                    if (!Inspect(child))
                        return false;
                }

                return true;
            }

            return false;
        }

        #endregion
    }
}
