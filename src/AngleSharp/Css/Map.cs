namespace AngleSharp.Css
{
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of mappings for CSS (keywords to constants).
    /// </summary>
    static class Map
    {
        /// <summary>
        /// Contains the string-Whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, Whitespace> WhitespaceModes = new Dictionary<String, Whitespace>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, Whitespace.Normal },
            { Keywords.Pre, Whitespace.Pre },
            { Keywords.Nowrap, Whitespace.NoWrap },
            { Keywords.PreWrap, Whitespace.PreWrap },
            { Keywords.PreLine, Whitespace.PreLine },
        };

        /// <summary>
        /// Contains the string-TextTransform mapping.
        /// </summary>
        public static readonly Dictionary<String, TextTransform> TextTransforms = new Dictionary<String, TextTransform>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, TextTransform.None },
            { Keywords.Capitalize, TextTransform.Capitalize },
            { Keywords.Uppercase, TextTransform.Uppercase },
            { Keywords.Lowercase, TextTransform.Lowercase },
            { Keywords.FullWidth, TextTransform.FullWidth },
        };

		/// <summary>
		/// Contains the string-TextAlignLast mapping.
		/// </summary>
		public static readonly Dictionary<String, TextAlignLast> TextAlignmentsLast = new Dictionary<String, TextAlignLast>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Auto, TextAlignLast.Auto },
			{ Keywords.Start, TextAlignLast.Start },
			{ Keywords.End, TextAlignLast.End },
			{ Keywords.Right, TextAlignLast.Right },
			{ Keywords.Left, TextAlignLast.Left },
			{ Keywords.Center, TextAlignLast.Center },
			{ Keywords.Justify, TextAlignLast.Justify }
		};

		/// <summary>
		/// Contains the string-TextAnchor mapping.
		/// </summary>
		public static readonly Dictionary<String, TextAnchor> TextAnchors = new Dictionary<String, TextAnchor>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Start, TextAnchor.Start },
			{ Keywords.Middle, TextAnchor.Middle },
			{ Keywords.End, TextAnchor.End }
		};

		/// <summary>
		/// Contains the string-TextJustify mapping.
		/// </summary>
		public static readonly Dictionary<String, TextJustify> TextJustifyOptions = new Dictionary<String, TextJustify>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Auto, TextJustify.Auto },
			{ Keywords.Distribute, TextJustify.Distribute },
			{ Keywords.DistributeAllLines, TextJustify.DistributeAllLines },
			{ Keywords.DistributeCenterLast, TextJustify.DistributeCenterLast },
			{ Keywords.InterCluster, TextJustify.InterCluster },
			{ Keywords.InterIdeograph, TextJustify.InterIdeograph },
			{ Keywords.InterWord, TextJustify.InterWord },
			{ Keywords.Kashida, TextJustify.Kashida },
			{ Keywords.Newspaper, TextJustify.Newspaper }
		};

		/// <summary>
		/// Contains the string-HorizontalAlignment mapping.
		/// </summary>
		public static readonly Dictionary<String, HorizontalAlignment> HorizontalAlignments = new Dictionary<String, HorizontalAlignment>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Left, HorizontalAlignment.Left },
            { Keywords.Right, HorizontalAlignment.Right },
            { Keywords.Center, HorizontalAlignment.Center },
            { Keywords.Justify, HorizontalAlignment.Justify },
        };

        /// <summary>
        /// Contains the string-VerticalAlignment mapping.
        /// </summary>
        public static readonly Dictionary<String, VerticalAlignment> VerticalAlignments = new Dictionary<String, VerticalAlignment>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Baseline, VerticalAlignment.Baseline },
            { Keywords.Sub, VerticalAlignment.Sub },
            { Keywords.Super, VerticalAlignment.Super },
            { Keywords.TextTop, VerticalAlignment.TextTop },
            { Keywords.TextBottom, VerticalAlignment.TextBottom },
            { Keywords.Middle, VerticalAlignment.Middle },
            { Keywords.Top, VerticalAlignment.Top },
            { Keywords.Bottom, VerticalAlignment.Bottom },
        };

        /// <summary>
        /// Contains the string-LineStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, LineStyle> LineStyles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, LineStyle.None },
            { Keywords.Solid, LineStyle.Solid },
            { Keywords.Double, LineStyle.Double },
            { Keywords.Dotted, LineStyle.Dotted },
            { Keywords.Dashed, LineStyle.Dashed },
            { Keywords.Inset, LineStyle.Inset },
            { Keywords.Outset, LineStyle.Outset },
            { Keywords.Ridge, LineStyle.Ridge },
            { Keywords.Groove, LineStyle.Groove },
            { Keywords.Hidden, LineStyle.Hidden },
        };

        /// <summary>
        /// Contains the string-BoxModel mapping.
        /// </summary>
        public static readonly Dictionary<String, BoxModel> BoxModels = new Dictionary<String, BoxModel>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.BorderBox, BoxModel.BorderBox },
            { Keywords.PaddingBox, BoxModel.PaddingBox },
            { Keywords.ContentBox, BoxModel.ContentBox },
        };

        /// <summary>
        /// Contains the string-TimingFunction mapping.
        /// </summary>
        public static readonly Dictionary<String, ITimingFunction> TimingFunctions = new Dictionary<String, ITimingFunction>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Ease, new CubicBezierTimingFunction(0.25f, 0.1f, 0.25f, 1f) },
            { Keywords.EaseIn, new CubicBezierTimingFunction(0.42f, 0f, 1f, 1f) },
            { Keywords.EaseOut, new CubicBezierTimingFunction(0f, 0f, 0.58f, 1f) },
            { Keywords.EaseInOut, new CubicBezierTimingFunction(0.42f, 0f, 0.58f, 1f) },
            { Keywords.Linear, new CubicBezierTimingFunction(0f, 0f, 1f, 1f) },
            { Keywords.StepStart, new StepsTimingFunction(1, true) },
            { Keywords.StepEnd, new StepsTimingFunction(1, false) },
        };

        /// <summary>
        /// Contains the string-AnimationFillStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, AnimationFillStyle> AnimationFillStyles = new Dictionary<String, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, AnimationFillStyle.None },
            { Keywords.Forwards, AnimationFillStyle.Forwards },
            { Keywords.Backwards, AnimationFillStyle.Backwards },
            { Keywords.Both, AnimationFillStyle.Both },
        };

        /// <summary>
        /// Contains the string-AnimationDirection mapping.
        /// </summary>
        public static readonly Dictionary<String, AnimationDirection> AnimationDirections = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, AnimationDirection.Normal },
            { Keywords.Reverse, AnimationDirection.Reverse },
            { Keywords.Alternate, AnimationDirection.Alternate },
            { Keywords.AlternateReverse, AnimationDirection.AlternateReverse },
        };

        /// <summary>
        /// Contains the string-Visibility mapping.
        /// </summary>
        public static readonly Dictionary<String, Visibility> Visibilities = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Visible, Visibility.Visible },
            { Keywords.Hidden, Visibility.Hidden },
            { Keywords.Collapse, Visibility.Collapse },
        };

        /// <summary>
        /// Contains the string-PlayState mapping.
        /// </summary>
        public static readonly Dictionary<String, PlayState> PlayStates = new Dictionary<String, PlayState>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Running, PlayState.Running },
            { Keywords.Paused, PlayState.Paused },
        };

        /// <summary>
        /// Contains the string-FontVariant mapping.
        /// </summary>
        public static readonly Dictionary<String, FontVariant> FontVariants = new Dictionary<String, FontVariant>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, FontVariant.Normal },
            { Keywords.SmallCaps, FontVariant.SmallCaps },
        };

        /// <summary>
        /// Contains the string-DirectionMode mapping.
        /// </summary>
        public static readonly Dictionary<String, DirectionMode> DirectionModes = new Dictionary<String, DirectionMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Ltr, DirectionMode.Ltr },
            { Keywords.Rtl, DirectionMode.Rtl },
        };

        /// <summary>
        /// Contains the string-ListStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, ListStyle> ListStyles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Disc, ListStyle.Disc },
            { Keywords.Circle, ListStyle.Circle },
            { Keywords.Square, ListStyle.Square },
            { Keywords.Decimal, ListStyle.Decimal },
            { Keywords.DecimalLeadingZero, ListStyle.DecimalLeadingZero },
            { Keywords.LowerRoman, ListStyle.LowerRoman },
            { Keywords.UpperRoman, ListStyle.UpperRoman },
            { Keywords.LowerGreek, ListStyle.LowerGreek },
            { Keywords.LowerLatin, ListStyle.LowerLatin },
            { Keywords.UpperLatin, ListStyle.UpperLatin },
            { Keywords.Armenian, ListStyle.Armenian },
            { Keywords.Georgian, ListStyle.Georgian },
            { Keywords.LowerAlpha, ListStyle.LowerLatin },
            { Keywords.UpperAlpha, ListStyle.UpperLatin },
            { Keywords.None, ListStyle.None },
        };

        /// <summary>
        /// Contains the string-ListPosition mapping.
        /// </summary>
        public static readonly Dictionary<String, ListPosition> ListPositions = new Dictionary<String, ListPosition>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Inside, ListPosition.Inside },
            { Keywords.Outside, ListPosition.Outside },
        };

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, FontSize> FontSizes = new Dictionary<String, FontSize>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.XxSmall, FontSize.Tiny },
            { Keywords.XSmall, FontSize.Little },
            { Keywords.Small, FontSize.Small },
            { Keywords.Medium, FontSize.Medium },
            { Keywords.Large, FontSize.Large },
            { Keywords.XLarge, FontSize.Big },
            { Keywords.XxLarge, FontSize.Huge },
            { Keywords.Larger, FontSize.Smaller },
            { Keywords.Smaller, FontSize.Larger },
        };

        /// <summary>
        /// Contains the string-TextDecorationStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, TextDecorationStyle> TextDecorationStyles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Solid, TextDecorationStyle.Solid },
            { Keywords.Double, TextDecorationStyle.Double },
            { Keywords.Dotted, TextDecorationStyle.Dotted },
            { Keywords.Dashed, TextDecorationStyle.Dashed },
            { Keywords.Wavy, TextDecorationStyle.Wavy },
        };

        /// <summary>
        /// Contains the string-TextDecorationLine mapping.
        /// </summary>
        public static readonly Dictionary<String, TextDecorationLine> TextDecorationLines = new Dictionary<String, TextDecorationLine>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Underline, TextDecorationLine.Underline },
            { Keywords.Overline, TextDecorationLine.Overline },
            { Keywords.LineThrough, TextDecorationLine.LineThrough },
            { Keywords.Blink, TextDecorationLine.Blink },
        };

        /// <summary>
        /// Contains the string-BorderRepeat mapping.
        /// </summary>
        public static readonly Dictionary<String, BorderRepeat> BorderRepeatModes = new Dictionary<String, BorderRepeat>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Stretch, BorderRepeat.Stretch },
            { Keywords.Repeat, BorderRepeat.Repeat },
            { Keywords.Round, BorderRepeat.Round },
        };

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, String> DefaultFontFamilies = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Serif, "Times New Roman" },
            { Keywords.SansSerif, "Arial" },
            { Keywords.Monospace, "Consolas" },
            { Keywords.Cursive, "Cursive" },
            { Keywords.Fantasy, "Comic Sans" },
        };

        /// <summary>
        /// Contains the string-BackgroundAttachment mapping.
        /// </summary>
        public static readonly Dictionary<String, BackgroundAttachment> BackgroundAttachments = new Dictionary<String, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Fixed, BackgroundAttachment.Fixed },
            { Keywords.Local, BackgroundAttachment.Local },
            { Keywords.Scroll, BackgroundAttachment.Scroll },
        };

        /// <summary>
        /// Contains the string-FontStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, FontStyle> FontStyles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, FontStyle.Normal },
            { Keywords.Italic, FontStyle.Italic },
            { Keywords.Oblique, FontStyle.Oblique },
        };

        /// <summary>
        /// Contains the string-FontStretch mapping.
        /// </summary>
        public static readonly Dictionary<String, FontStretch> FontStretches = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, FontStretch.Normal },
            { Keywords.UltraCondensed, FontStretch.UltraCondensed },
            { Keywords.ExtraCondensed, FontStretch.ExtraCondensed },
            { Keywords.Condensed, FontStretch.Condensed },
            { Keywords.SemiCondensed, FontStretch.SemiCondensed },
            { Keywords.SemiExpanded, FontStretch.SemiExpanded },
            { Keywords.Expanded, FontStretch.Expanded },
            { Keywords.ExtraExpanded, FontStretch.ExtraExpanded },
            { Keywords.UltraExpanded, FontStretch.UltraExpanded },
        };

        /// <summary>
        /// Contains the string-BreakMode (general) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> BreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Auto, BreakMode.Auto },
            { Keywords.Always, BreakMode.Always },
            { Keywords.Avoid, BreakMode.Avoid },
            { Keywords.Left, BreakMode.Left },
            { Keywords.Right, BreakMode.Right },
            { Keywords.Page, BreakMode.Page },
            { Keywords.Column, BreakMode.Column },
            { Keywords.AvoidPage, BreakMode.AvoidPage },
            { Keywords.AvoidColumn, BreakMode.AvoidColumn },
        };

        /// <summary>
        /// Contains the string-BreakMode (page) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> PageBreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Auto, BreakMode.Auto },
            { Keywords.Always, BreakMode.Always },
            { Keywords.Avoid, BreakMode.Avoid },
            { Keywords.Left, BreakMode.Left },
            { Keywords.Right, BreakMode.Right },
        };

        /// <summary>
        /// Contains the string-BreakMode (inside) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> BreakInsideModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Auto, BreakMode.Auto },
            { Keywords.Avoid, BreakMode.Avoid },
            { Keywords.AvoidPage, BreakMode.AvoidPage },
            { Keywords.AvoidColumn, BreakMode.AvoidColumn },
            { Keywords.AvoidRegion, BreakMode.AvoidRegion },
        };

        /// <summary>
        /// Contains the string-horizontal modes mapping.
        /// </summary>
        public static readonly Dictionary<String, Single> HorizontalModes = new Dictionary<String, Single>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Left, 0f },
            { Keywords.Center, 0.5f },
            { Keywords.Right, 1f },
        };

        /// <summary>
        /// Contains the string-vertical modes mapping.
        /// </summary>
        public static readonly Dictionary<String, Single> VerticalModes = new Dictionary<String, Single>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Top, 0f },
            { Keywords.Center, 0.5f },
            { Keywords.Bottom, 1f },
        };

        /// <summary>
        /// Contains the string-UnicodeMode mapping.
        /// </summary>
        public static readonly Dictionary<String, UnicodeMode> UnicodeModes = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, UnicodeMode.Normal },
            { Keywords.Embed, UnicodeMode.Embed },
            { Keywords.Isolate, UnicodeMode.Isolate },
            { Keywords.IsolateOverride, UnicodeMode.IsolateOverride },
            { Keywords.BidiOverride, UnicodeMode.BidiOverride },
            { Keywords.Plaintext, UnicodeMode.Plaintext },
        };

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, SystemCursor> Cursors = new Dictionary<String, SystemCursor>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Auto, SystemCursor.Auto },
            { Keywords.Default, SystemCursor.Default },
            { Keywords.None, SystemCursor.None },
            { Keywords.ContextMenu, SystemCursor.ContextMenu },
            { Keywords.Help, SystemCursor.Help },
            { Keywords.Pointer, SystemCursor.Pointer },
            { Keywords.Progress, SystemCursor.Progress },
            { Keywords.Wait, SystemCursor.Wait },
            { Keywords.Cell, SystemCursor.Cell },
            { Keywords.Crosshair, SystemCursor.Crosshair },
            { Keywords.Text, SystemCursor.Text },
            { Keywords.VerticalText, SystemCursor.VerticalText },
            { Keywords.Alias, SystemCursor.Alias },
            { Keywords.Copy, SystemCursor.Copy },
            { Keywords.Move, SystemCursor.Move },
            { Keywords.NoDrop, SystemCursor.NoDrop },
            { Keywords.NotAllowed, SystemCursor.NotAllowed },
            { Keywords.EastResize, SystemCursor.EResize },
            { Keywords.NorthResize, SystemCursor.NResize },
            { Keywords.NorthEastResize, SystemCursor.NeResize },
            { Keywords.NorthWestResize, SystemCursor.NwResize },
            { Keywords.SouthResize, SystemCursor.SResize },
            { Keywords.SouthEastResize, SystemCursor.SeResize },
            { Keywords.SouthWestResize, SystemCursor.WResize },
            { Keywords.WestResize, SystemCursor.WResize },
            { Keywords.EastWestResize, SystemCursor.EwResize },
            { Keywords.NorthSouthResize, SystemCursor.NsResize },
            { Keywords.NorthEastSouthWestResize, SystemCursor.NeswResize },
            { Keywords.NorthWestSouthEastResize, SystemCursor.NwseResize },
            { Keywords.ColResize, SystemCursor.ColResize },
            { Keywords.RowResize, SystemCursor.RowResize },
            { Keywords.AllScroll, SystemCursor.AllScroll },
            { Keywords.ZoomIn, SystemCursor.ZoomIn },
            { Keywords.ZoomOut, SystemCursor.ZoomOut },
            { Keywords.Grab, SystemCursor.Grab },
            { Keywords.Grabbing, SystemCursor.Grabbing },
        };

        /// <summary>
        /// Contains the string-PositionMode mapping.
        /// </summary>
        public static readonly Dictionary<String, PositionMode> PositionModes = new Dictionary<String, PositionMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Static, PositionMode.Static },
            { Keywords.Relative, PositionMode.Relative },
            { Keywords.Absolute, PositionMode.Absolute },
            { Keywords.Sticky, PositionMode.Sticky },
            { Keywords.Fixed, PositionMode.Fixed },
        };

        /// <summary>
        /// Contains the string-OverflowMode mapping.
        /// </summary>
        public static readonly Dictionary<String, OverflowMode> OverflowModes = new Dictionary<String, OverflowMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Visible, OverflowMode.Visible },
            { Keywords.Hidden, OverflowMode.Hidden },
            { Keywords.Scroll, OverflowMode.Scroll },
            { Keywords.Auto, OverflowMode.Auto },
        };

        /// <summary>
        /// Contains the string-Floating mapping.
        /// </summary>
        public static readonly Dictionary<String, Floating> FloatingModes = new Dictionary<String, Floating>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, Floating.None },
            { Keywords.Left, Floating.Left },
            { Keywords.Right, Floating.Right },
        };

        /// <summary>
        /// Contains the string-DisplayMode mapping.
        /// </summary>
        public static readonly Dictionary<String, DisplayMode> DisplayModes = new Dictionary<String, DisplayMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, DisplayMode.None },
            { Keywords.Inline, DisplayMode.Inline },
            { Keywords.Block, DisplayMode.Block },
            { Keywords.InlineBlock, DisplayMode.InlineBlock },
            { Keywords.ListItem, DisplayMode.ListItem },
            { Keywords.InlineTable, DisplayMode.InlineTable },
            { Keywords.Table, DisplayMode.Table },
            { Keywords.TableCaption, DisplayMode.TableCaption },
            { Keywords.TableCell, DisplayMode.TableCell },
            { Keywords.TableColumn, DisplayMode.TableColumn },
            { Keywords.TableColumnGroup, DisplayMode.TableColumnGroup },
            { Keywords.TableFooterGroup, DisplayMode.TableFooterGroup },
            { Keywords.TableHeaderGroup, DisplayMode.TableHeaderGroup },
            { Keywords.TableRow, DisplayMode.TableRow },
            { Keywords.TableRowGroup, DisplayMode.TableRowGroup },
            { Keywords.Flex, DisplayMode.Flex },
            { Keywords.InlineFlex, DisplayMode.InlineFlex },
            { Keywords.Grid, DisplayMode.Grid },
            { Keywords.InlineGrid, DisplayMode.InlineGrid },
        };

        /// <summary>
        /// Contains the string-ClearMode mapping.
        /// </summary>
        public static readonly Dictionary<String, ClearMode> ClearModes = new Dictionary<String, ClearMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, ClearMode.None },
            { Keywords.Left, ClearMode.Left },
            { Keywords.Right, ClearMode.Right },
            { Keywords.Both, ClearMode.Both },
        };

        /// <summary>
        /// Contains the string-BackgroundRepeat mapping.
        /// </summary>
        public static readonly Dictionary<String, BackgroundRepeat> BackgroundRepeats = new Dictionary<String, BackgroundRepeat>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.NoRepeat, BackgroundRepeat.NoRepeat },
            { Keywords.Repeat, BackgroundRepeat.Repeat },
            { Keywords.Round, BackgroundRepeat.Round },
            { Keywords.Space, BackgroundRepeat.Space },
        };

        /// <summary>
        /// Contains the string-BlendMode mapping.
        /// </summary>
        public static readonly Dictionary<String, BlendMode> BlendModes = new Dictionary<String, BlendMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Color, BlendMode.Color },
            { Keywords.ColorBurn, BlendMode.ColorBurn },
            { Keywords.ColorDodge, BlendMode.ColorDodge },
            { Keywords.Darken, BlendMode.Darken },
            { Keywords.Difference, BlendMode.Difference },
            { Keywords.Exclusion, BlendMode.Exclusion },
            { Keywords.HardLight, BlendMode.HardLight },
            { Keywords.Hue, BlendMode.Hue },
            { Keywords.Lighten, BlendMode.Lighten },
            { Keywords.Luminosity, BlendMode.Luminosity },
            { Keywords.Multiply, BlendMode.Multiply },
            { Keywords.Normal, BlendMode.Normal },
            { Keywords.Overlay, BlendMode.Overlay },
            { Keywords.Saturation, BlendMode.Saturation },
            { Keywords.Screen, BlendMode.Screen },
            { Keywords.SoftLight, BlendMode.SoftLight },
        };

        /// <summary>
        /// Contains the string-UpdateFrequency mapping.
        /// </summary>
        public static readonly Dictionary<String, UpdateFrequency> UpdateFrequencies = new Dictionary<String, UpdateFrequency>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, UpdateFrequency.None },
            { Keywords.Slow, UpdateFrequency.Slow },
            { Keywords.Normal, UpdateFrequency.Normal },
        };

        /// <summary>
        /// Contains the string-ScriptingState mapping.
        /// </summary>
        public static readonly Dictionary<String, ScriptingState> ScriptingStates = new Dictionary<String, ScriptingState>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, ScriptingState.None },
            { Keywords.InitialOnly, ScriptingState.InitialOnly },
            { Keywords.Enabled, ScriptingState.Enabled },
        };

        /// <summary>
        /// Contains the string-PointerAccuracy mapping.
        /// </summary>
        public static readonly Dictionary<String, PointerAccuracy> PointerAccuracies = new Dictionary<String, PointerAccuracy>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, PointerAccuracy.None },
            { Keywords.Coarse, PointerAccuracy.Coarse },
            { Keywords.Fine, PointerAccuracy.Fine },
        };

        /// <summary>
        /// Contains the string-HoverAbility mapping.
        /// </summary>
        public static readonly Dictionary<String, HoverAbility> HoverAbilities = new Dictionary<String, HoverAbility>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, HoverAbility.None },
            { Keywords.OnDemand, HoverAbility.OnDemand },
            { Keywords.Hover, HoverAbility.Hover },
        };

        /// <summary>
        /// Contains the string-SizeMode mapping.
        /// </summary>
        public static readonly Dictionary<String, RadialGradient.SizeMode> RadialGradientSizeModes = new Dictionary<String, RadialGradient.SizeMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.ClosestSide, RadialGradient.SizeMode.ClosestSide },
            { Keywords.FarthestSide, RadialGradient.SizeMode.FarthestSide },
            { Keywords.ClosestCorner, RadialGradient.SizeMode.ClosestCorner },
            { Keywords.FarthestCorner, RadialGradient.SizeMode.FarthestCorner },
        };

        /// <summary>
        /// Contains the string-ObjectFitting mapping.
        /// </summary>
        public static readonly Dictionary<String, ObjectFitting> ObjectFittings = new Dictionary<String, ObjectFitting>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.None, ObjectFitting.None },
            { Keywords.Cover, ObjectFitting.Cover },
            { Keywords.Contain, ObjectFitting.Contain },
            { Keywords.Fill, ObjectFitting.Fill },
            { Keywords.ScaleDown, ObjectFitting.ScaleDown },
        };

        /// <summary>
        /// Contains the string-FontWeight mapping.
        /// </summary>
        public static readonly Dictionary<String, FontWeight> FontWeights = new Dictionary<String, FontWeight>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, FontWeight.Normal },
            { Keywords.Bold, FontWeight.Bold },
            { Keywords.Bolder, FontWeight.Bolder },
            { Keywords.Lighter, FontWeight.Lighter },
        };

        /// <summary>
        /// Contains the string-SystemFont mapping.
        /// </summary>
        public static readonly Dictionary<String, SystemFont> SystemFonts = new Dictionary<String, SystemFont>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Caption, SystemFont.Caption },
            { Keywords.Icon, SystemFont.Icon },
            { Keywords.Menu, SystemFont.Menu },
            { Keywords.MessageBox, SystemFont.MessageBox },
            { Keywords.SmallCaption, SystemFont.SmallCaption },
            { Keywords.StatusBar, SystemFont.StatusBar },
        };

		/// <summary>
		/// Contains the string-StrokeLinecap mapping.
		/// </summary>
		public static readonly Dictionary<String, StrokeLinecap> StrokeLinecaps = new Dictionary<String, StrokeLinecap>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Butt, StrokeLinecap.Butt },
			{ Keywords.Round, StrokeLinecap.Round },
			{ Keywords.Square, StrokeLinecap.Square }
		};

		/// <summary>
		/// Contains the string-StrokeLinejoin mapping.
		/// </summary>
		public static readonly Dictionary<String, StrokeLinejoin> StrokeLinejoins = new Dictionary<String, StrokeLinejoin>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Miter, StrokeLinejoin.Miter },
			{ Keywords.Round, StrokeLinejoin.Round },
			{ Keywords.Bevel, StrokeLinejoin.Bevel }
		};

		/// <summary>
		/// Contains the string-WordBreak mapping.
		/// </summary>
		public static readonly Dictionary<String, WordBreak> WordBreaks = new Dictionary<String, WordBreak>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Normal, WordBreak.Normal },
			{ Keywords.BreakAll, WordBreak.BreakAll },
			{ Keywords.KeepAll, WordBreak.KeepAll }
		};

		/// <summary>
		/// Contains the string-WordBreak mapping.
		/// </summary>
		public static readonly Dictionary<String, OverflowWrap> OverflowWraps = new Dictionary<String, OverflowWrap>(StringComparer.OrdinalIgnoreCase)
		{
			{ Keywords.Normal, OverflowWrap.Normal },
			{ Keywords.BreakWord, OverflowWrap.BreakWord },
		};
	}
}
