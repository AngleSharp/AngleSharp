namespace AngleSharp.DOM
{
	using System;

	[DOM("Text")]
	interface IText : ICharacterData
	{
		[DOM("splitText")]
		IText Split(Int32 offset);

		[DOM("wholeText")]
		String Text { get; }
	}
}