namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    class DateInputType : BaseInputType
    {
        #region ctor

        public DateInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value;
            var date = ConvertFromDate(value);
            var min = ConvertFromDate(Input.Minimum);
            var max = ConvertFromDate(Input.Maximum);
            return CheckTime(current, value, date, min, max);
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromDate(value);

            if (dt.HasValue)
            {
                return dt.Value.Subtract(UnixEpoch).TotalMilliseconds;
            }

            return null;
        }

        public override String ConvertFromNumber(Double value)
        {
            var dt = UnixEpoch.AddMilliseconds(value);
            return ConvertFromDate(dt);
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromDate(value);
        }

        public override String ConvertFromDate(DateTime value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", value.Year, value.Month, value.Day);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromDate(Input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromDate(Input.Minimum);
                var max = ConvertFromDate(Input.Maximum);

                if ((!min.HasValue || min.Value <= date) && (!max.HasValue || max.Value >= date))
                {
                    Input.ValueAsDate = date;
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
            return 86400000.0;
        }

        #endregion

        #region Helper

        protected static DateTime? ConvertFromDate(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var position = FetchDigits(value);

                if (IsLegalPosition(value, position))
                {
                    var yearString = value.Substring(0, position);
                    var year = Int32.Parse(yearString, CultureInfo.InvariantCulture);
                    var monthString = value.Substring(position + 1, 2);
                    var month = Int32.Parse(monthString, CultureInfo.InvariantCulture);
                    var dayString = value.Substring(position + 4, 2);
                    var day = Int32.Parse(dayString, CultureInfo.InvariantCulture);

                    if (IsLegalDay(day, month, year))
                    {
                        return new DateTime(year, month, day, 0, 0, 0, 0, DateTimeKind.Utc);
                    }
                }
            }

            return null;
        }

        private static Boolean IsLegalPosition(String value, Int32 position)
        {
            return position >= 4 && position == value.Length - 6 &&
                    value[position + 0] == Symbols.Minus &&
                    value[position + 1].IsDigit() &&
                    value[position + 2].IsDigit() &&
                    value[position + 3] == Symbols.Minus &&
                    value[position + 4].IsDigit() &&
                    value[position + 5].IsDigit();
        }

        #endregion
    }
}
