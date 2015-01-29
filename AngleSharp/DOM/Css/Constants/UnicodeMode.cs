namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with various unicode modes.
    /// </summary>
    public enum UnicodeMode : ushort
    {
        /// <summary>
        /// The element does not offer a additional level of embedding
        /// with respect to the bidirectional algorithm. For inline elements
        /// implicit reordering works across element boundaries.
        /// </summary>
        Normal,
        /// <summary>
        /// If the element is inline, this value opens an additional level of
        /// embedding with respect to the bidirectional algorithm. The
        /// direction of this embedding level is given by the direction property.
        /// </summary>
        Embed,
        /// <summary>
        /// This keyword indicates that the element's container directionality
        /// should be calculated without considering the content of this element.
        /// The element is therefore isolated from its siblings. When applying
        /// its bidirectional-resolution algorithm, its container element treats
        /// it as one or several U+FFFC Object Replacement Character, i.e. like an image.
        /// </summary>
        Isolate,
        /// <summary>
        /// For inline elements this creates an override. For block container
        /// elements this creates an override for inline-level descendants not
        /// within another block container element. This means that inside the element,
        /// reordering is strictly in sequence according to the direction property; the
        /// implicit part of the bidirectional algorithm is ignored.
        /// </summary>
        BidiOverride,
        /// <summary>
        /// This keyword applies the isolation behavior of the isolate keyword to the
        /// surrounding content and the override behavior of the bidi-override keyword
        /// to the inner content.
        /// </summary>
        IsolateOverride,
        /// <summary>
        /// This keyword makes the elements directionality calculated without considering
        /// its parent bidirectional state or the value of the direction property. 
        /// </summary>
        Plaintext
    }
}
