namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Base type for the all input field types. Primarely from:
    /// http://www.w3.org/TR/html5/forms.html#range-state-(type=range)
    /// </summary>
    abstract class BaseInputType
    {
        #region Fields

        protected static readonly DateTime OriginTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        static readonly Regex number = new Regex("^\\-?\\d+(\\.\\d+)?([eE][\\-\\+]?\\d+)?$");

        readonly IHtmlInputElement _input;
        readonly Boolean _validate;
        readonly String _name;

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
                return GetDefaultStep() * GetStepScaleFactor();
            else if (step.Equals(Keywords.Any, StringComparison.OrdinalIgnoreCase))
                return 0.0;

            var num = ToNumber(step);

            if (num.HasValue == false || num <= 0.0)
                return GetDefaultStep() * GetStepScaleFactor();

            return num.Value * GetStepScaleFactor();
        }

        Double GetStepBase()
        {
            var num = ConvertToNumber(_input.Minimum);

            if (num.HasValue)
                return num.Value;

            num = ConvertToNumber(_input.DefaultValue);

            if (num.HasValue)
                return num.Value;

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
                    return regex.IsMatch(value) == false;
                }
                catch { }
            }

            return false;
        }

        protected static Double? ToNumber(String value)
        {
            if (!String.IsNullOrEmpty(value) && number.IsMatch(value))
                return Double.Parse(value);

            return null;
        }

        protected static TimeSpan? ToTime(String value, ref Int32 position)
        {
            var offset = position;
            var hour = 0;
            var minute = 0;
            var second = 0;
            var ms = 0;

            if (value.Length < 5 + offset || value[position++].IsDigit() == false || value[position++].IsDigit() == false || value[position++] != Symbols.Colon)
                return null;

            hour = Int32.Parse(value.Substring(offset, 2));

            if (hour < 0 || hour > 23)
                return null;

            if (value[position++].IsDigit() == false || value[position++].IsDigit() == false)
                return null;

            minute = Int32.Parse(value.Substring(3 + offset, 2));

            if (minute < 0 || minute > 59)
                return null;

            if (value.Length >= 8 + offset && value[position] == Symbols.Colon)
            {
                position++;

                if (value[position++].IsDigit() == false || value[position++].IsDigit() == false)
                    return null;

                second = Int32.Parse(value.Substring(6 + offset, 2));

                if (second < 0 || second > 59)
                    return null;

                if (position + 1 < value.Length && value[position] == Symbols.Dot)
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

        #endregion
    }
}
