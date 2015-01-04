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
                var min = ConvertToNumber(input.Minimum);
                var max = ConvertToNumber(input.Maximum);

                state.IsRangeUnderflow = min.HasValue && num < min.Value;
                state.IsRangeOverflow = max.HasValue && num > max.Value;
                state.IsValueMissing = false;
                state.IsStepMismatch = IsStepMismatch(input);
            }
            else
            {
                state.IsRangeUnderflow = false;
                state.IsRangeOverflow = false;
                state.IsValueMissing = input.IsRequired;
                state.IsStepMismatch = false;
            }
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
