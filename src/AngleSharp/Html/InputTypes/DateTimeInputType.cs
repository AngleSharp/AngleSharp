﻿namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    class DatetimeInputType : BaseInputType
    {
        #region ctor

        public DatetimeInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion
        
        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value;
            var date = ConvertFromDateTime(value);
            var min = ConvertFromDateTime(Input.Minimum);
            var max = ConvertFromDateTime(Input.Maximum);
            return CheckTime(current, value, date, min, max);
        }

        public override Double? ConvertToNumber(String value)
        {
            var dt = ConvertFromDateTime(value);

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
            return ConvertFromDateTime(value);
        }

        public override String ConvertFromDate(DateTime value)
        {
            var date = String.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", value.Year, value.Month, value.Day);
            var time = String.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00},{3:000}", value.Hour, value.Minute, value.Second, value.Millisecond);
            return String.Concat(date, "T", time, "Z");
        }

        public override void DoStep(Int32 n)
        {
            var dt = ConvertFromDateTime(Input.Value);

            if (dt.HasValue)
            {
                var date = dt.Value.AddMilliseconds(GetStep() * n);
                var min = ConvertFromDateTime(Input.Minimum);
                var max = ConvertFromDateTime(Input.Maximum);

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

        protected static DateTime? ConvertFromDateTime(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var position = FetchDigits(value);

                if (PositionIsValidForDateTime(value, position))
                {
                    var yearString = value.Substring(0, position);
                    var year = Int32.Parse(yearString, CultureInfo.InvariantCulture);
                    var monthString = value.Substring(position + 1, 2);
                    var month = Int32.Parse(monthString, CultureInfo.InvariantCulture);
                    var dayString = value.Substring(position + 4, 2);
                    var day = Int32.Parse(dayString, CultureInfo.InvariantCulture);
                    position += 6;

                    if (IsLegalDay(day, month, year) && IsTimeSeparator(value[position]))
                    {
                        var requireOffset = value[position++] == ' ';
                        var ts = ToTime(value, ref position);
                        var dt = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);

                        if (ts != null)
                        {
                            dt = dt.Add(ts.Value);

                            if (position == value.Length)
                            {
                                if (requireOffset)
                                {
                                    return null;
                                }

                                return dt;
                            }

                            if (value[position] != 'Z')
                            {
                                if (IsLegalPosition(value, position))
                                {
                                    var hoursString = value.Substring(position + 1, 2);
                                    var hours = Int32.Parse(hoursString, CultureInfo.InvariantCulture);
                                    var minutesString = value.Substring(position + 4, 2);
                                    var minutes = Int32.Parse(minutesString, CultureInfo.InvariantCulture);
                                    var offset = new TimeSpan(hours, minutes, 0);

                                    if (value[position] == '+')
                                    {
                                        dt = dt.Add(offset);
                                    }
                                    else if (value[position] == '-')
                                    {
                                        dt = dt.Subtract(offset);
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else if (position + 1 != value.Length)
                            {
                                return null;
                            }
                            else
                            {
                                dt = dt.ToUniversalTime();
                            }

                            return dt;
                        }
                    }
                }
            }

            return null;
        }

        private static Boolean IsLegalPosition(String value, Int32 position)
        {
            return position == value.Length - 6 &&
                    value[position + 1].IsDigit() &&
                    value[position + 2].IsDigit() &&
                    value[position + 3] == Symbols.Colon &&
                    value[position + 4].IsDigit() &&
                    value[position + 5].IsDigit();
        }

        #endregion
    }
}
