namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a single CSS value.
    /// </summary>
    [DomName("CSSPrimitiveValue")]
    public interface ICssPrimitiveValue : ICssValue
    {
        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        [DomName("primitiveType")]
        UnitType Type { get; }
        
        /// <summary>
        /// Sets the number with a specified unit. If the property attached with
        /// this value can not accept the specified unit or the number, the value
        /// will be unchanged and a DOMException will be raised.
        /// </summary>
        /// <param name="unit">The unit can only be a number unit type.</param>
        /// <param name="value">The number's value.</param>
        [DomName("setFloatValue")]
        void SetNumber(UnitType unit, Double value);
        
        /// <summary>
        /// Gets the value in a specified unit. If this CSS value doesn't
        /// contain a number or can't be converted into the specified unit,
        /// a DOMException is raised.
        /// </summary>
        /// <param name="unit">A unit code to get the number value.</param>
        /// <returns>The value of the number.</returns>
        [DomName("getFloatValue")]
        Double GetNumber(UnitType unit);

        /// <summary>
        /// Sets the string value with the specified unit. If the property attached to
        /// this value can't accept the specified unit or the string value, the value
        /// will be unchanged and a DOMException will be raised.
        /// </summary>
        /// <param name="unit">The unit can only be a string unit type.</param>
        /// <param name="value">The new string value.</param>
        [DomName("setStringValue")]
        void SetString(UnitType unit, String value);
        
        /// <summary>
        /// Gets the string value. If the CSS value doesn't contain a string value,
        /// a DOMException is raised.
        /// </summary>
        /// <returns>The specific string.</returns>
        [DomName("getStringValue")]
        String GetString();
        
        ///// <summary>
        ///// Gets the Counter value. If this CSS value doesn't contain a counter value,
        ////  a DOMException is raised. 
        ///// </summary>
        ///// <returns>The specific counter.</returns>
        //[DomName("getCounterValue")]
        //Counter GetCounter();
        
        ///// <summary>
        ///// Gets the Rect value. If this CSS value doesn't contain a rect value, a
        ///// DOMException is raised.
        ///// </summary>
        ///// <returns>The specific rectangle.</returns>
        //[DomName("getRectValue")]
        //Rect GetRectangle();
        
        /// <summary>
        /// Gets the RGB color. If this CSS value doesn't contain a RGB color value, 
        /// a DOMException is raised. 
        /// </summary>
        /// <returns>The specific color.</returns>
        [DomName("getRGBColorValue")]
        Color GetColor();
    }
}
