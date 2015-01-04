namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using AngleSharp.Extensions;
    using System;
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
            return null;
        }

        public virtual DateTime? ConvertToDate(String value)
        {
            return null;
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

        protected static TimeSpan? ConvertFromTime(String value, ref Int32 position)
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

        #endregion
    }
}
