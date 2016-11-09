namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
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

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value;
            var num = ConvertToNumber(value);
            var result = current.IsCustomError ?
                ValidationErrors.Custom :
                ValidationErrors.None;

            if (num.HasValue)
            {
                var min = ConvertToNumber(Input.Minimum);
                var max = ConvertToNumber(Input.Maximum);

                if (min.HasValue && num < min.Value)
                {
                    result ^= ValidationErrors.RangeUnderflow;
                }

                if (max.HasValue && num > max.Value)
                {
                    result ^= ValidationErrors.RangeOverflow;
                }

                if (IsStepMismatch())
                {
                    result ^= ValidationErrors.StepMismatch;
                }
            }
            else
            {
                if (Input.IsRequired)
                {
                    result ^= ValidationErrors.ValueMissing;
                }
            }

            return result;
        }

        public override void DoStep(Int32 n)
        {
            var num = ToNumber(Input.Value);

            if (num.HasValue)
            {
                var res = num.Value + GetStep() * n;
                var min = ToNumber(Input.Minimum);
                var max = ToNumber(Input.Maximum);

                if ((!min.HasValue || min.Value <= res) && (!max.HasValue || max.Value >= res))
                {
                    Input.ValueAsNumber = res;
                }
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
