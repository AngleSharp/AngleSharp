namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    sealed class CSSUnicodeBidiProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, UnicodeMode> modes = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase);
        UnicodeMode _mode;

        #endregion

        #region ctor

        static CSSUnicodeBidiProperty()
        {
            modes.Add("normal", new NormalUnicodeMode());
            modes.Add("embed", new EmbedUnicodeMode());
            modes.Add("isolate", new IsolateUnicodeMode());
            modes.Add("isolate-override", new IsolateOverrideUnicodeMode());
            modes.Add("bidi-override", new BidiOverrideUnicodeMode());
            modes.Add("plaintext", new PlaintextUnicodeMode());
        }

        public CSSUnicodeBidiProperty()
            : base(PropertyNames.UnicodeBidi)
        {
            _mode = modes["normal"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                UnicodeMode mode;

                if (modes.TryGetValue(ident.Value, out mode))
                {
                    _mode = mode;
                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion

        #region Modes
        
        abstract class UnicodeMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The element does not offer a additional level of embedding
        /// with respect to the bidirectional algorithm. For inline elements
        /// implicit reordering works across element boundaries.
        /// </summary>
        sealed class NormalUnicodeMode : UnicodeMode
        {
        }

        /// <summary>
        /// If the element is inline, this value opens an additional level of
        /// embedding with respect to the bidirectional algorithm. The
        /// direction of this embedding level is given by the direction property.
        /// </summary>
        sealed class EmbedUnicodeMode : UnicodeMode
        {
        }

        /// <summary>
        /// This keyword indicates that the element's container directionality
        /// should be calculated without considering the content of this element.
        /// The element is therefore isolated from its siblings. When applying
        /// its bidirectional-resolution algorithm, its container element treats
        /// it as one or several U+FFFC Object Replacement Character, i.e. like an image.
        /// </summary>
        sealed class IsolateUnicodeMode : UnicodeMode
        {
        }

        /// <summary>
        /// For inline elements this creates an override. For block container
        /// elements this creates an override for inline-level descendants not
        /// within another block container element. This means that inside the element,
        /// reordering is strictly in sequence according to the direction property; the
        /// implicit part of the bidirectional algorithm is ignored.
        /// </summary>
        sealed class BidiOverrideUnicodeMode : UnicodeMode
        {
        }

        /// <summary>
        /// This keyword applies the isolation behavior of the isolate keyword to the
        /// surrounding content and the override behavior of the bidi-override keyword
        /// to the inner content.
        /// </summary>
        sealed class IsolateOverrideUnicodeMode : UnicodeMode
        {
        }

        /// <summary>
        /// This keyword makes the elements directionality calculated without considering
        /// its parent bidirectional state or the value of the direction property. 
        /// </summary>
        sealed class PlaintextUnicodeMode : UnicodeMode
        {
        }

        #endregion
    }
}
