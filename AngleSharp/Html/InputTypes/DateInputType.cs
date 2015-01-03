namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class DateInputType : BaseInputType
    {
        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromDate(value);

            if (date.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertFromDate(input.Minimum);
                var max = ConvertFromDate(input.Maximum);

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
            var dt = ConvertFromDate(value);

            if (dt.HasValue)
                return dt.Value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0).AddDays(-1)).TotalMilliseconds;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromDate(value);
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var dt = ConvertFromDate(input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep(input) * n);
                var min = ConvertFromDate(input.Minimum);
                var max = ConvertFromDate(input.Maximum);

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
            return 86400000.0;
        }

        #endregion
    }
}
