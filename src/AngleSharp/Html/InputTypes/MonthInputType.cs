namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
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

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value;
            var date = ConvertFromMonth(value.AsSpan());
            var min = ConvertFromMonth(Input.Minimum.AsSpan());
            var max = ConvertFromMonth(Input.Maximum.AsSpan());
            return CheckTime(current, value, date, min, max);
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromMonth(value.AsSpan());

            if (dt.HasValue)
            {
                return (dt.Value.Year - 1970) * 12 + dt.Value.Month - 1;
            }

            return null;
        }

        public override String ConvertFromNumber(Double value)
        {
            var dt = UnixEpoch.AddMonths((Int32)value);
            return ConvertFromDate(dt);
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromMonth(value.AsSpan());
        }

        public override String ConvertFromDate(DateTime value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}", value.Year, value.Month);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromMonth(Input.Value.AsSpan());

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromMonth(Input.Minimum.AsSpan());
                var max = ConvertFromMonth(Input.Maximum.AsSpan());

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
            return 1.0;
        }

        #endregion

        #region Helper

        protected static DateTime? ConvertFromMonth(ReadOnlySpan<char> value)
        {
            if (value.Length > 0)
            {
                var position = FetchDigits(value);

                if (IsLegalPosition(value, position))
                {
                    var yearString = value.Slice(0, position);
                    var year = NumberHelper.ParseInt32(yearString);
                    var monthString = value.Slice(position + 1);
                    var month = NumberHelper.ParseInt32(monthString);

                    if (IsLegalDay(1, month, year))
                    {
                        return new DateTime(year, month, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    }
                }
            }

            return null;
        }

        private static Boolean IsLegalPosition(ReadOnlySpan<char> value, Int32 position)
        {
            return position >= 4 && position == value.Length - 3 &&
                    value[position + 0] == Symbols.Minus &&
                    value[position + 1].IsDigit() &&
                    value[position + 2].IsDigit();
        }

        #endregion
    }
}
