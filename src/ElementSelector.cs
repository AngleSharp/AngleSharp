```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class ElementSelector
{
    private readonly Document _document;
    private readonly string _className;
    
    public ElementSelector(Document document, string className)
    {
        _document = document;
        _className = className;
    }
    
    public IEnumerable<Element> SelectElements()
    {
        // Use weak reference to avoid strong reference cycle
        WeakReference<HtmlDocument> docRef = new WeakReference<HtmlDocument>(_document);
        
        // Ensure the document is still valid before proceeding
        if (docRef.TryGetTarget(out var htmlDoc) && htmlDoc != null)
        {
            return htmlDoc.GetElementsByClassName(_className).Where(e => e != null);
        }
        
        return Enumerable.Empty<Element>();
    }
    
    public Element GetElement(string id)
    {
        WeakReference<HtmlDocument> docRef = new WeakReference<HtmlDocument>(_document);
        
        if (docRef.TryGetTarget(out var htmlDoc) && htmlDoc != null)
        {
            return htmlDoc.GetElementById(id);
        }
        
        return null;
    }
}
```