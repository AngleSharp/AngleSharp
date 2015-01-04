namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Globalization;

    class WeekInputType : BaseInputType
    {
        #region ctor

        public WeekInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromWeek(value);

            if (date.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertFromWeek(input.Minimum);
                var max = ConvertFromWeek(input.Maximum);

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
            var dt = ConvertFromWeek(value);

            if (dt.HasValue)
                return dt.Value.Subtract(new DateTime(1970, 1, 5, 0, 0, 0)).TotalMilliseconds;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromWeek(value);
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var dt = ConvertFromWeek(input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep(input) * n);
                var min = ConvertFromWeek(input.Minimum);
                var max = ConvertFromWeek(input.Maximum);

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
            return 604800000.0;
        }

        #endregion

        #region Helper

        protected static DateTime? ConvertFromWeek(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var year = 0;
            var week = 0;

            while (position < value.Length)
            {
                if (value[position].IsDigit())
                    position++;
                else
                    break;
            }

            if (position < 4 ||
                position != value.Length - 4 ||
                value[position + 0] != Specification.Minus ||
                value[position + 1] != 'W' ||
                value[position + 2].IsDigit() == false ||
                value[position + 3].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            week = Int32.Parse(value.Substring(position + 2)) - 1;

            if (year < 0 || year > 9999)
                return null;

            var endOfYear = new DateTime(year, 12, 31);
            var cal = CultureInfo.InvariantCulture.Calendar;
            var numOfWeeks = cal.GetWeekOfYear(endOfYear, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (week < 0 || week >= numOfWeeks)
                return null;

            var startOfYear = new DateTime(year, 1, 1);
            var day = cal.GetDayOfWeek(startOfYear);

            if (day == DayOfWeek.Sunday)
                startOfYear = startOfYear.AddDays(1);
            else if (day > DayOfWeek.Monday)
                startOfYear = startOfYear.AddDays(8 - (Int32)day);

            return startOfYear.AddDays(7 * week);
        }

        #endregion
    }
}
