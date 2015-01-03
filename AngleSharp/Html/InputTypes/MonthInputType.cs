namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class MonthInputType : BaseInputType
    {
        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromMonth(value);

            if (date.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertFromMonth(input.Minimum);
                var max = ConvertFromMonth(input.Maximum);

                state.IsRangeUnderflow = min.HasValue && date < min.Value;
                state.IsRangeOverflow = max.HasValue && date > max.Value;
                state.IsValueMissing = false;
                state.IsBadInput = false;
                state.IsStepMismatch = step != 0.0 && GetStepBase(input) % step != 0.0;
            }
            else
            {
                state.IsRangeUnderflow = false;
                state.IsRangeOverflow = false;
                state.IsStepMismatch = false;
                state.IsValueMissing = input.IsRequired;
                state.IsBadInput = !String.IsNullOrEmpty(value);
            }
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromMonth(value);

            if (dt.HasValue)
                return dt.Value.Month - 1;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromMonth(value);
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var dt = ConvertFromMonth(input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep(input) * n);
                var min = ConvertFromMonth(input.Minimum);
                var max = ConvertFromMonth(input.Maximum);

                if ((min.HasValue == false || min.Value <= date) && (max.HasValue == false || max.Value >= date))
                    input.ValueAsDate = date;
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
