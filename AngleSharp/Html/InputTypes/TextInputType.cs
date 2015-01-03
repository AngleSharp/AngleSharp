namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class TextInputType : BaseInputType
    {
        #region ctor

        public TextInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(input.Pattern, value);
        }

        public override void ConstructDataSet(IHtmlInputElement input, FormDataSet dataSet)
        {
            base.ConstructDataSet(input, dataSet);
            var dirname = input.GetAttribute(AttributeNames.DirName);

            if (!String.IsNullOrEmpty(dirname))
                dataSet.Append(dirname, input.Direction.ToLowerInvariant(), "Direction");
        }

        #endregion
    }
}
