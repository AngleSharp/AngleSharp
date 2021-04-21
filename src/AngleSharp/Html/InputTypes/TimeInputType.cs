namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using System;
    using System.Globalization;

    class TimeInputType : BaseInputType
    {
        #region ctor

        public TimeInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value;
            var date = ConvertFromTime(value.AsSpan());
            var min = ConvertFromTime(Input.Minimum.AsSpan());
            var max = ConvertFromTime(Input.Maximum.AsSpan());
            return CheckTime(current, value, date, min, max);
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromTime(value.AsSpan());

            if (dt.HasValue)
            {
                return dt.Value.Subtract(new DateTime()).TotalMilliseconds;
            }

            return null;
        }

        public override String ConvertFromNumber(Double value)
        {
            var dt = new DateTime().AddMilliseconds(value);
            return ConvertFromDate(dt);
        }

        public override DateTime? ConvertToDate(String value)
        {
            var time = ConvertFromTime(value.AsSpan());

            if (time != null)
            {
                return UnixEpoch.Add(time.Value.Subtract(new DateTime()));
            }

            return null;
        }

        public override String ConvertFromDate(DateTime value)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00},{3:000}", value.Hour, value.Minute, value.Second, value.Millisecond);
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromTime(Input.Value.AsSpan());

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromTime(Input.Minimum.AsSpan());
                var max = ConvertFromTime(Input.Maximum.AsSpan());

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
            return 0.0;
        }

        protected override Double GetDefaultStep()
        {
            return 60.0;
        }

        protected override Double GetStepScaleFactor()
        {
            return 1000.0;
        }

        #endregion

        #region Helper

        protected static DateTime? ConvertFromTime(ReadOnlySpan<char> value)
        {
            if (value.Length > 0)
            {
                var position = 0;
                var ts = ToTime(value, ref position);

                if (ts != null && position == value.Length)
                {
                    return new DateTime(0, DateTimeKind.Utc).Add(ts.Value);
                }
            }

            return null;
        }

        #endregion
    }
}
