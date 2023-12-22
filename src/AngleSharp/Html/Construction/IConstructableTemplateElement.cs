namespace AngleSharp.Html.Construction;

internal interface IConstructableTemplateElement : IConstructableElement
{
    void PopulateFragment();
}