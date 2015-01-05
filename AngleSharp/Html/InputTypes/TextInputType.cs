namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class TextInputType : BaseInputType
    {
        #region ctor

        public TextInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(ValidityState state)
        {
            var value = Input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(Input.Pattern, value);
        }

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            base.ConstructDataSet(dataSet);
            var dirname = Input.GetAttribute(AttributeNames.DirName);

            if (!String.IsNullOrEmpty(dirname))
                dataSet.Append(dirname, Input.Direction.ToLowerInvariant(), "Direction");
        }

        #endregion
    }
}
