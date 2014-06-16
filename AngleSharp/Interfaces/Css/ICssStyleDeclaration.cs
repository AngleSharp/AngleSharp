namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS declaration block, including its underlying state, where
    /// this underlying state depends upon the source of the CSSStyleDeclaration instance.
    /// </summary>
    [DomName("CSSStyleDeclaration")]
    interface ICssStyleDeclaration
    {
        [DomName("cssText")]
        String CssText { get; set; }
        
        [DomName("length")]
        Int32 Length { get; }
        
        [DomName("item")]
        String this[Int32 index] { get; }
        
        [DomName("getPropertyValue")]
        String GetPropertyValue(String property);
        
        [DomName("getPropertyPriority")]
        String GetPropertyPriority(String property);
        
        [DomName("setProperty")]
        void SetProperty(String property, String value, String priority = "");
        
        [DomName("setPropertyValue")]
        void SetPropertyValue(String property, String value);
        
        [DomName("setPropertyPriority")]
        void SetPropertyPriority(String property, String priority);
        
        [DomName("removeProperty")]
        String RemoveProperty(String property);

        [DomName("parentRule")]
        CSSRule ParentRule { get; }
        
        [DomName("cssFloat")]
        String CssFloat { get; set;}
    }
}
