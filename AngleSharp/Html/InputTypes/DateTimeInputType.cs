namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Globalization;

    class DatetimeInputType : BaseInputType
    {
        #region ctor

        public DatetimeInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion


        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromDateTime(value);

            if (date.HasValue)
            {
                var min = ConvertFromDateTime(input.Minimum);
                var max = ConvertFromDateTime(input.Maximum);

                state.IsRangeUnderflow = min.HasValue && date < min.Value;
                state.IsRangeOverflow = max.HasValue && date > max.Value;
                state.IsValueMissing = false;
                state.IsBadInput = false;
                state.IsStepMismatch = IsStepMismatch(input);
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
            var dt = ConvertFromDateTime(value);

            if (dt.HasValue)
                return dt.Value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

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
                value[position + 0] != Specification.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false ||
                value[position + 3] != Specification.Minus ||
                value[position + 4].IsDigit() == false ||
                value[position + 5].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1, 2));
            day = Int32.Parse(value.Substring(position + 4, 2));
            position += 6;
            var cal = CultureInfo.InvariantCulture.Calendar;
            var requireOffset = value[position] == ' ';

            if (year < 0 || year > 9999 || month < 1 || month > 12 || day < 1 || day > cal.GetDaysInMonth(year, month) || (requireOffset == false && value[position] != 'T'))
                return null;

            position++;
            var ts = ConvertFromTime(value, ref position);
            var dt = new DateTime(year, month, day);

            if (ts == null)
                return null;

            dt = dt.Add(ts.Value);

            if (position == value.Length)
            {
                if (requireOffset)
                    return null;

                return dt;
            }

            if (value[position] != 'Z')
            {
                if (position + 6 != value.Length ||
                    value[position + 1].IsDigit() == false ||
                    value[position + 2].IsDigit() == false ||
                    value[position + 3] != Specification.Colon ||
                    value[position + 4].IsDigit() == false ||
                    value[position + 5].IsDigit() == false)
                    return null;

                var hours = Int32.Parse(value.Substring(position + 1, 2));
                var minutes = Int32.Parse(value.Substring(position + 4, 2));
                var offset = new TimeSpan(hours, minutes, 0);

                if (value[position] == '+')
                    dt = dt.Add(offset);
                else if (value[position] == '-')
                    dt = dt.Subtract(offset);
                else
                    return null;
            }
            else if (position + 1 != value.Length)
                return null;
            else
                dt = dt.ToUniversalTime();

            return dt;
        }

        #endregion
    }
}
