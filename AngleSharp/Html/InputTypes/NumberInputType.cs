namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class NumberInputType : BaseInputType
    {
        #region ctor

        public NumberInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override Double? ConvertToNumber(String value)
        {
            return ConvertFromNumber(value);
        }

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var num = ConvertToNumber(value);
            state.Reset();

            if (num.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertToNumber(input.Minimum);
                var max = ConvertToNumber(input.Maximum);

                if (min.HasValue)
                    state.IsRangeUnderflow = num < min.Value;

                if (max.HasValue)
                    state.IsRangeOverflow = num > max.Value;

                state.IsStepMismatch = step != 0.0 && GetStepBase(input) % step != 0.0;
            }
            else
                state.IsValueMissing = input.IsRequired;
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var num = ConvertFromNumber(input.Value);

            if (num.HasValue)
            {
                var res = num.Value + GetStep(input) * n;
                var min = ConvertFromNumber(input.Minimum);
                var max = ConvertFromNumber(input.Maximum);

                if ((min.HasValue == false || min.Value <= res) && (max.HasValue == false || max.Value >= res))
                    input.ValueAsNumber = res;
            }
        }

        #endregion

        #region Step

        protected override Double GetDefaultStepBase(IHtmlInputElement input)
        {
            return 0.0;
        }

        protected override Double GetDefaultStep(IHtmlInputElement input)
        {
            return 1.0;
        }

        protected override Double GetStepScaleFactor(IHtmlInputElement input)
        {
            return 1.0;
        }

        #endregion
    }
}
