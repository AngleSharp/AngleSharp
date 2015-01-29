namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using System;
    using System.Globalization;

    class NumberInputType : BaseInputType
    {
        #region ctor

        public NumberInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override Double? ConvertToNumber(String value)
        {
            return ToNumber(value);
        }

        public override String ConvertFromNumber(Double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public override void Check(ValidityState state)
        {
            var value = Input.Value;
            var num = ConvertToNumber(value);
            state.Reset();

            if (num.HasValue)
            {
                var min = ConvertToNumber(Input.Minimum);
                var max = ConvertToNumber(Input.Maximum);

                state.IsRangeUnderflow = min.HasValue && num < min.Value;
                state.IsRangeOverflow = max.HasValue && num > max.Value;
                state.IsValueMissing = false;
                state.IsStepMismatch = IsStepMismatch();
            }
            else
            {
                state.IsRangeUnderflow = false;
                state.IsRangeOverflow = false;
                state.IsValueMissing = Input.IsRequired;
                state.IsStepMismatch = false;
            }
        }

        public override void DoStep(Int32 n)
        {
            var num = ToNumber(Input.Value);

            if (num.HasValue)
            {
                var res = num.Value + GetStep() * n;
                var min = ToNumber(Input.Minimum);
                var max = ToNumber(Input.Maximum);

                if ((min.HasValue == false || min.Value <= res) && (max.HasValue == false || max.Value >= res))
                    Input.ValueAsNumber = res;
            }
        }

        #endregion

        #region Step

        protected override Double GetDefaultStepBase()
        {
            return 0.0;
        }

        protected override Double GetDefaultStep()
        {
            return 1.0;
        }

        protected override Double GetStepScaleFactor()
        {
            return 1.0;
        }

        #endregion
    }
}
