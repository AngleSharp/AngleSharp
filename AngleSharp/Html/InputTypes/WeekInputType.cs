namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Globalization;

    class WeekInputType : BaseInputType
    {
        #region ctor

        public WeekInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(ValidityState state)
        {
            var value = Input.Value;
            var date = ConvertFromWeek(value);

            if (date.HasValue)
            {
                var min = ConvertFromWeek(Input.Minimum);
                var max = ConvertFromWeek(Input.Maximum);

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
            var dt = ConvertFromWeek(value);

            if (dt.HasValue)
                return dt.Value.Subtract(new DateTime(1970, 1, 5, 0, 0, 0)).TotalMilliseconds;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromWeek(value);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromWeek(Input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromWeek(Input.Minimum);
                var max = ConvertFromWeek(Input.Maximum);

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
