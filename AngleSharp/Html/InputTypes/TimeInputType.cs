namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class TimeInputType : BaseInputType
    {
        #region ctor

        public TimeInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value;
            var date = ConvertFromTime(value);

            if (date.HasValue)
            {
                var step = GetStep(input);
                var min = ConvertFromTime(input.Minimum);
                var max = ConvertFromTime(input.Maximum);

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
            var dt = ConvertFromTime(value);

            if (dt.HasValue)
                return dt.Value.Subtract(new DateTime()).TotalMilliseconds;

            return null;
        }

        public override DateTime? ConvertToDate(String value)
        {
            return ConvertFromTime(value);
        }

        public override void DoStep(IHtmlInputElement input, Int32 n)
        {
            var dt = ConvertFromTime(input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep(input) * n);
                var min = ConvertFromTime(input.Minimum);
                var max = ConvertFromTime(input.Maximum);

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

        #endregion
    }
}
