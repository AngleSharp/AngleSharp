namespace AngleSharp.Dom.Css
{
	/// <summary>
	/// An enumeration with all possible text justification options.
	/// </summary>
	public enum TextJustify : byte
	{
		/// <summary>
		/// The browser determines the justification algorithm
		/// </summary>
		Auto,
		/// <summary>
		/// Increases/Decreases the space between words
		/// </summary>
		InterWord,
		/// <summary>
		/// Justifies content with ideographic text
		/// </summary>
		InterIdeograph,
		/// <summary>
		/// Only content that does not contain any inter-word spacing 
		/// (such as Asian languages) is justified
		/// </summary>
		InterCluster,
		/// <summary>
		/// Handles spacing much like the newspaper value. 
		/// This form of justification is optimized for documents in Asian languages, 
		/// such as Thai.
		/// </summary>
		Distribute,
		/// <summary>
		/// Justifies lines of text in the same way as the distribute value, 
		/// except that it also justifies the last line of the paragraph
        /// </summary>
		DistributeAllLines,
		/// <summary>
		/// Not implemented
		/// </summary>
		DistributeCenterLast,
		/// <summary>
		/// Justifies content by elongating characters
		/// </summary>
		Kashida,
		/// <summary>
		/// Increases or decreases the spacing between letters and between words. 
		/// It is the most sophisticated form of justification for Latin alphabets.
		/// </summary>
		Newspaper
	}
}
