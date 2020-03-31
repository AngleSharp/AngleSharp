namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Base type for the all input field types. Primarely from:
    /// http://www.w3.org/TR/html5/forms.html#range-state-(type=range)
    /// </summary>
    public abstract class BaseInputType
    {
        #region Fields

        /// <summary>
        /// The start of the unix epoch (1st of January 1970).
        /// </summary>
        protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Simple regular expression for floating point numbers.
        /// </summary>
        protected static readonly Regex Number = new Regex("^\\-?\\d+(\\.\\d+)?([eE][\\-\\+]?\\d+)?$");

        private readonly IHtmlInputElement _input;
        private readonly Boolean _validate;
        private readonly String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new base input type.
        /// </summary>
        public BaseInputType(IHtmlInputElement input, String name, Boolean validate)
        {
            _input = input;
            _validate = validate;
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the input type.
        /// </summary>
        public String Name => _name;

        /// <summary>
        /// Gets if the input type can be validated.
        /// </summary>
        public Boolean CanBeValidated => _validate;

        /// <summary>
        /// Gets the associated input element.
        /// </summary>
        public IHtmlInputElement Input => _input;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the given type wants to append data.
        /// </summary>
        public virtual Boolean IsAppendingData(IHtmlElement submitter)
        {
            return true;
        }

        /// <summary>
        /// Checks the current input for its validity.
        /// </summary>
        public virtual ValidationErrors Check(IValidityState current)
        {
            return GetErrorsFrom(current);
        }

        /// <summary>
        /// Tries to convert the given string to a number.
        /// </summary>
        public virtual Double? ConvertToNumber(String value)
        {
            return null;
        }

        /// <summary>
        /// Tries to convert the given number to a string.
        /// </summary>
        public virtual String ConvertFromNumber(Double value)
        {
            throw new DomException(DomError.InvalidState);
        }

        /// <summary>
        /// Tries to convert the given string to a date time.
        /// </summary>
        public virtual DateTime? ConvertToDate(String value)
        {
            return null;
        }

        /// <summary>
        /// Tries to convert the given date time to a string.
        /// </summary>
        public virtual String ConvertFromDate(DateTime value)
        {
            throw new DomException(DomError.InvalidState);
        }

        /// <summary>
        /// Populates the form data set with the current input.
        /// </summary>
        public virtual void ConstructDataSet(FormDataSet dataSet)
        {
            dataSet.Append(_input.Name, _input.Value, _input.Type);
        }

        /// <summary>
        /// Changes the value by n steps.
        /// </summary>
        public virtual void DoStep(Int32 n)
        {
            throw new DomException(DomError.InvalidState);
        }

        #endregion

        #region Step

        /// <summary>
        /// Checks if the current value does not match the steps.
        /// </summary>
        protected Boolean IsStepMismatch()
        {
            var step = GetStep();
            var value = ConvertToNumber(_input.Value);
            var offset = GetStepBase();
            return step != 0.0 && (value - offset) % step != 0.0;
        }

        /// <summary>
        /// Gets the current step size.
        /// </summary>
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

        /// <summary>
        /// Gets the default step offset.
        /// </summary>
        protected virtual Double GetDefaultStepBase()
        {
            return 0.0;
        }

        /// <summary>
        /// Gets the default step size.
        /// </summary>
        protected virtual Double GetDefaultStep()
        {
            return 1.0;
        }

        /// <summary>
        /// Gets the step scaling factor.
        /// </summary>
        protected virtual Double GetStepScaleFactor()
        {
            return 1.0;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Converts the given validity state to a validation error enum.
        /// </summary>
        protected static ValidationErrors GetErrorsFrom(IValidityState state)
        {
            var result = ValidationErrors.None;

            if (state.IsBadInput)
            {
                result ^= ValidationErrors.BadInput;
            }

            if (state.IsTooShort)
            {
                result ^= ValidationErrors.TooShort;
            }

            if (state.IsTooLong)
            {
                result ^= ValidationErrors.TooLong;
            }

            if (state.IsValueMissing)
            {
                result ^= ValidationErrors.ValueMissing;
            }

            if (state.IsCustomError)
            {
                result ^= ValidationErrors.Custom;
            }

            return result;
        }

        /// <summary>
        /// Validates the time using the given parameters.
        /// </summary>
        protected ValidationErrors CheckTime(IValidityState state, String value, DateTime? date, DateTime? min, DateTime? max)
        {
            var result = state.IsCustomError ?
                ValidationErrors.Custom :
                ValidationErrors.None;

            if (date.HasValue)
            {
                if (min.HasValue && date < min.Value)
                {
                    result ^= ValidationErrors.RangeUnderflow;
                }

                if (max.HasValue && date > max.Value)
                {
                    result ^= ValidationErrors.RangeOverflow;
                }

                if (IsStepMismatch())
                {
                    result ^= ValidationErrors.StepMismatch;
                }
            }
            else
            {
                if (Input.IsRequired)
                {
                    result ^= ValidationErrors.ValueMissing;
                }

                if (!String.IsNullOrEmpty(value))
                {
                    result ^= ValidationErrors.BadInput;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if the string does not follow the pattern.
        /// </summary>
        protected static Boolean IsInvalidPattern(String pattern, String value)
        {
            if (!String.IsNullOrEmpty(pattern) && !String.IsNullOrEmpty(value))
            {
                try
                {
                    var regex = new Regex(pattern, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
                    return !regex.IsMatch(value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error while matching the pattern: {0}.", ex);
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to convert the value to a number using the default expression.
        /// </summary>
        protected static Double? ToNumber(String value)
        {
            if (!String.IsNullOrEmpty(value) && Number.IsMatch(value))
            {
                return Double.Parse(value, CultureInfo.InvariantCulture);
            }

            return null;
        }

        /// <summary>
        /// Tries to convert the value to a time starting at the given position.
        /// </summary>
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
                var hour = Int32.Parse(value.Substring(offset, 2), CultureInfo.InvariantCulture);

                if (!IsLegalHour(hour) || !value[position++].IsDigit() || !value[position++].IsDigit())
                {
                    return null;
                }

                var minute = Int32.Parse(value.Substring(3 + offset, 2), CultureInfo.InvariantCulture);

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

                    second = Int32.Parse(value.Substring(6 + offset, 2), CultureInfo.InvariantCulture);

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
                        ms = Int32.Parse(fraction, CultureInfo.InvariantCulture) * (Int32)Math.Pow(10, 3 - fraction.Length);
                    }
                }

                return new TimeSpan(0, hour, minute, second, ms);
            }

            return null;
        }

        /// <summary>
        /// Tries to convert the value to a week.
        /// </summary>
        protected static Int32 GetWeekOfYear(DateTime value)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// Checks if the given value is a legal hour.
        /// </summary>
        protected static Boolean IsLegalHour(Int32 value)
        {
            return value >= 0 && value <= 23;
        }

        /// <summary>
        /// Checks if the given value is a legal second.
        /// </summary>
        protected static Boolean IsLegalSecond(Int32 value)
        {
            return value >= 0 && value <= 59;
        }

        /// <summary>
        /// Checks if the given value is a legal minute.
        /// </summary>
        protected static Boolean IsLegalMinute(Int32 value)
        {
            return value >= 0 && value <= 59;
        }

        /// <summary>
        /// Checks if the given value is a legal month.
        /// </summary>
        protected static Boolean IsLegalMonth(Int32 value)
        {
            return value >= 1 && value <= 12;
        }

        /// <summary>
        /// Checks if the given value is a legal year.
        /// </summary>
        protected static Boolean IsLegalYear(Int32 value)
        {
            return value >= 0 && value <= 9999;
        }

        /// <summary>
        /// Checks if the given values form a legal date.
        /// </summary>
        protected static Boolean IsLegalDay(Int32 day, Int32 month, Int32 year)
        {
            if (IsLegalYear(year) && IsLegalMonth(month))
            {
                var cal = CultureInfo.InvariantCulture.Calendar;
                return day >= 1 && day <= cal.GetDaysInMonth(year, month);
            }

            return false;
        }

        /// <summary>
        /// Checks if the given values form a legal week.
        /// </summary>
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

        /// <summary>
        /// Checks if the given character is a valid time separator.
        /// </summary>
        protected static Boolean IsTimeSeparator(Char chr)
        {
            return chr == ' ' || chr == 'T';
        }

        /// <summary>
        /// Skips all legit digits while returning the final position.
        /// </summary>
        protected static Int32 FetchDigits(String value)
        {
            var position = 0;

            while (position < value.Length && value[position].IsDigit())
            {
                position++;
            }

            return position;
        }

        /// <summary>
        /// Checks the assumption that the string continues with a date time.
        /// </summary>
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
