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
        #region Dictionaries

        /// <summary>
        /// Contains the string-Whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, Whitespace> WhitespaceModes = new Dictionary<String, Whitespace>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-TextTransform mapping.
        /// </summary>
        public static readonly Dictionary<String, TextTransform> TextTransforms = new Dictionary<String, TextTransform>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-HorizontalAlignment mapping.
        /// </summary>
        public static readonly Dictionary<String, HorizontalAlignment> HorizontalAlignments = new Dictionary<String, HorizontalAlignment>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-VerticalAlignment mapping.
        /// </summary>
        public static readonly Dictionary<String, VerticalAlignment> VerticalAlignments = new Dictionary<String, VerticalAlignment>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-LineStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, LineStyle> LineStyles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BoxModel mapping.
        /// </summary>
        public static readonly Dictionary<String, BoxModel> BoxModels = new Dictionary<String, BoxModel>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-TimingFunction mapping.
        /// </summary>
        public static readonly Dictionary<String, ITimingFunction> TimingFunctions = new Dictionary<String, ITimingFunction>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-AnimationFillStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, AnimationFillStyle> AnimationFillStyles = new Dictionary<String, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-AnimationDirection mapping.
        /// </summary>
        public static readonly Dictionary<String, AnimationDirection> AnimationDirections = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-Visibility mapping.
        /// </summary>
        public static readonly Dictionary<String, Visibility> Visibilities = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-ListStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, ListStyle> ListStyles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-ListPosition mapping.
        /// </summary>
        public static readonly Dictionary<String, ListPosition> ListPositions = new Dictionary<String, ListPosition>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, FontSize> FontSizes = new Dictionary<String, FontSize>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-TextDecorationStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, TextDecorationStyle> TextDecorationStyles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-TextDecorationLine mapping.
        /// </summary>
        public static readonly Dictionary<String, TextDecorationLine> TextDecorationLines = new Dictionary<String, TextDecorationLine>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BorderRepeat mapping.
        /// </summary>
        public static readonly Dictionary<String, BorderRepeat> BorderRepeatModes = new Dictionary<String, BorderRepeat>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, String> DefaultFontFamilies = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BackgroundAttachment mapping.
        /// </summary>
        public static readonly Dictionary<String, BackgroundAttachment> BackgroundAttachments = new Dictionary<String, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-FontStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, FontStyle> FontStyles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-FontStretch mapping.
        /// </summary>
        public static readonly Dictionary<String, FontStretch> FontStretches = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BreakMode (general) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> BreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BreakMode (page) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> PageBreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BreakMode (inside) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> BreakInsideModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-horizontal modes mapping.
        /// </summary>
        public static readonly Dictionary<String, Single> HorizontalModes = new Dictionary<String, Single>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-vertical modes mapping.
        /// </summary>
        public static readonly Dictionary<String, Single> VerticalModes = new Dictionary<String, Single>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-UnicodeMode mapping.
        /// </summary>
        public static readonly Dictionary<String, UnicodeMode> UnicodeModes = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, SystemCursor> Cursors = new Dictionary<String, SystemCursor>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-PositionMode mapping.
        /// </summary>
        public static readonly Dictionary<String, PositionMode> PositionModes = new Dictionary<String, PositionMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-OverflowMode mapping.
        /// </summary>
        public static readonly Dictionary<String, OverflowMode> OverflowModes = new Dictionary<String, OverflowMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-Floating mapping.
        /// </summary>
        public static readonly Dictionary<String, Floating> FloatingModes = new Dictionary<String, Floating>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-DisplayMode mapping.
        /// </summary>
        public static readonly Dictionary<String, DisplayMode> DisplayModes = new Dictionary<String, DisplayMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-ClearMode mapping.
        /// </summary>
        public static readonly Dictionary<String, ClearMode> ClearModes = new Dictionary<String, ClearMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BackgroundRepeat mapping.
        /// </summary>
        public static readonly Dictionary<String, BackgroundRepeat> BackgroundRepeats = new Dictionary<String, BackgroundRepeat>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-BlendMode mapping.
        /// </summary>
        public static readonly Dictionary<String, BlendMode> BlendModes = new Dictionary<String, BlendMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-UpdateFrequency mapping.
        /// </summary>
        public static readonly Dictionary<String, UpdateFrequency> UpdateFrequencies = new Dictionary<String, UpdateFrequency>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-ScriptingState mapping.
        /// </summary>
        public static readonly Dictionary<String, ScriptingState> ScriptingStates = new Dictionary<String, ScriptingState>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-PointerAccuracy mapping.
        /// </summary>
        public static readonly Dictionary<String, PointerAccuracy> PointerAccuracies = new Dictionary<String, PointerAccuracy>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-HoverAbility mapping.
        /// </summary>
        public static readonly Dictionary<String, HoverAbility> HoverAbilities = new Dictionary<String, HoverAbility>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-SizeMode mapping.
        /// </summary>
        public static readonly Dictionary<String, RadialGradient.SizeMode> RadialGradientSizeModes = new Dictionary<String, RadialGradient.SizeMode>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Contains the string-ObjectFitting mapping.
        /// </summary>
        public static readonly Dictionary<String, ObjectFitting> ObjectFittings = new Dictionary<String, ObjectFitting>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Initial Population

        static Map()
        {
            LineStyles.Add(Keywords.None, LineStyle.None);
            LineStyles.Add(Keywords.Solid, LineStyle.Solid);
            LineStyles.Add(Keywords.Double, LineStyle.Double);
            LineStyles.Add(Keywords.Dotted, LineStyle.Dotted);
            LineStyles.Add(Keywords.Dashed, LineStyle.Dashed);
            LineStyles.Add(Keywords.Inset, LineStyle.Inset);
            LineStyles.Add(Keywords.Outset, LineStyle.Outset);
            LineStyles.Add(Keywords.Ridge, LineStyle.Ridge);
            LineStyles.Add(Keywords.Groove, LineStyle.Groove);
            LineStyles.Add(Keywords.Hidden, LineStyle.Hidden);

            BoxModels.Add(Keywords.BorderBox, BoxModel.BorderBox);
            BoxModels.Add(Keywords.PaddingBox, BoxModel.PaddingBox);
            BoxModels.Add(Keywords.ContentBox, BoxModel.ContentBox);

            TimingFunctions.Add(Keywords.Ease, new CubicBezierTimingFunction(0.25f, 0.1f, 0.25f, 1f));
            TimingFunctions.Add(Keywords.EaseIn, new CubicBezierTimingFunction(0.42f, 0f, 1f, 1f));
            TimingFunctions.Add(Keywords.EaseOut, new CubicBezierTimingFunction(0f, 0f, 0.58f, 1f));
            TimingFunctions.Add(Keywords.EaseInOut, new CubicBezierTimingFunction(0.42f, 0f, 0.58f, 1f));
            TimingFunctions.Add(Keywords.Linear, new CubicBezierTimingFunction(0f, 0f, 1f, 1f));
            TimingFunctions.Add(Keywords.StepStart, new StepsTimingFunction(1, true));
            TimingFunctions.Add(Keywords.StepEnd, new StepsTimingFunction(1, false));

            AnimationFillStyles.Add(Keywords.None, AnimationFillStyle.None);
            AnimationFillStyles.Add(Keywords.Forwards, AnimationFillStyle.Forwards);
            AnimationFillStyles.Add(Keywords.Backwards, AnimationFillStyle.Backwards);
            AnimationFillStyles.Add(Keywords.Both, AnimationFillStyle.Both);

            AnimationDirections.Add(Keywords.Normal, AnimationDirection.Normal);
            AnimationDirections.Add(Keywords.Reverse, AnimationDirection.Reverse);
            AnimationDirections.Add(Keywords.Alternate, AnimationDirection.Alternate);
            AnimationDirections.Add(Keywords.AlternateReverse, AnimationDirection.AlternateReverse);

            Visibilities.Add(Keywords.Visible, Visibility.Visible);
            Visibilities.Add(Keywords.Hidden, Visibility.Hidden);
            Visibilities.Add(Keywords.Collapse, Visibility.Collapse);

            ListStyles.Add(Keywords.Disc, ListStyle.Disc);
            ListStyles.Add(Keywords.Circle, ListStyle.Circle);
            ListStyles.Add(Keywords.Square, ListStyle.Square);
            ListStyles.Add(Keywords.Decimal, ListStyle.Decimal);
            ListStyles.Add(Keywords.DecimalLeadingZero, ListStyle.DecimalLeadingZero);
            ListStyles.Add(Keywords.LowerRoman, ListStyle.LowerRoman);
            ListStyles.Add(Keywords.UpperRoman, ListStyle.UpperRoman);
            ListStyles.Add(Keywords.LowerGreek, ListStyle.LowerGreek);
            ListStyles.Add(Keywords.LowerLatin, ListStyle.LowerLatin);
            ListStyles.Add(Keywords.UpperLatin, ListStyle.UpperLatin);
            ListStyles.Add(Keywords.Armenian, ListStyle.Armenian);
            ListStyles.Add(Keywords.Georgian, ListStyle.Georgian);
            ListStyles.Add(Keywords.LowerAlpha, ListStyle.LowerLatin);
            ListStyles.Add(Keywords.UpperAlpha, ListStyle.UpperLatin);
            ListStyles.Add(Keywords.None, ListStyle.None);

            ListPositions.Add(Keywords.Inside, ListPosition.Inside);
            ListPositions.Add(Keywords.Outside, ListPosition.Outside);

            FontSizes.Add(Keywords.XxSmall, FontSize.Tiny);
            FontSizes.Add(Keywords.XSmall, FontSize.Little);
            FontSizes.Add(Keywords.Small, FontSize.Small);
            FontSizes.Add(Keywords.Medium, FontSize.Medium);
            FontSizes.Add(Keywords.Large, FontSize.Large);
            FontSizes.Add(Keywords.XLarge, FontSize.Big);
            FontSizes.Add(Keywords.XxLarge, FontSize.Huge);
            FontSizes.Add(Keywords.Larger, FontSize.Smaller);
            FontSizes.Add(Keywords.Smaller, FontSize.Larger);

            TextDecorationStyles.Add(Keywords.Solid, TextDecorationStyle.Solid);
            TextDecorationStyles.Add(Keywords.Double, TextDecorationStyle.Double);
            TextDecorationStyles.Add(Keywords.Dotted, TextDecorationStyle.Dotted);
            TextDecorationStyles.Add(Keywords.Dashed, TextDecorationStyle.Dashed);
            TextDecorationStyles.Add(Keywords.Wavy, TextDecorationStyle.Wavy);

            TextDecorationLines.Add(Keywords.Underline, TextDecorationLine.Underline);
            TextDecorationLines.Add(Keywords.Overline, TextDecorationLine.Overline);
            TextDecorationLines.Add(Keywords.LineThrough, TextDecorationLine.LineThrough);
            TextDecorationLines.Add(Keywords.Blink, TextDecorationLine.Blink);

            BorderRepeatModes.Add(Keywords.Stretch, BorderRepeat.Stretch);
            BorderRepeatModes.Add(Keywords.Repeat, BorderRepeat.Repeat);
            BorderRepeatModes.Add(Keywords.Round, BorderRepeat.Round);

            DefaultFontFamilies.Add(Keywords.Serif, "Times New Roman");
            DefaultFontFamilies.Add(Keywords.SansSerif, "Arial");
            DefaultFontFamilies.Add(Keywords.Monospace, "Consolas");
            DefaultFontFamilies.Add(Keywords.Cursive, "Cursive");
            DefaultFontFamilies.Add(Keywords.Fantasy, "Comic Sans");

            BackgroundAttachments.Add(Keywords.Fixed, BackgroundAttachment.Fixed);
            BackgroundAttachments.Add(Keywords.Local, BackgroundAttachment.Local);
            BackgroundAttachments.Add(Keywords.Scroll, BackgroundAttachment.Scroll);

            FontStyles.Add(Keywords.Normal, FontStyle.Normal);
            FontStyles.Add(Keywords.Italic, FontStyle.Italic);
            FontStyles.Add(Keywords.Oblique, FontStyle.Oblique);

            FontStretches.Add(Keywords.Normal, FontStretch.Normal);
            FontStretches.Add(Keywords.UltraCondensed, FontStretch.UltraCondensed);
            FontStretches.Add(Keywords.ExtraCondensed, FontStretch.ExtraCondensed);
            FontStretches.Add(Keywords.Condensed, FontStretch.Condensed);
            FontStretches.Add(Keywords.SemiCondensed, FontStretch.SemiCondensed);
            FontStretches.Add(Keywords.SemiExpanded, FontStretch.SemiExpanded);
            FontStretches.Add(Keywords.Expanded, FontStretch.Expanded);
            FontStretches.Add(Keywords.ExtraExpanded, FontStretch.ExtraExpanded);
            FontStretches.Add(Keywords.UltraExpanded, FontStretch.UltraExpanded);

            BreakModes.Add(Keywords.Auto, BreakMode.Auto);
            BreakModes.Add(Keywords.Always, BreakMode.Always);
            BreakModes.Add(Keywords.Avoid, BreakMode.Avoid);
            BreakModes.Add(Keywords.Left, BreakMode.Left);
            BreakModes.Add(Keywords.Right, BreakMode.Right);
            BreakModes.Add(Keywords.Page, BreakMode.Page);
            BreakModes.Add(Keywords.Column, BreakMode.Column);
            BreakModes.Add(Keywords.AvoidPage, BreakMode.AvoidPage);
            BreakModes.Add(Keywords.AvoidColumn, BreakMode.AvoidColumn);

            PageBreakModes.Add(Keywords.Auto, BreakMode.Auto);
            PageBreakModes.Add(Keywords.Always, BreakMode.Always);
            PageBreakModes.Add(Keywords.Avoid, BreakMode.Avoid);
            PageBreakModes.Add(Keywords.Left, BreakMode.Left);
            PageBreakModes.Add(Keywords.Right, BreakMode.Right);

            BreakInsideModes.Add(Keywords.Auto, BreakMode.Auto);
            BreakInsideModes.Add(Keywords.Avoid, BreakMode.Avoid);
            BreakInsideModes.Add(Keywords.AvoidPage, BreakMode.AvoidPage);
            BreakInsideModes.Add(Keywords.AvoidColumn, BreakMode.AvoidColumn);
            BreakInsideModes.Add(Keywords.AvoidRegion, BreakMode.AvoidRegion);

            HorizontalModes.Add(Keywords.Left, 0f);
            HorizontalModes.Add(Keywords.Center, 0.5f);
            HorizontalModes.Add(Keywords.Right, 1f);

            VerticalModes.Add(Keywords.Top, 0f);
            VerticalModes.Add(Keywords.Center, 0.5f);
            VerticalModes.Add(Keywords.Bottom, 1f);

            UnicodeModes.Add(Keywords.Normal, UnicodeMode.Normal);
            UnicodeModes.Add(Keywords.Embed, UnicodeMode.Embed);
            UnicodeModes.Add(Keywords.Isolate, UnicodeMode.Isolate);
            UnicodeModes.Add(Keywords.IsolateOverride, UnicodeMode.IsolateOverride);
            UnicodeModes.Add(Keywords.BidiOverride, UnicodeMode.BidiOverride);
            UnicodeModes.Add(Keywords.Plaintext, UnicodeMode.Plaintext);

            Cursors.Add(Keywords.Auto, SystemCursor.Auto);
            Cursors.Add(Keywords.Default, SystemCursor.Default);
            Cursors.Add(Keywords.None, SystemCursor.None);
            Cursors.Add(Keywords.ContextMenu, SystemCursor.ContextMenu);
            Cursors.Add(Keywords.Help, SystemCursor.Help);
            Cursors.Add(Keywords.Pointer, SystemCursor.Pointer);
            Cursors.Add(Keywords.Progress, SystemCursor.Progress);
            Cursors.Add(Keywords.Wait, SystemCursor.Wait);
            Cursors.Add(Keywords.Cell, SystemCursor.Cell);
            Cursors.Add(Keywords.Crosshair, SystemCursor.Crosshair);
            Cursors.Add(Keywords.Text, SystemCursor.Text);
            Cursors.Add(Keywords.VerticalText, SystemCursor.VerticalText);
            Cursors.Add(Keywords.Alias, SystemCursor.Alias);
            Cursors.Add(Keywords.Copy, SystemCursor.Copy);
            Cursors.Add(Keywords.Move, SystemCursor.Move);
            Cursors.Add(Keywords.NoDrop, SystemCursor.NoDrop);
            Cursors.Add(Keywords.NotAllowed, SystemCursor.NotAllowed);
            Cursors.Add(Keywords.EastResize, SystemCursor.EResize);
            Cursors.Add(Keywords.NorthResize, SystemCursor.NResize);
            Cursors.Add(Keywords.NorthEastResize, SystemCursor.NeResize);
            Cursors.Add(Keywords.NorthWestResize, SystemCursor.NwResize);
            Cursors.Add(Keywords.SouthResize, SystemCursor.SResize);
            Cursors.Add(Keywords.SouthEastResize, SystemCursor.SeResize);
            Cursors.Add(Keywords.SouthWestResize, SystemCursor.WResize);
            Cursors.Add(Keywords.WestResize, SystemCursor.WResize);
            Cursors.Add(Keywords.EastWestResize, SystemCursor.EwResize);
            Cursors.Add(Keywords.NorthSouthResize, SystemCursor.NsResize);
            Cursors.Add(Keywords.NorthEastSouthWestResize, SystemCursor.NeswResize);
            Cursors.Add(Keywords.NorthWestSouthEastResize, SystemCursor.NwseResize);
            Cursors.Add(Keywords.ColResize, SystemCursor.ColResize);
            Cursors.Add(Keywords.RowResize, SystemCursor.RowResize);
            Cursors.Add(Keywords.AllScroll, SystemCursor.AllScroll);
            Cursors.Add(Keywords.ZoomIn, SystemCursor.ZoomIn);
            Cursors.Add(Keywords.ZoomOut, SystemCursor.ZoomOut);
            Cursors.Add(Keywords.Grab, SystemCursor.Grab);
            Cursors.Add(Keywords.Grabbing, SystemCursor.Grabbing);

            VerticalAlignments.Add(Keywords.Baseline, VerticalAlignment.Baseline);
            VerticalAlignments.Add(Keywords.Sub, VerticalAlignment.Sub);
            VerticalAlignments.Add(Keywords.Super, VerticalAlignment.Super);
            VerticalAlignments.Add(Keywords.TextTop, VerticalAlignment.TextTop);
            VerticalAlignments.Add(Keywords.TextBottom, VerticalAlignment.TextBottom);
            VerticalAlignments.Add(Keywords.Middle, VerticalAlignment.Middle);
            VerticalAlignments.Add(Keywords.Top, VerticalAlignment.Top);
            VerticalAlignments.Add(Keywords.Bottom, VerticalAlignment.Bottom);

            TextTransforms.Add(Keywords.None, TextTransform.None);
            TextTransforms.Add(Keywords.Capitalize, TextTransform.Capitalize);
            TextTransforms.Add(Keywords.Uppercase, TextTransform.Uppercase);
            TextTransforms.Add(Keywords.Lowercase, TextTransform.Lowercase);
            TextTransforms.Add(Keywords.FullWidth, TextTransform.FullWidth);

            WhitespaceModes.Add(Keywords.Normal, Whitespace.Normal);
            WhitespaceModes.Add(Keywords.Pre, Whitespace.Pre);
            WhitespaceModes.Add(Keywords.Nowrap, Whitespace.NoWrap);
            WhitespaceModes.Add(Keywords.PreWrap, Whitespace.PreWrap);
            WhitespaceModes.Add(Keywords.PreLine, Whitespace.PreLine);

            HorizontalAlignments.Add(Keywords.Left, HorizontalAlignment.Left);
            HorizontalAlignments.Add(Keywords.Right, HorizontalAlignment.Right);
            HorizontalAlignments.Add(Keywords.Center, HorizontalAlignment.Center);
            HorizontalAlignments.Add(Keywords.Justify, HorizontalAlignment.Justify);

            PositionModes.Add(Keywords.Static, PositionMode.Static);
            PositionModes.Add(Keywords.Relative, PositionMode.Relative);
            PositionModes.Add(Keywords.Absolute, PositionMode.Absolute);
            PositionModes.Add(Keywords.Sticky, PositionMode.Sticky);
            PositionModes.Add(Keywords.Fixed, PositionMode.Fixed);

            OverflowModes.Add(Keywords.Visible, OverflowMode.Visible);
            OverflowModes.Add(Keywords.Hidden, OverflowMode.Hidden);
            OverflowModes.Add(Keywords.Scroll, OverflowMode.Scroll);
            OverflowModes.Add(Keywords.Auto, OverflowMode.Auto);

            FloatingModes.Add(Keywords.None, Floating.None);
            FloatingModes.Add(Keywords.Left, Floating.Left);
            FloatingModes.Add(Keywords.Right, Floating.Right);

            DisplayModes.Add(Keywords.None, DisplayMode.None);
            DisplayModes.Add(Keywords.Inline, DisplayMode.Inline);
            DisplayModes.Add(Keywords.Block, DisplayMode.Block);
            DisplayModes.Add(Keywords.InlineBlock, DisplayMode.InlineBlock);
            DisplayModes.Add(Keywords.ListItem, DisplayMode.ListItem);
            DisplayModes.Add(Keywords.InlineTable, DisplayMode.InlineTable);
            DisplayModes.Add(Keywords.Table, DisplayMode.Table);
            DisplayModes.Add(Keywords.TableCaption, DisplayMode.TableCaption);
            DisplayModes.Add(Keywords.TableCell, DisplayMode.TableCell);
            DisplayModes.Add(Keywords.TableColumn, DisplayMode.TableColumn);
            DisplayModes.Add(Keywords.TableColumnGroup, DisplayMode.TableColumnGroup);
            DisplayModes.Add(Keywords.TableFooterGroup, DisplayMode.TableFooterGroup);
            DisplayModes.Add(Keywords.TableHeaderGroup, DisplayMode.TableHeaderGroup);
            DisplayModes.Add(Keywords.TableRow, DisplayMode.TableRow);
            DisplayModes.Add(Keywords.TableRowGroup, DisplayMode.TableRowGroup);
            DisplayModes.Add(Keywords.Flex, DisplayMode.Flex);
            DisplayModes.Add(Keywords.InlineFlex, DisplayMode.InlineFlex);
            DisplayModes.Add(Keywords.Grid, DisplayMode.Grid);
            DisplayModes.Add(Keywords.InlineGrid, DisplayMode.InlineGrid);

            ClearModes.Add(Keywords.None, ClearMode.None);
            ClearModes.Add(Keywords.Left, ClearMode.Left);
            ClearModes.Add(Keywords.Right, ClearMode.Right);
            ClearModes.Add(Keywords.Both, ClearMode.Both);

            BackgroundRepeats.Add(Keywords.NoRepeat, BackgroundRepeat.NoRepeat);
            BackgroundRepeats.Add(Keywords.Repeat, BackgroundRepeat.Repeat);
            BackgroundRepeats.Add(Keywords.Round, BackgroundRepeat.Round);
            BackgroundRepeats.Add(Keywords.Space, BackgroundRepeat.Space);

            BlendModes.Add(Keywords.Color, BlendMode.Color);
            BlendModes.Add(Keywords.ColorBurn, BlendMode.ColorBurn);
            BlendModes.Add(Keywords.ColorDodge, BlendMode.ColorDodge);
            BlendModes.Add(Keywords.Darken, BlendMode.Darken);
            BlendModes.Add(Keywords.Difference, BlendMode.Difference);
            BlendModes.Add(Keywords.Exclusion, BlendMode.Exclusion);
            BlendModes.Add(Keywords.HardLight, BlendMode.HardLight);
            BlendModes.Add(Keywords.Hue, BlendMode.Hue);
            BlendModes.Add(Keywords.Lighten, BlendMode.Lighten);
            BlendModes.Add(Keywords.Luminosity, BlendMode.Luminosity);
            BlendModes.Add(Keywords.Multiply, BlendMode.Multiply);
            BlendModes.Add(Keywords.Normal, BlendMode.Normal);
            BlendModes.Add(Keywords.Overlay, BlendMode.Overlay);
            BlendModes.Add(Keywords.Saturation, BlendMode.Saturation);
            BlendModes.Add(Keywords.Screen, BlendMode.Screen);
            BlendModes.Add(Keywords.SoftLight, BlendMode.SoftLight);

            UpdateFrequencies.Add(Keywords.None, UpdateFrequency.None);
            UpdateFrequencies.Add(Keywords.Slow, UpdateFrequency.Slow);
            UpdateFrequencies.Add(Keywords.Normal, UpdateFrequency.Normal);

            ScriptingStates.Add(Keywords.None, ScriptingState.None);
            ScriptingStates.Add(Keywords.InitialOnly, ScriptingState.InitialOnly);
            ScriptingStates.Add(Keywords.Enabled, ScriptingState.Enabled);

            PointerAccuracies.Add(Keywords.None, PointerAccuracy.None);
            PointerAccuracies.Add(Keywords.Coarse, PointerAccuracy.Coarse);
            PointerAccuracies.Add(Keywords.Fine, PointerAccuracy.Fine);

            HoverAbilities.Add(Keywords.None, HoverAbility.None);
            HoverAbilities.Add(Keywords.OnDemand, HoverAbility.OnDemand);
            HoverAbilities.Add(Keywords.Hover, HoverAbility.Hover);

            RadialGradientSizeModes.Add(Keywords.ClosestSide, RadialGradient.SizeMode.ClosestSide);
            RadialGradientSizeModes.Add(Keywords.FarthestSide, RadialGradient.SizeMode.FarthestSide);
            RadialGradientSizeModes.Add(Keywords.ClosestCorner, RadialGradient.SizeMode.ClosestCorner);
            RadialGradientSizeModes.Add(Keywords.FarthestCorner, RadialGradient.SizeMode.FarthestCorner);

            ObjectFittings.Add(Keywords.None, ObjectFitting.None);
            ObjectFittings.Add(Keywords.Cover, ObjectFitting.Cover);
            ObjectFittings.Add(Keywords.Contain, ObjectFitting.Contain);
            ObjectFittings.Add(Keywords.Fill, ObjectFitting.Fill);
            ObjectFittings.Add(Keywords.ScaleDown, ObjectFitting.ScaleDown);
        }

        #endregion
    }
}
