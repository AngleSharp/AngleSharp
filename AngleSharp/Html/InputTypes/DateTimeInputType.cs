namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class DatetimeInputType : BaseInputType
    {
        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromDateTime(value);
            state.Reset();

            if (date.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertFromDateTime(input.Minimum);
                var max = ConvertFromDateTime(input.Maximum);

                if (min.HasValue)
                    state.IsRangeUnderflow = date < min.Value;

                if (max.HasValue)
                    state.IsRangeOverflow = date > max.Value;

                state.IsStepMismatch = step != 0.0 && GetStepBase(input) % step != 0.0;
            }
            else
            {
                state.IsValueMissing = input.IsRequired;
                state.IsBadInput = !String.IsNullOrEmpty(value);
            }
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromDateTime(value);

            if (dt.HasValue)
                return dt.Value.Month - 1;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromDateTime(value);
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var dt = ConvertFromDateTime(input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep(input) * n);
                var min = ConvertFromDateTime(input.Minimum);
                var max = ConvertFromDateTime(input.Maximum);

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
            return 60.0;
        }

        protected override Double GetStepScaleFactor(IHtmlInputElement input)
        {
            return 1000.0;
        }

        #endregion
    }
}
