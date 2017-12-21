namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Base type for the all input field types. Primarely from:
    /// http://www.w3.org/TR/html5/forms.html#range-state-(type=range)
    /// </summary>
    abstract class BaseInputType
    {
        #region Fields

        protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        protected static readonly Regex Number = new Regex("^\\-?\\d+(\\.\\d+)?([eE][\\-\\+]?\\d+)?$");

        private readonly IHtmlInputElement _input;
        private readonly Boolean _validate;
        private readonly String _name;

        #endregion

        #region ctor

        public BaseInputType(IHtmlInputElement input, String name, Boolean validate)
        {
            _input = input;
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

        public IHtmlInputElement Input
        {
            get { return _input; }
        }

        #endregion

        #region Methods

        public virtual Boolean IsAppendingData(IHtmlElement submitter)
        {
            return true;
        }

        public virtual void Check(ValidityState state)
        {
        }

        public virtual Double? ConvertToNumber(String value)
        {
            return null;
        }

        public virtual String ConvertFromNumber(Double value)
        {
            throw new DomException(DomError.InvalidState);
        }

        public virtual DateTime? ConvertToDate(String value)
        {
            return null;
        }

        public virtual String ConvertFromDate(DateTime value)
        {
            throw new DomException(DomError.InvalidState);
        }

        public virtual void ConstructDataSet(FormDataSet dataSet)
        {
            dataSet.Append(_input.Name, _input.Value, _input.Type);
        }

        public virtual void DoStep(Int32 n)
        {
            throw new DomException(DomError.InvalidState);
        }

        #endregion

        #region Step

        protected Boolean IsStepMismatch()
        {
            var step = GetStep();
            var value = ConvertToNumber(_input.Value);
            var offset = GetStepBase();
            return step != 0.0 && (value - offset) % step != 0.0;
        }

        protected Double GetStep()
        {
            var step = _input.Step;

            if (String.IsNullOrEmpty(step))
            {
                return GetDefaultStep() * GetStepScaleFactor();
            }
            else if (step.Isi(Keywords.Any))
            {
                return 0.0;
            }

            var num = ToNumber(step);

            if (num.HasValue == false || num <= 0.0)
            {
                return GetDefaultStep() * GetStepScaleFactor();
            }

            return num.Value * GetStepScaleFactor();
        }

        private Double GetStepBase()
        {
            var num = ConvertToNumber(_input.Minimum);

            if (num.HasValue)
            {
                return num.Value;
            }

            num = ConvertToNumber(_input.DefaultValue);

            if (num.HasValue)
            {
                return num.Value;
            }

            return GetDefaultStepBase();
        }

        protected virtual Double GetDefaultStepBase()
        {
            return 0.0;
        }

        protected virtual Double GetDefaultStep()
        {
            return 1.0;
        }

        protected virtual Double GetStepScaleFactor()
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
                    return !regex.IsMatch(value);
                }
                catch { }
            }

            return false;
        }

        protected static Double? ToNumber(String value)
        {
            if (!String.IsNullOrEmpty(value) && Number.IsMatch(value))
            {
                return Double.Parse(value, CultureInfo.InvariantCulture);
            }

            return null;
        }

        protected static TimeSpan? ToTime(String value, ref Int32 position)
        {
            var offset = position;
            var second = 0;
            var ms = 0;

            if (value.Length >= 5 + offset && 
                value[position++].IsDigit() && 
                value[position++].IsDigit() && 
                value[position++] == Symbols.Colon)
            {
                var hour = Int32.Parse(value.Substring(offset, 2));

                if (!IsLegalHour(hour) || !value[position++].IsDigit() || !value[position++].IsDigit())
                {
                    return null;
                }

                var minute = Int32.Parse(value.Substring(3 + offset, 2));

                if (!IsLegalMinute(minute))
                {
                    return null;
                }

                if (value.Length >= 8 + offset && value[position] == Symbols.Colon)
                {
                    position++;

                    if (!value[position++].IsDigit() || !value[position++].IsDigit())
                    {
                        return null;
                    }

                    second = Int32.Parse(value.Substring(6 + offset, 2));

                    if (!IsLegalSecond(second))
                    {
                        return null;
                    }

                    if (position + 1 < value.Length && value[position] == Symbols.Dot)
                    {
                        position++;
                        var start = position;

                        while (position < value.Length && value[position].IsDigit())
                        {
                            position++;
                        }

                        var fraction = value.Substring(start, position - start);
                        ms = Int32.Parse(fraction) * (Int32)Math.Pow(10, 3 - fraction.Length);
                    }
                }

                return new TimeSpan(0, hour, minute, second, ms);
            }

            return null;
        }

        protected static Int32 GetWeekOfYear(DateTime value)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        protected static Boolean IsLegalHour(Int32 value)
        {
            return value >= 0 && value <= 23;
        }

        protected static Boolean IsLegalSecond(Int32 value)
        {
            return value >= 0 && value <= 59;
        }

        protected static Boolean IsLegalMinute(Int32 value)
        {
            return value >= 0 && value <= 59;
        }
        
        protected static Boolean IsLegalMonth(Int32 value)
        {
            return value >= 1 && value <= 12;
        }

        protected static Boolean IsLegalYear(Int32 value)
        {
            return value >= 0 && value <= 9999;
        }

        protected static Boolean IsLegalDay(Int32 day, Int32 month, Int32 year)
        {
            if (IsLegalYear(year) && IsLegalMonth(month))
            {
                var cal = CultureInfo.InvariantCulture.Calendar;
                return day >= 1 && day <= cal.GetDaysInMonth(year, month);
            }

            return false;
        }

        protected static Boolean IsLegalWeek(Int32 week, Int32 year)
        {
            if (IsLegalYear(year))
            {
                var endOfYear = new DateTime(year, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc);
                var numOfWeeks = GetWeekOfYear(endOfYear);
                return week >= 0 && week < numOfWeeks;
            }

            return false;
        }

        protected static Boolean IsTimeSeparator(Char chr)
        {
            return chr == ' ' || chr == 'T';
        }

        protected static Int32 FetchDigits(String value)
        {
            var position = 0;

            while (position < value.Length && value[position].IsDigit())
            {
                position++;
            }

            return position;
        }

        protected static Boolean PositionIsValidForDateTime(String value, Int32 position)
        {
            return position >= 4 && position <= value.Length - 13 &&
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
