namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using System;
    using System.Globalization;

    class DatetimeLocalInputType : BaseInputType
    {
        #region ctor

        public DatetimeLocalInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value;
            var date = ConvertFromDateTime(value.AsSpan());
            var min = ConvertFromDateTime(Input.Minimum.AsSpan());
            var max = ConvertFromDateTime(Input.Maximum.AsSpan());
            return CheckTime(current, value, date, min, max);
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromDateTime(value.AsSpan());

            if (dt.HasValue)
            {
                return dt.Value.ToUniversalTime().Subtract(UnixEpoch).TotalMilliseconds;
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
            return ConvertFromDateTime(value.AsSpan());
        }

        public override String ConvertFromDate(DateTime value)
        {
            value = value.ToLocalTime();
            var date = String.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", value.Year, value.Month, value.Day);
            var time = String.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00},{3:000}", value.Hour, value.Minute, value.Second, value.Millisecond);
            return String.Concat(date, "T", time);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromDateTime(Input.Value.AsSpan());

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromDateTime(Input.Minimum.AsSpan());
                var max = ConvertFromDateTime(Input.Maximum.AsSpan());

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
            return 60.0;
        }

        protected override Double GetStepScaleFactor()
        {
            return 1000.0;
        }

        #endregion

        #region Helper

        protected static DateTime? ConvertFromDateTime(ReadOnlySpan<char> value)
        {
            if (value.Length > 0)
            {
                var position = FetchDigits(value);

                if (PositionIsValidForDateTime(value, position))
                {
                    var yearString = value.Slice(0, position);
                    var year = NumberHelper.ParseInt32(yearString);
                    var monthString = value.Slice(position + 1, 2);
                    var month = NumberHelper.ParseInt32(monthString);
                    var dayString = value.Slice(position + 4, 2);
                    var day = NumberHelper.ParseInt32(dayString);
                    position += 6;

                    if (IsLegalDay(day, month, year) && IsTimeSeparator(value[position]))
                    {
                        position++;
                        var ts = ToTime(value, ref position);
                        var dt = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Local);

                        if (ts != null)
                        {
                            dt = dt.Add(ts.Value);

                            if (position == value.Length)
                            {
                                return dt;
                            }
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
