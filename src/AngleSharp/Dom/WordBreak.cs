namespace AngleSharp.Dom
{
	/// <summary>
	/// The list of possible horizontal alignments.
	/// </summary>
	public enum WordBreak : byte
	{
		/// <summary>
		/// Use the default line break rule.
		/// </summary>
		Normal,
		/// <summary>
		/// Word breaks may be inserted between any 
		/// character for non-CJK (Chinese/Japanese/Korean) text.
		/// </summary>
		BreakAll,
		/// <summary>
		/// Don't allow word breaks for CJK text.  
		/// Non-CJK text behavior is the same as for normal.
		/// </summary>
		KeepAll
	}
}
