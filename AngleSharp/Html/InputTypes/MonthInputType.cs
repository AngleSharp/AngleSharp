namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Globalization;

    class MonthInputType : BaseInputType
    {
        #region ctor

        public MonthInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(ValidityState state)
        {
            var value = Input.Value;
            var date = ConvertFromMonth(value);

            if (date.HasValue)
            {
                var min = ConvertFromMonth(Input.Minimum);
                var max = ConvertFromMonth(Input.Maximum);

                state.IsRangeUnderflow = min.HasValue && date < min.Value;
                state.IsRangeOverflow = max.HasValue && date > max.Value;
                state.IsValueMissing = false;
                state.IsBadInput = false;
                state.IsStepMismatch = IsStepMismatch();
            }
            else
            {
                state.IsRangeUnderflow = false;
                state.IsRangeOverflow = false;
                state.IsStepMismatch = false;
                state.IsValueMissing = Input.IsRequired;
                state.IsBadInput = !String.IsNullOrEmpty(value);
            }
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromMonth(value);

            if (dt.HasValue)
                return (dt.Value.Year - 1970) * 12 + dt.Value.Month - 1;

            return null;
        }

        public override String ConvertFromNumber(Double value)
        {
            var dt = OriginTime.AddMonths((Int32)value);
            return ConvertFromDate(dt);
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromMonth(value);
        }

        public override String ConvertFromDate(DateTime value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}", value.Year, value.Month);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromMonth(Input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromMonth(Input.Minimum);
                var max = ConvertFromMonth(Input.Maximum);

                if ((min.HasValue == false || min.Value <= date) && (max.HasValue == false || max.Value >= date))
                    Input.ValueAsDate = date;
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

        #region Helper

        protected static DateTime? ConvertFromMonth(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var month = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 ||
                position != value.Length - 3 ||
                value[position + 0] != Symbols.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1));

            if (year < 0 || year > 9999 || month < 1 || month > 12)
                return null;

            return new DateTime(year, month, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        }

        #endregion
    }
}
