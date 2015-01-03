namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    abstract class BaseInputType
    {
        #region Fields

        static readonly Regex number = new Regex("^\\-?\\d+(\\.\\d+)?([eE][\\-\\+]?\\d+)?$");

        readonly Boolean _validate;
        readonly String _name;

        #endregion

        #region ctor

        public BaseInputType(String name)
            : this(name, true)
        {
        }

        public BaseInputType(String name, Boolean validate)
        {
            _validate = validate;
            _name = name;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
        }

        public Boolean CanBeValidated
        {
            get { return _validate; }
        }

        #endregion

        #region Methods

        public virtual void Check(IHtmlInputElement input, ValidityState state)
        {
        }

        public virtual Double? ConvertToNumber(String value)
        {
            return ConvertFromNumber(value);
        }

        public virtual DateTime? ConvertToDate(String value)
        {
            return ConvertFromDateTime(value);
        }

        public virtual void ConstructDataSet(IHtmlInputElement input, FormDataSet dataSet)
        {
            dataSet.Append(input.Name, input.Value, input.Type);
        }

        public virtual void DoStep(IHtmlInputElement input, Int32 n)
        {
            throw new DomException(ErrorCode.InvalidState);
        }

        #endregion

        #region Step

        protected Double GetStep(IHtmlInputElement input)
        {
            var step = input.Step;

            if (String.IsNullOrEmpty(step))
                return GetDefaultStep(input) * GetStepScaleFactor(input);
            else if (step.Equals(Keywords.Any, StringComparison.OrdinalIgnoreCase))
                return 0.0;

            var num = ConvertFromNumber(step);

            if (num.HasValue == false || num <= 0.0)
                return GetDefaultStep(input) * GetStepScaleFactor(input);

            return num.Value * GetStepScaleFactor(input);
        }

        protected Double GetStepBase(IHtmlInputElement input)
        {
            var num = ConvertToNumber(input.Minimum);

            if (num.HasValue)
                return num.Value;

            num = ConvertToNumber(input.Value);

            if (num.HasValue)
                return num.Value;

            return GetDefaultStepBase(input);
        }

        protected virtual Double GetDefaultStepBase(IHtmlInputElement input)
        {
            return 0.0;
        }

        protected virtual Double GetDefaultStep(IHtmlInputElement input)
        {
            return 1.0;
        }

        protected virtual Double GetStepScaleFactor(IHtmlInputElement input)
        {
            return 1.0;
        }

        #endregion

        #region Helper

        protected static Boolean IsInvalidPattern(String pattern, String value)
        {
            if (!String.IsNullOrEmpty(pattern) && !String.IsNullOrEmpty(value))
            {
                try
                {
                    var regex = new Regex(pattern, RegexOptions.ECMAScript);
                    return regex.IsMatch(value) == false;
                }
                catch { }
            }

            return false;
        }

        protected static Double? ConvertFromNumber(String value)
        {
            if (!String.IsNullOrEmpty(value) && number.IsMatch(value))
                return Double.Parse(value);

            return null;
        }

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
                value[position + 0] != Specification.Minus ||
                value[position + 1].IsDigit() == false ||
                value[position + 2].IsDigit() == false)
                return null;

            year = Int32.Parse(value.Substring(0, position));
            month = Int32.Parse(value.Substring(position + 1));

            if (year < 0 || year > 9999 || month < 1 || month > 12)
                return null;

            return new DateTime(year, month, 1);
        }

        protected static DateTime? ConvertFromDate(String value)
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
                position != value.Length - 6 ||
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
            var cal = CultureInfo.InvariantCulture.Calendar;

            if (year < 0 || year > 9999 || month < 1 || month > 12 || day < 1 || day > cal.GetDaysInMonth(year, month))
                return null;

            return new DateTime(year, month, day);
        }

        static TimeSpan? ConvertFromTime(String value, ref Int32 position)
        {
            var offset = position;
            var hour = 0;
            var minute = 0;
            var second = 0;
            var ms = 0;

            if (value.Length < 5 + offset || value[position++].IsDigit() == false || value[position++].IsDigit() == false || value[position++] != Specification.Colon)
                return null;

            hour = Int32.Parse(value.Substring(offset, 2));

            if (hour < 0 || hour > 23)
                return null;

            if (value[position++].IsDigit() == false || value[position++].IsDigit() == false)
                return null;

            minute = Int32.Parse(value.Substring(3 + offset, 2));

            if (minute < 0 || minute > 59)
                return null;

            if (value.Length >= 8 + offset && value[position] == Specification.Colon)
            {
                position++;

                if (value[position++].IsDigit() == false || value[position++].IsDigit() == false)
                    return null;

                second = Int32.Parse(value.Substring(6 + offset, 2));

                if (second < 0 || second > 59)
                    return null;

                if (position + 1 < value.Length && value[position] == Specification.Dot)
                {
                    position++;
                    var start = position;

                    while (position < value.Length)
                    {
                        if (value[position].IsDigit())
                            position++;
                        else
                            break;
                    }

                    var fraction = value.Substring(start, position - start);
                    ms = Int32.Parse(fraction) * (Int32)Math.Pow(10, 3 - fraction.Length);
                }
            }

            return new TimeSpan(0, hour, minute, second, ms);
        }

        protected static DateTime? ConvertFromTime(String value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var position = 0;
            var ts = ConvertFromTime(value, ref position);

            if (ts == null || position != value.Length)
                return null;

            return new DateTime().Add(ts.Value);
        }

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
