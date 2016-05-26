namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    interface ICssPropertyFactory
    {
        CssProperty Create(String name);
        CssProperty CreateFont(String name);
        CssProperty CreateLonghand(String name);
        CssProperty[] CreateLonghandsFor(String name);
        CssShorthandProperty CreateShorthand(String name);
        CssProperty CreateViewport(String name);
        String[] GetLonghands(String name);
        IEnumerable<String> GetShorthands(String name);
        Boolean IsAnimatable(String name);
        Boolean IsLonghand(String name);
        Boolean IsShorthand(String name);
    }
}
