namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
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

        public override ValidationErrors Check(IValidityState current)
        {
            var result = GetErrorsFrom(current);

            if (IsInvalidPattern(Input.Pattern, Input.Value ?? String.Empty))
            {
                result ^= ValidationErrors.PatternMismatch;
            }

            return result;
        }

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            base.ConstructDataSet(dataSet);
            var dirname = Input.GetAttribute(null, AttributeNames.DirName);

            if (!String.IsNullOrEmpty(dirname))
            {
                dataSet.Append(dirname, Input.Direction.ToLowerInvariant(), "Direction");
            }
        }

        #endregion
    }
}
