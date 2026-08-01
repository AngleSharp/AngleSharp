namespace AngleSharp.Dom.Geometry;

using System;

static class GeometryMath
{
    /// <summary>
    /// Provides the member value.
    /// </summary>
    public static Double NaNSafeMinimum(params Double[] values)
    {
        var minimum = values[0];

        for (var i = 0; i < values.Length; i++)
        {
            if (Double.IsNaN(values[i]))
            {
                return Double.NaN;
            }

            if (values[i] < minimum)
            {
                minimum = values[i];
            }
        }

        return minimum;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    public static Double NaNSafeMaximum(params Double[] values)
    {
        var maximum = values[0];

        for (var i = 0; i < values.Length; i++)
        {
            if (Double.IsNaN(values[i]))
            {
                return Double.NaN;
            }

            if (values[i] > maximum)
            {
                maximum = values[i];
            }
        }

        return maximum;
    }
}
