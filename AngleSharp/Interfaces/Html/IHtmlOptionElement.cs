using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the option HTML element.
    /// </summary>
    [DomName("HTMLOptionElement")]
    public interface IHtmlOptionElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets if the option is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean Disabled { get; set; }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DomName("form")]
        IHtmlFormElement Form { get; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [DomName("label")]
        String Label { get; set; }

        /// <summary>
        /// Gets or sets if the option is selected by default.
        /// </summary>
        [DomName("defaultSelected")]
        Boolean DefaultSelected { get; set; }

        /// <summary>
        /// Gets or sets if the option is currently selected.
        /// </summary>
        [DomName("selected")]
        Boolean Selected { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }

        /// <summary>
        /// Gets or sets the text of the option.
        /// </summary>
        [DomName("text")]
        String Text { get; set; }

        /// <summary>
        /// Gets the index of the option element.
        /// </summary>
        [DomName("index")]
        Int32 Index { get; }
    }
}
