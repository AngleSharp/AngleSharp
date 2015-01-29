namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
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

        public override void Check(ValidityState state)
        {
            var value = Input.Value;
            var date = ConvertFromDateTime(value);

            if (date.HasValue)
            {
                var min = ConvertFromDateTime(Input.Minimum);
                var max = ConvertFromDateTime(Input.Maximum);

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
            var dt = ConvertFromDateTime(value);

            if (dt.HasValue)
                return dt.Value.ToUniversalTime().Subtract(OriginTime).TotalMilliseconds;

            return null;
        }

        public override String ConvertFromNumber(Double value)
        {
            var dt = OriginTime.AddMilliseconds(value);
            return ConvertFromDate(dt);
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromDateTime(value);
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
            var dt = ConvertFromDateTime(Input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromDateTime(Input.Minimum);
                var max = ConvertFromDateTime(Input.Maximum);

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
            return 60.0;
        }

        protected override Double GetStepScaleFactor()
        {
            return 1000.0;
        }

        #endregion

        #region Helper

        protected static DateTime? ConvertFromDateTime(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var month = 0;
            var day = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 ||
                position > value.Length - 13 ||
                value[position + 0] != Symbols.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false ||
                value[position + 3] != Symbols.Minus ||
                value[position + 4].IsDigit() == false ||
                value[position + 5].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1, 2));
            day = Int32.Parse(value.Substring(position + 4, 2));
            position += 6;
            var cal = CultureInfo.InvariantCulture.Calendar;

            if (year < 0 || year > 9999 || month < 1 || month > 12 || day < 1 || day > cal.GetDaysInMonth(year, month) || (value[position] != ' ' && value[position] != 'T'))
                return null;

            position++;
            var ts = ToTime(value, ref position);
            var dt = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Local);

            if (ts == null)
                return null;

            dt = dt.Add(ts.Value);

            if (position != value.Length)
                return null;

            return dt;
        }

        #endregion
    }
}
