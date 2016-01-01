namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
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
            return ConvertFromWeek(value);
        }

        public override String ConvertFromDate(DateTime value)
        {
            var week = GetWeekOfYear(value);
            return String.Format(CultureInfo.InvariantCulture, "{0:0000}-W{1:00}", value.Year, week);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromWeek(Input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromWeek(Input.Minimum);
                var max = ConvertFromWeek(Input.Maximum);

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
            return -259200000;
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
            if (!String.IsNullOrEmpty(value))
            {
                var position = FetchDigits(value);

                if (IsLegalPosition(value, position))
                {
                    var year = Int32.Parse(value.Substring(0, position));
                    var week = Int32.Parse(value.Substring(position + 2)) - 1;

                    if (IsLegalWeek(week, year))
                    {
                        var startOfYear = new DateTime(year, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        var day = startOfYear.DayOfWeek;

                        if (day == DayOfWeek.Sunday)
                        {
                            startOfYear = startOfYear.AddDays(-6);
                        }
                        else if (day > DayOfWeek.Monday)
                        {
                            startOfYear = startOfYear.AddDays(1 - (Int32)day);
                        }

                        return startOfYear.AddDays(7 * week);
                    }
                }
            }

            return null;
        }

        static Boolean IsLegalPosition(String value, Int32 position)
        {
            return position >= 4 && position == value.Length - 4 &&
                    value[position + 0] == Symbols.Minus &&
                    value[position + 1] == 'W' &&
                    value[position + 2].IsDigit() &&
                    value[position + 3].IsDigit();
        }

        #endregion
    }
}
