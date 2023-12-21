// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics.CodeAnalysis;
// using System.Globalization;
// using System.IO;
// using System.Linq;
// using AngleSharp;
//
// using AngleSharp.Common;
// using AngleSharp.Css;
//
// using AngleSharp.Dom;
//
// using AngleSharp.Html.InputTypes;
// using AngleSharp.Html.LinkRels;
//
// using AngleSharp.Svg.Dom;
// using AngleSharp.Text;
//
//
// // rewrite this file to use the new AngleSharp/ReadOnly/ReadOnlyHtml*Element.cs files
// // and the new AngleSharp/Html/Dom/Internal/Html*Element.cs files
//
// sealed class ReadOnlyHtmlAddressElement : ReadOnlyHtmlElement
// {
//     public ReadOnlyHtmlAddressElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Address, prefix, NodeFlags.Special)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlAnchorElement : ReadOnlyHtmlUrlBaseElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new anchor element.
//     /// </summary>
//     // public HtmlAnchorElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.A, prefix, NodeFlags.HtmlFormatting)
//     // {
//     // }
//
//     public ReadOnlyHtmlAnchorElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.A, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the character encoding for the target resource.
//     /// </summary>
//     public StringOrMemory Charset
//     {
//         get => this.GetOwnAttribute(AttributeNames.Charset);
//         set => this.SetOwnAttribute(AttributeNames.Charset, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the anchor name.
//     /// </summary>
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the text of the anchor tag (same as TextContent).
//     /// </summary>
//     public StringOrMemory Text
//     {
//         get => TextContent;
//         set => TextContent = value;
//     }
//
//     #endregion
//
// }
//
// sealed class ReadOnlyHtmlAppletElement : ReadOnlyHtmlElement
// {
//     // public HtmlAppletElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Applet, prefix, NodeFlags.Special | NodeFlags.Scoped)
//     // {
//     // }
//
//     public ReadOnlyHtmlAppletElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Applet, prefix, NodeFlags.Special | NodeFlags.Scoped)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlAreaElement : ReadOnlyHtmlUrlBaseElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new area element.
//     /// </summary>
//     // public HtmlAreaElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Area, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlAreaElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Area, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the alternative text for the element.
//     /// </summary>
//     public StringOrMemory AlternativeText
//     {
//         get => this.GetOwnAttribute(AttributeNames.Alt);
//         set => this.SetOwnAttribute(AttributeNames.Alt, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the coordinates to define the hot-spot region.
//     /// </summary>
//     public StringOrMemory Coordinates
//     {
//         get => this.GetOwnAttribute(AttributeNames.Coords);
//         set => this.SetOwnAttribute(AttributeNames.Coords, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the shape of the hot-spot, limited to known values.
//     /// The known values are: circle, default. poly, rect. The missing
//     /// value is rect.
//     /// </summary>
//     public StringOrMemory Shape
//     {
//         get => this.GetOwnAttribute(AttributeNames.Shape);
//         set => this.SetOwnAttribute(AttributeNames.Shape, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlAudioElement : ReadOnlyHtmlMediaElement<IAudioInfo>
// {
//     #region Fields
//
//     private IAudioTrackList? _audios;
//
//     #endregion
//
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML audio element.
//     /// </summary>
//     // public HtmlAudioElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Audio, prefix)
//     // {
//     //     _audios = null;
//     // }
//
//     public ReadOnlyHtmlAudioElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Audio, prefix)
//     {
//         _audios = null;
//     }
//
//     #endregion
//
//     #region Properties
//
//     public override IAudioTrackList? AudioTracks => _audios;
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlBaseElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlBaseElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlBaseElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Href
//     {
//         get => this.GetOwnAttribute(AttributeNames.Href);
//         set => this.SetOwnAttribute(AttributeNames.Href, value);
//     }
//
//     public StringOrMemory Target
//     {
//         get => this.GetOwnAttribute(AttributeNames.Target);
//         set => this.SetOwnAttribute(AttributeNames.Target, value);
//     }
//
//     #endregion
//
//     #region Internal Methods
//
//     internal override void SetupElement()
//     {
//         base.SetupElement();
//
//         var href = this.GetOwnAttribute(AttributeNames.Href);
//
//         if (!href.IsNullOrEmpty)
//         {
//             UpdateUrl(href);
//         }
//     }
//
//     internal void UpdateUrl(StringOrMemory url)
//     {
//         // Owner.BaseUrl = new Url(Owner.DocumentUrl, url);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlBaseFontElement : ReadOnlyHtmlElement
// {
//     // public HtmlBaseFontElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.BaseFont, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlBaseFontElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.BaseFont, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlBgsoundElement : ReadOnlyHtmlElement
// {
//     // public HtmlBgsoundElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Bgsound, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlBgsoundElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Bgsound, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlBigElement : ReadOnlyHtmlElement
// {
//     // public HtmlBigElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Big, prefix, NodeFlags.HtmlFormatting)
//     // {
//     // }
//
//     public ReadOnlyHtmlBigElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Big, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlBodyElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlBodyElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Body, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed)
//     // {
//     // }
//
//     public ReadOnlyHtmlBodyElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Body, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory ALink
//     {
//         get => this.GetOwnAttribute(AttributeNames.Alink);
//         set => this.SetOwnAttribute(AttributeNames.Alink, value);
//     }
//
//     public StringOrMemory Background
//     {
//         get => this.GetOwnAttribute(AttributeNames.Background);
//         set => this.SetOwnAttribute(AttributeNames.Background, value);
//     }
//
//     public StringOrMemory BgColor
//     {
//         get => this.GetOwnAttribute(AttributeNames.BgColor);
//         set => this.SetOwnAttribute(AttributeNames.BgColor, value);
//     }
//
//     public StringOrMemory Link
//     {
//         get => this.GetOwnAttribute(AttributeNames.Link);
//         set => this.SetOwnAttribute(AttributeNames.Link, value);
//     }
//
//     public StringOrMemory Text
//     {
//         get => this.GetOwnAttribute(AttributeNames.Text);
//         set => this.SetOwnAttribute(AttributeNames.Text, value);
//     }
//
//     public StringOrMemory VLink
//     {
//         get => this.GetOwnAttribute(AttributeNames.Vlink);
//         set => this.SetOwnAttribute(AttributeNames.Vlink, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlBoldElement : ReadOnlyHtmlElement
// {
//     // public HtmlBoldElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.B, prefix, NodeFlags.HtmlFormatting)
//     // {
//     // }
//
//     public ReadOnlyHtmlBoldElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.B, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlBreakRowElement : ReadOnlyHtmlElement
// {
//     // public HtmlBreakRowElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Br, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlBreakRowElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Br, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlButtonElement : ReadOnlyHtmlFormControlElement
// {
//     #region ctor
//
//     // /// <summary>
//     // /// Creates a new HTML button element.
//     // /// </summary>
//     // public HtmlButtonElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Button, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlButtonElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Button, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the behavior of the button.
//     /// </summary>
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type).OrElse(InputTypeNames.Submit);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the URI of a resource that processes information submitted by the button.
//     /// If specified, this attribute overrides the action attribute of the form element that owns this element.
//     /// </summary>
//     public StringOrMemory FormAction
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormAction);
//         set => this.SetOwnAttribute(AttributeNames.FormAction, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
//     /// attribute overrides the enctype attribute of the form element that owns this element.
//     /// </summary>
//     public StringOrMemory FormEncType
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormEncType);
//         set => this.SetOwnAttribute(AttributeNames.FormEncType, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
//     /// overrides the method attribute of the form element that owns this element.
//     /// </summary>
//     public StringOrMemory FormMethod
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormMethod);
//         set => this.SetOwnAttribute(AttributeNames.FormMethod, value);
//     }
//
//     /// <summary>
//     /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
//     /// overrides the enctype attribute of the form element that owns this element.
//     /// </summary>
//     public Boolean FormNoValidate
//     {
//         get => this.GetBoolAttribute(AttributeNames.FormNoValidate);
//         set => this.SetBoolAttribute(AttributeNames.FormNoValidate, value);
//     }
//
//     /// <summary>
//     /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
//     /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
//     /// </summary>
//     [AllowNull]
//     public StringOrMemory FormTarget
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormTarget);
//         set => this.SetOwnAttribute(AttributeNames.FormTarget, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the current value of the control.
//     /// </summary>
//     public StringOrMemory Value
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value);
//         set => this.SetOwnAttribute(AttributeNames.Value, value);
//     }
//
//     #endregion
//
//     #region Design properties
//
//     /// <summary>
//     /// Gets or sets if the link has been visited.
//     /// </summary>
//     internal Boolean IsVisited { get; set; }
//
//     /// <summary>
//     /// Gets or sets if the link is currently active.
//     /// </summary>
//     internal Boolean IsActive { get; set; }
//
//     #endregion
//
//     #region Methods
//
//     public override async void DoClick()
//     {
//         var cancelled = await IsClickedCancelled().ConfigureAwait(false);
//         var form = Form;
//
//         if (!cancelled && form != null)
//         {
//             var type = Type;
//
//             if (type.Is(InputTypeNames.Submit))
//             {
//                 await form.SubmitAsync(this).ConfigureAwait(false);
//             }
//             else if (type.Is(InputTypeNames.Reset))
//             {
//                 form.Reset();
//             }
//         }
//     }
//
//     #endregion
//
//     #region Helper
//
//     protected override Boolean CanBeValidated()
//     {
//         return Type.Is(InputTypeNames.Submit) && !this.HasDataListAncestor();
//     }
//
//     internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
//     {
//         var type = Type;
//
//         if (Object.ReferenceEquals(this, submitter) && type.IsOneOf(InputTypeNames.Submit, InputTypeNames.Reset))
//         {
//             dataSet.Append(Name!, Value, type);
//         }
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlCanvasElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML canvas element.
//     /// </summary>
//     public ReadOnlyHtmlCanvasElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Canvas, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the displayed width of the canvas element.
//     /// </summary>
//     public Int32 Width
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(300);
//         set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the displayed height of the canvas element.
//     /// </summary>
//     public Int32 Height
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(150);
//         set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlCodeElement : ReadOnlyHtmlElement
// {
//     public ReadOnlyHtmlCodeElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Code, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlDataElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlDataElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Data, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlDataElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Data, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Value
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value);
//         set => this.SetOwnAttribute(AttributeNames.Value, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlDataListElement : ReadOnlyHtmlElement
// {
//     #region Fields
//
//     // private HtmlCollection<IHtmlOptionElement>? _options;
//
//     #endregion
//
//     #region ctor
//
//     // public HtmlDataListElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Datalist, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlDataListElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Datalist, prefix)
//     {
//     }
//
//     #endregion
//
//     // #region Properties
//     //
//     // public IHtmlCollection<IHtmlOptionElement> Options => _options ??= new HtmlCollection<IHtmlOptionElement>(this);
//     //
//     // #endregion
// }
//
// sealed class ReadOnlyHtmlDefinitionListElement : ReadOnlyHtmlElement
// {
//     // public HtmlDefinitionListElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Dl, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlDefinitionListElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Dl, prefix, NodeFlags.Special)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlDetailsElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlDetailsElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Details, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlDetailsElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Details, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Boolean IsOpen
//     {
//         get => this.GetBoolAttribute(AttributeNames.Open);
//         set => this.SetBoolAttribute(AttributeNames.Open, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlDialogElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlDialogElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Dialog, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlDialogElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Dialog, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Boolean Open
//     {
//         get => this.GetBoolAttribute(AttributeNames.Open);
//         set => this.SetBoolAttribute(AttributeNames.Open, value);
//     }
//
//
//
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlDirectoryElement : ReadOnlyHtmlElement
// {
//     // public HtmlDirectoryElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Dir, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlDirectoryElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Dir, prefix, NodeFlags.Special)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlDivElement : ReadOnlyHtmlElement
// {
//     // public HtmlDivElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Div, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlDivElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Div, prefix, NodeFlags.Special)
//     {
//     }
// }
//
// abstract class ReadOnlyHtmlUrlBaseElement : ReadOnlyHtmlElement
//     {
//         #region ctor
//
//         public ReadOnlyHtmlUrlBaseElement(ReadOnlyDocument owner, StringOrMemory name, StringOrMemory prefix, NodeFlags flags)
//             : base(owner, name, prefix, flags)
//         {
//         }
//
//         #endregion
//
//         #region Properties
//
//         public StringOrMemory Download
//         {
//             get => this.GetOwnAttribute(AttributeNames.Download);
//             set => this.SetOwnAttribute(AttributeNames.Download, value);
//         }
//
//         public StringOrMemory Href
//         {
//             get => this.GetOwnAttribute(AttributeNames.Href);
//             // set => SetAttribute(AttributeNames.Href, value);
//         }
//
//         // public StringOrMemory Hash
//         // {
//         //     get => GetLocationPart(m => m.Hash)!;
//         //     set => SetLocationPart(m => m.Hash = value);
//         // }
//         //
//         // public StringOrMemory Host
//         // {
//         //     get => GetLocationPart(m => m.Host)!;
//         //     set => SetLocationPart(m => m.Host = value);
//         // }
//         //
//         // public StringOrMemory HostName
//         // {
//         //     get => GetLocationPart(m => m.HostName)!;
//         //     set => SetLocationPart(m => m.HostName = value);
//         // }
//         //
//         // public StringOrMemory PathName
//         // {
//         //     get => GetLocationPart(m => m.PathName)!;
//         //     set => SetLocationPart(m => m.PathName = value);
//         // }
//         //
//         // public StringOrMemory Port
//         // {
//         //     get => GetLocationPart(m => m.Port)!;
//         //     set => SetLocationPart(m => m.Port = value);
//         // }
//         //
//         // public StringOrMemory Protocol
//         // {
//         //     get => GetLocationPart(m => m.Protocol)!;
//         //     set => SetLocationPart(m => m.Protocol = value);
//         // }
//         //
//         // public StringOrMemory UserName
//         // {
//         //     get => GetLocationPart(m => m.UserName);
//         //     set => SetLocationPart(m => m.UserName = value);
//         // }
//         //
//         // public StringOrMemory Password
//         // {
//         //     get => GetLocationPart(m => m.Password);
//         //     set => SetLocationPart(m => m.Password = value);
//         // }
//         //
//         // public StringOrMemory Search
//         // {
//         //     get => GetLocationPart(m => m.Search)!;
//         //     // set => SetLocationPart(m => m.Search = value);
//         // }
//
//         public StringOrMemory Origin => GetLocationPart(m => m.Origin);
//
//         public StringOrMemory TargetLanguage
//         {
//             get => this.GetOwnAttribute(AttributeNames.HrefLang);
//             set => this.SetOwnAttribute(AttributeNames.HrefLang, value);
//         }
//
//         public StringOrMemory Media
//         {
//             get => this.GetOwnAttribute(AttributeNames.Media);
//             set => this.SetOwnAttribute(AttributeNames.Media, value);
//         }
//
//         public StringOrMemory Relation
//         {
//             get => this.GetOwnAttribute(AttributeNames.Rel);
//             set => this.SetOwnAttribute(AttributeNames.Rel, value);
//         }
//
//         // public ITokenList RelationList
//         // {
//         //     get
//         //     {
//         //         if (_relList is null)
//         //         {
//         //             _relList = new TokenList(this.GetOwnAttribute(AttributeNames.Rel));
//         //             _relList.Changed += value => UpdateAttribute(AttributeNames.Rel, value);
//         //         }
//         //
//         //         return _relList;
//         //     }
//         // }
//         //
//         // public ISettableTokenList Ping
//         // {
//         //     get
//         //     {
//         //         if (_ping is null)
//         //         {
//         //             _ping = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Ping));
//         //             _ping.Changed += value => UpdateAttribute(AttributeNames.Ping, value);
//         //         }
//         //
//         //         return _ping;
//         //     }
//         // }
//
//         public StringOrMemory Target
//         {
//             get => this.GetOwnAttribute(AttributeNames.Target);
//             set => this.SetOwnAttribute(AttributeNames.Target, value);
//         }
//
//         public StringOrMemory Type
//         {
//             get => this.GetOwnAttribute(AttributeNames.Type);
//             set => this.SetOwnAttribute(AttributeNames.Type, value);
//         }
//
//         #endregion
//
//         #region Helpers
//
//         private StringOrMemory GetLocationPart(Func<ILocation, String?> getter)
//         {
//             var href = this.GetOwnAttribute(AttributeNames.Href);
//             var url = href != null ? new Url(BaseUrl!, href) : null;
//
//             if (url != null && !url.IsInvalid)
//             {
//                 var location = new Location(url);
//                 return getter.Invoke(location);
//             }
//
//             return String.Empty;
//         }
//
//         // private void SetLocationPart(Action<ILocation> setter)
//         // {
//         //     var href = this.GetOwnAttribute(AttributeNames.Href);
//         //     var url = href != null ? new Url(BaseUrl!, href) : null;
//         //
//         //     if (url is null || url.IsInvalid)
//         //     {
//         //         url = new Url(BaseUrl!);
//         //     }
//         //
//         //     var location = new Location(url);
//         //     setter.Invoke(location);
//         //     this.SetOwnAttribute(AttributeNames.Href, location.Href);
//         // }
//
//         #endregion
//     }
//
// sealed class ReadOnlyHtmlEmbedElement : ReadOnlyHtmlElement
// {
//     #region Fields
//
//     #endregion
//
//     #region ctor
//
//     // public HtmlEmbedElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlEmbedElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Source
//     {
//         get => this.GetOwnAttribute(AttributeNames.Src);
//         set => this.SetOwnAttribute(AttributeNames.Src, value);
//     }
//
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     public StringOrMemory DisplayWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width);
//         set => this.SetOwnAttribute(AttributeNames.Width, value);
//     }
//
//     public StringOrMemory DisplayHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height);
//         set => this.SetOwnAttribute(AttributeNames.Height, value);
//     }
//
//     #endregion
//
// }
//
// sealed class ReadOnlyHtmlEmphasizeElement : ReadOnlyHtmlElement
// {
//     // public HtmlEmphasizeElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Em, prefix, NodeFlags.HtmlFormatting)
//     // {
//     // }
//
//     public ReadOnlyHtmlEmphasizeElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Em, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlFieldSetElement : ReadOnlyHtmlFormControlElement
// {
//     #region Fields
//
//     private HtmlFormControlsCollection? _elements;
//
//     #endregion
//
//     #region ctor
//
//     // public HtmlFieldSetElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Fieldset, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlFieldSetElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Fieldset, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Type => TagNames.Fieldset;
//
//     public HtmlFormControlsCollection Elements =>
//         _elements ?? (_elements = new HtmlFormControlsCollection(Form!, this));
//
//     #endregion
//
//     #region Methods
//
//     protected override Boolean IsFieldsetDisabled()
//     {
//         return false;
//     }
//
//     protected override Boolean CanBeValidated()
//     {
//         return true;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlFontElement : ReadOnlyHtmlElement
// {
//     // public HtmlFontElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Font, prefix, NodeFlags.HtmlFormatting)
//     // {
//     // }
//
//     public ReadOnlyHtmlFontElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Font, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlFormElement : ReadOnlyHtmlElement
// {
//     #region Fields
//
//     private HtmlFormControlsCollection? _elements;
//
//     #endregion
//
//     #region ctor
//
//     // public HtmlFormElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Form, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlFormElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Form, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Index
//
//     public IElement? this[Int32 index] => Elements[index];
//
//     public IElement? this[String name] => Elements[name];
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     public Int32 Length => Elements.Length;
//
//     public HtmlFormControlsCollection Elements => _elements ?? (_elements = new HtmlFormControlsCollection(this));
//
//     // IHtmlFormControlsCollection IHtmlFormElement.Elements => Elements;
//
//     public StringOrMemory AcceptCharset
//     {
//         get => this.GetOwnAttribute(AttributeNames.AcceptCharset);
//         set => this.SetOwnAttribute(AttributeNames.AcceptCharset, value);
//     }
//
//     public StringOrMemory Action
//     {
//         get => this.GetOwnAttribute(AttributeNames.Action);
//         set => this.SetOwnAttribute(AttributeNames.Action, value);
//     }
//
//     public StringOrMemory Autocomplete
//     {
//         get => this.GetOwnAttribute(AttributeNames.AutoComplete);
//         set => this.SetOwnAttribute(AttributeNames.AutoComplete, value);
//     }
//
//     [AllowNull]
//     public StringOrMemory Enctype
//     {
//         get => this.GetOwnAttribute(AttributeNames.Enctype);
//         set => this.SetOwnAttribute(AttributeNames.Enctype, value);
//     }
//
//     public StringOrMemory Encoding
//     {
//         get => Enctype;
//         set => Enctype = value;
//     }
//
//     public StringOrMemory Method
//     {
//         get => this.GetOwnAttribute(AttributeNames.Method);
//         set => this.SetOwnAttribute(AttributeNames.Method, value);
//     }
//
//     public Boolean NoValidate
//     {
//         get => this.GetBoolAttribute(AttributeNames.NoValidate);
//         set => this.SetBoolAttribute(AttributeNames.NoValidate, value);
//     }
//
//     public StringOrMemory Target
//     {
//         get => this.GetOwnAttribute(AttributeNames.Target);
//         set => this.SetOwnAttribute(AttributeNames.Target, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlFrameElement : ReadOnlyHtmlFrameElementBase
// {
//     #region ctor
//
//     // public HtmlFrameElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Frame, prefix, NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlFrameElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Frame, prefix, NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Boolean NoResize
//     {
//         get => this.GetOwnAttribute(AttributeNames.NoResize).ToBoolean(false);
//         set => this.SetOwnAttribute(AttributeNames.NoResize, value.ToString());
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlFrameSetElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlFrameSetElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Frameset, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlFrameSetElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Frameset, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Int32 Columns
//     {
//         get => this.GetOwnAttribute(AttributeNames.Cols).ToInteger(1);
//         set => this.SetOwnAttribute(AttributeNames.Cols, value.ToString());
//     }
//
//     public Int32 Rows
//     {
//         get => this.GetOwnAttribute(AttributeNames.Rows).ToInteger(1);
//         set => this.SetOwnAttribute(AttributeNames.Rows, value.ToString());
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlHeadElement : ReadOnlyHtmlElement
// {
//     // public HtmlHeadElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Head, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlHeadElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Head, prefix, NodeFlags.Special)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlHeadingElement : ReadOnlyHtmlElement
// {
//     // public HtmlHeadingElement(Document owner, String? name = null, String? prefix = null)
//     //     : base(owner, name ?? TagNames.H1, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlHeadingElement(ReadOnlyDocument owner, StringOrMemory name = default, StringOrMemory prefix = default)
//         : base(owner, name.OrElse(TagNames.H1), prefix, NodeFlags.Special)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlHrElement : ReadOnlyHtmlElement
// {
//     // public HtmlHrElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlHrElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlHtmlElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlHtmlElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Html, prefix,
//     //         NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped |
//     //         NodeFlags.HtmlTableSectionScoped)
//     // {
//     // }
//
//     public ReadOnlyHtmlHtmlElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Html, prefix,
//             NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped |
//             NodeFlags.HtmlTableSectionScoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Manifest
//     {
//         get => this.GetOwnAttribute(AttributeNames.Manifest);
//         set => this.SetOwnAttribute(AttributeNames.Manifest, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlIFrameElement : ReadOnlyHtmlFrameElementBase
// {
//     #region Fields
//
//     private SettableTokenList? _sandbox;
//
//     #endregion
//
//     #region ctor
//
//     // public HtmlIFrameElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Iframe, prefix, NodeFlags.LiteralText)
//     // {
//     // }
//
//     public ReadOnlyHtmlIFrameElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Iframe, prefix, NodeFlags.LiteralText)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Alignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     public StringOrMemory ContentHtml
//     {
//         get => this.GetOwnAttribute(AttributeNames.SrcDoc);
//         set => this.SetOwnAttribute(AttributeNames.SrcDoc, value);
//     }
//
//     public ISettableTokenList Sandbox
//     {
//         get
//         {
//             if (_sandbox is null)
//             {
//                 _sandbox = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sandbox));
//                 _sandbox.Changed += value => UpdateAttribute(AttributeNames.Sandbox, value);
//             }
//
//             return _sandbox;
//         }
//     }
//
//     public Boolean IsSeamless
//     {
//         get => this.GetBoolAttribute(AttributeNames.SrcDoc);
//         set => this.SetBoolAttribute(AttributeNames.SrcDoc, value);
//     }
//
//     public Boolean IsFullscreenAllowed
//     {
//         get => this.GetBoolAttribute(AttributeNames.AllowFullscreen);
//         set => this.SetBoolAttribute(AttributeNames.AllowFullscreen, value);
//     }
//
//     public Boolean IsPaymentRequestAllowed
//     {
//         get => this.GetBoolAttribute(AttributeNames.AllowPaymentRequest);
//         set => this.SetBoolAttribute(AttributeNames.AllowPaymentRequest, value);
//     }
//
//     public StringOrMemory ReferrerPolicy
//     {
//         get => this.GetOwnAttribute(AttributeNames.ReferrerPolicy);
//         set => this.SetOwnAttribute(AttributeNames.ReferrerPolicy, value);
//     }
//
//     #endregion
//
//     #region Internal Methods
//
//     internal override String GetContentHtml()
//     {
//         return ContentHtml!;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlImageElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlImageElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlImageElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory SourceSet
//     {
//         get => this.GetOwnAttribute(AttributeNames.SrcSet);
//         set => this.SetOwnAttribute(AttributeNames.SrcSet, value);
//     }
//
//     public StringOrMemory Sizes
//     {
//         get => this.GetOwnAttribute(AttributeNames.Sizes);
//         set => this.SetOwnAttribute(AttributeNames.Sizes, value);
//     }
//
//     public StringOrMemory Source
//     {
//         get => this.GetOwnAttribute(AttributeNames.Src);
//         set => this.SetOwnAttribute(AttributeNames.Src, value);
//     }
//
//     public StringOrMemory AlternativeText
//     {
//         get => this.GetOwnAttribute(AttributeNames.Alt);
//         set => this.SetOwnAttribute(AttributeNames.Alt, value);
//     }
//
//     public StringOrMemory CrossOrigin
//     {
//         get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
//         set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
//     }
//
//     public StringOrMemory UseMap
//     {
//         get => this.GetOwnAttribute(AttributeNames.UseMap);
//         set => this.SetOwnAttribute(AttributeNames.UseMap, value);
//     }
//
//     public Int32 DisplayWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
//     }
//
//     public Int32 DisplayHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
//     }
//
//     public Boolean IsMap
//     {
//         get => this.GetBoolAttribute(AttributeNames.IsMap);
//         set => this.SetBoolAttribute(AttributeNames.IsMap, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlInputElement : ReadOnlyHtmlTextFormControlElement
// {
//     #region Fields
//
//     private BaseInputType? _type;
//     private Boolean? _checked;
//
//     #endregion
//
//     #region ctor
//
//     // public HtmlInputElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Input, prefix, NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlInputElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Input, prefix, NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public override StringOrMemory DefaultValue
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value);
//         set => this.SetOwnAttribute(AttributeNames.Value, value);
//     }
//
//     public Boolean IsDefaultChecked
//     {
//         get => this.GetBoolAttribute(AttributeNames.Checked);
//         set => this.SetBoolAttribute(AttributeNames.Checked, value);
//     }
//
//     public Boolean IsChecked
//     {
//         get => _checked ?? IsDefaultChecked;
//         set => _checked = value;
//     }
//
//     public StringOrMemory Type
//     {
//         get => _type!.Name;
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     public Boolean IsIndeterminate { get; set; }
//
//     public Boolean IsMultiple
//     {
//         get => this.GetBoolAttribute(AttributeNames.Multiple);
//         set => this.SetBoolAttribute(AttributeNames.Multiple, value);
//     }
//
//     public DateTime? ValueAsDate
//     {
//         get => _type!.ConvertToDate(Value);
//         set
//         {
//             if (value is null)
//             {
//                 Value = String.Empty;
//             }
//             else
//             {
//                 Value = _type!.ConvertFromDate(value.Value);
//             }
//         }
//     }
//
//     public Double ValueAsNumber
//     {
//         get => _type!.ConvertToNumber(Value) ?? Double.NaN;
//         set
//         {
//             if (Double.IsInfinity(value))
//             {
//                 throw new DomException(DomError.TypeMismatch);
//             }
//
//             if (Double.IsNaN(value))
//             {
//                 Value = String.Empty;
//             }
//             else
//             {
//                 Value = _type!.ConvertFromNumber(value);
//             }
//         }
//     }
//
//     public StringOrMemory FormAction
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormAction) ?? Owner?.DocumentUri;
//         set => this.SetOwnAttribute(AttributeNames.FormAction, value);
//     }
//
//     public StringOrMemory FormEncType
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormEncType).ToEncodingType();
//         set => this.SetOwnAttribute(AttributeNames.FormEncType, value);
//     }
//
//     public StringOrMemory FormMethod
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormMethod).ToFormMethod();
//         set => this.SetOwnAttribute(AttributeNames.FormMethod, value);
//     }
//
//     public Boolean FormNoValidate
//     {
//         get => this.GetBoolAttribute(AttributeNames.FormNoValidate);
//         set => this.SetBoolAttribute(AttributeNames.FormNoValidate, value);
//     }
//
//     [AllowNull]
//     public StringOrMemory FormTarget
//     {
//         get => this.GetOwnAttribute(AttributeNames.FormTarget);
//         set => this.SetOwnAttribute(AttributeNames.FormTarget, value);
//     }
//
//     public StringOrMemory Accept
//     {
//         get => this.GetOwnAttribute(AttributeNames.Accept);
//         set => this.SetOwnAttribute(AttributeNames.Accept, value);
//     }
//
//     public Alignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Left);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     public StringOrMemory AlternativeText
//     {
//         get => this.GetOwnAttribute(AttributeNames.Alt);
//         set => this.SetOwnAttribute(AttributeNames.Alt, value);
//     }
//
//     public StringOrMemory Autocomplete
//     {
//         get => this.GetOwnAttribute(AttributeNames.AutoComplete);
//         set => this.SetOwnAttribute(AttributeNames.AutoComplete, value);
//     }
//
//     public IFileList? Files
//     {
//         get
//         {
//             var type = _type as FileInputType;
//             return type?.Files;
//         }
//     }
//
//     public IHtmlDataListElement? List
//     {
//         get
//         {
//             var list = this.GetOwnAttribute(AttributeNames.List);
//
//             if (list is { Length: > 0 })
//             {
//                 var element = Owner?.GetElementById(list);
//                 return element as IHtmlDataListElement;
//             }
//
//             return null;
//         }
//     }
//
//     public StringOrMemory Maximum
//     {
//         get => this.GetOwnAttribute(AttributeNames.Max);
//         set => this.SetOwnAttribute(AttributeNames.Max, value);
//     }
//
//     public StringOrMemory Minimum
//     {
//         get => this.GetOwnAttribute(AttributeNames.Min);
//         set => this.SetOwnAttribute(AttributeNames.Min, value);
//     }
//
//     public StringOrMemory Pattern
//     {
//         get => this.GetOwnAttribute(AttributeNames.Pattern);
//         set => this.SetOwnAttribute(AttributeNames.Pattern, value);
//     }
//
//     public Int32 Size
//     {
//         get => this.GetOwnAttribute(AttributeNames.Size).ToInteger(20);
//         set => this.SetOwnAttribute(AttributeNames.Size, value.ToString());
//     }
//
//     public StringOrMemory Source
//     {
//         get => this.GetOwnAttribute(AttributeNames.Src);
//         set => this.SetOwnAttribute(AttributeNames.Src, value);
//     }
//
//     public StringOrMemory Step
//     {
//         get => this.GetOwnAttribute(AttributeNames.Step);
//         set => this.SetOwnAttribute(AttributeNames.Step, value);
//     }
//
//     public StringOrMemory UseMap
//     {
//         get => this.GetOwnAttribute(AttributeNames.UseMap);
//         set => this.SetOwnAttribute(AttributeNames.UseMap, value);
//     }
//
//     public Int32 DisplayWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth);
//         set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
//     }
//
//     public Int32 DisplayHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight);
//         set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
//     }
//
//     public Int32 OriginalWidth
//     {
//         get
//         {
//             var type = _type as ImageInputType;
//             return type?.Width ?? 0;
//         }
//     }
//
//     public Int32 OriginalHeight
//     {
//         get
//         {
//             var type = _type as ImageInputType;
//             return type?.Height ?? 0;
//         }
//     }
//
//     #endregion
//
//     #region Design properties
//
//     internal Boolean IsVisited { get; set; }
//
//     internal Boolean IsActive { get; set; }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlIsIndexElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlIsIndexElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.IsIndex, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlIsIndexElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.IsIndex, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     // public IHtmlFormElement? Form { get; internal set; }
//
//     public StringOrMemory Prompt
//     {
//         get => this.GetOwnAttribute(AttributeNames.Prompt);
//         set => this.SetOwnAttribute(AttributeNames.Prompt, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlItalicElement : ReadOnlyHtmlElement
// {
//     // public HtmlItalicElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.I, prefix, NodeFlags.HtmlFormatting)
//     // {
//     // }
// }
//
// sealed class ReadOnlyHtmlKeygenElement : ReadOnlyHtmlFormControlElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML keygen element.
//     /// </summary>
//     // public HtmlKeygenElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Keygen, prefix, NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlKeygenElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Keygen, prefix, NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the challenge attribute.
//     /// </summary>
//     public StringOrMemory Challenge
//     {
//         get => this.GetOwnAttribute(AttributeNames.Challenge);
//         set => this.SetOwnAttribute(AttributeNames.Challenge, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the type of key used.
//     /// </summary>
//     public StringOrMemory KeyEncryption
//     {
//         get => this.GetOwnAttribute(AttributeNames.Keytype);
//         set => this.SetOwnAttribute(AttributeNames.Keytype, value);
//     }
//
//     /// <summary>
//     /// Gets the type of input control (keygen).
//     /// </summary>
//     public StringOrMemory Type => TagNames.Keygen;
//
//     #endregion
//
// }
//
// sealed class ReadOnlyHtmlLabelElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlLabelElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Label, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlLabelElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Label, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//
//
//     /// <summary>
//     /// Gets or sets the ID of the labeled control. Reflects the for attribute.
//     /// </summary>
//     public StringOrMemory HtmlFor
//     {
//         get => this.GetOwnAttribute(AttributeNames.For);
//         set => this.SetOwnAttribute(AttributeNames.For, value);
//     }
//
//
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlLegendElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlLegendElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Legend, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlLegendElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Legend, prefix)
//     {
//     }
//
//     #endregion
//     //
//     // #region Properties
//     //
//     // /// <summary>
//     // /// Gets the associated form.
//     // /// </summary>
//     // public IHtmlFormElement? Form
//     // {
//     //     get
//     //     {
//     //         var fieldset = Parent as HtmlFieldSetElement;
//     //         return fieldset?.Form;
//     //     }
//     // }
//     //
//     // #endregion
// }
//
// sealed class ReadOnlyHtmlLinkElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlLinkElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Link, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     // {
//     // }
//
//     public ReadOnlyHtmlLinkElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Link, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Href
//     {
//         get => this.GetUrlAttribute(AttributeNames.Href);
//         set => this.SetOwnAttribute(AttributeNames.Href, value);
//     }
//
//     public StringOrMemory TargetLanguage
//     {
//         get => this.GetOwnAttribute(AttributeNames.HrefLang);
//         set => this.SetOwnAttribute(AttributeNames.HrefLang, value);
//     }
//
//     public StringOrMemory Charset
//     {
//         get => this.GetOwnAttribute(AttributeNames.Charset);
//         set => this.SetOwnAttribute(AttributeNames.Charset, value);
//     }
//
//     public StringOrMemory Relation
//     {
//         get => this.GetOwnAttribute(AttributeNames.Rel);
//         set => this.SetOwnAttribute(AttributeNames.Rel, value);
//     }
//
//     public StringOrMemory ReverseRelation
//     {
//         get => this.GetOwnAttribute(AttributeNames.Rev);
//         set => this.SetOwnAttribute(AttributeNames.Rev, value);
//     }
//
//     public StringOrMemory NumberUsedOnce
//     {
//         get => this.GetOwnAttribute(AttributeNames.Nonce);
//         set => this.SetOwnAttribute(AttributeNames.Nonce, value);
//     }
//
//     public StringOrMemory Rev
//     {
//         get => this.GetOwnAttribute(AttributeNames.Rev);
//         set => this.SetOwnAttribute(AttributeNames.Rev, value);
//     }
//
//     public Boolean IsDisabled
//     {
//         get => this.GetBoolAttribute(AttributeNames.Disabled);
//         set => this.SetBoolAttribute(AttributeNames.Disabled, value);
//     }
//
//     public StringOrMemory Target
//     {
//         get => this.GetOwnAttribute(AttributeNames.Target);
//         set => this.SetOwnAttribute(AttributeNames.Target, value);
//     }
//
//     public StringOrMemory Media
//     {
//         get => this.GetOwnAttribute(AttributeNames.Media);
//         set => this.SetOwnAttribute(AttributeNames.Media, value);
//     }
//
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     public StringOrMemory Integrity
//     {
//         get => this.GetOwnAttribute(AttributeNames.Integrity);
//         set => this.SetOwnAttribute(AttributeNames.Integrity, value);
//     }
//
//     public StringOrMemory CrossOrigin
//     {
//         get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
//         set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlListItemElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new item tag.
//     /// </summary>
//     // public HtmlListItemElement(Document owner, String? name = null, String? prefix = null)
//     //     : base(owner, name ?? TagNames.Li, prefix,
//     //         NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     // {
//     // }
//
//     public ReadOnlyHtmlListItemElement(ReadOnlyDocument owner, StringOrMemory name = default, StringOrMemory prefix = default)
//         : base(owner, name.OrElse(TagNames.Li), prefix,
//             NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     // public Int32? Value
//     // {
//     //     get => Int32.TryParse(this.GetOwnAttribute(AttributeNames.Value), NumberStyles.Integer,
//     //         CultureInfo.InvariantCulture, out var i)
//     //         ? i
//     //         : new Int32?();
//     //     set => this.SetOwnAttribute(AttributeNames.Value, value.HasValue ? value.Value.ToString() : null);
//     // }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlMapElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlMapElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Map, prefix)
//     // {
//     // }
//
//     public ReadOnlyHtmlMapElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Map, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the value of the href attribute.
//     /// </summary>
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlMarqueeElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public HtmlMarqueeElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Marquee, prefix, NodeFlags.Special | NodeFlags.Scoped)
//     // {
//     // }
//
//     public ReadOnlyHtmlMarqueeElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Marquee, prefix, NodeFlags.Special | NodeFlags.Scoped)
//     {
//     }
//
//     #endregion
//
//
//
// }
//
// sealed class ReadOnlyHtmlMenuElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML menu element.
//     /// </summary>
//     // public HtmlMenuElement(Document owner, String? prefix = null)
//     //     : base(owner, TagNames.Menu, prefix, NodeFlags.Special)
//     // {
//     // }
//
//     public ReadOnlyHtmlMenuElement(ReadOnlyDocument owner, StringOrMemory prefix = default)
//         : base(owner, TagNames.Menu, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the type of the menu element.
//     /// </summary>
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the text label of the menu element.
//     /// </summary>
//     public StringOrMemory Label
//     {
//         get => this.GetOwnAttribute(AttributeNames.Label);
//         set => this.SetOwnAttribute(AttributeNames.Label, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlMenuItemElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML menuitem element.
//     /// </summary>
//     public HtmlMenuItemElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.MenuItem, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets the assigned master command, if any.
//     /// </summary>
//     public IHtmlElement? Command
//     {
//         get
//         {
//             var id = this.GetOwnAttribute(AttributeNames.Command);
//
//             if (id is { Length: > 0 })
//             {
//                 return Owner?.GetElementById(id) as IHtmlElement;
//             }
//
//             return null;
//         }
//     }
//
//     /// <summary>
//     /// Gets or sets the type of command.
//     /// </summary>
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the user-visible label.
//     /// </summary>
//     public StringOrMemory Label
//     {
//         get => this.GetOwnAttribute(AttributeNames.Label);
//         set => this.SetOwnAttribute(AttributeNames.Label, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the icon for the command.
//     /// </summary>
//     public StringOrMemory Icon
//     {
//         get => this.GetOwnAttribute(AttributeNames.Icon);
//         set => this.SetOwnAttribute(AttributeNames.Icon, value);
//     }
//
//     /// <summary>
//     /// Gets or sets if the menuitem element is enabled or disabled.
//     /// </summary>
//     public Boolean IsDisabled
//     {
//         get => this.GetBoolAttribute(AttributeNames.Disabled);
//         set => this.SetBoolAttribute(AttributeNames.Disabled, value);
//     }
//
//     /// <summary>
//     /// Gets or sets if the menuitem element is checked or not.
//     /// </summary>
//     public Boolean IsChecked
//     {
//         get => this.GetBoolAttribute(AttributeNames.Checked);
//         set => this.SetBoolAttribute(AttributeNames.Checked, value);
//     }
//
//     /// <summary>
//     /// Gets or sets if the menuitem element is the default command.
//     /// </summary>
//     public Boolean IsDefault
//     {
//         get => this.GetBoolAttribute(AttributeNames.Default);
//         set => this.SetBoolAttribute(AttributeNames.Default, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the name of group of commands to
//     /// treat as a radio button group.
//     /// </summary>
//     public StringOrMemory RadioGroup
//     {
//         get => this.GetOwnAttribute(AttributeNames.Radiogroup);
//         set => this.SetOwnAttribute(AttributeNames.Radiogroup, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlMetaElement : ReadOnlyHtmlElement, IHtmlMetaElement
// {
//     #region ctor
//
//     public HtmlMetaElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Content
//     {
//         get => this.GetOwnAttribute(AttributeNames.Content);
//         set => this.SetOwnAttribute(AttributeNames.Content, value);
//     }
//
//     public StringOrMemory Charset
//     {
//         get => this.GetOwnAttribute(AttributeNames.Charset);
//         set => this.SetOwnAttribute(AttributeNames.Charset, value);
//     }
//
//     public StringOrMemory HttpEquivalent
//     {
//         get => this.GetOwnAttribute(AttributeNames.HttpEquiv);
//         set => this.SetOwnAttribute(AttributeNames.HttpEquiv, value);
//     }
//
//     public StringOrMemory Scheme
//     {
//         get => this.GetOwnAttribute(AttributeNames.Scheme);
//         set => this.SetOwnAttribute(AttributeNames.Scheme, value);
//     }
//
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     #endregion
//
//     #region Methods
//
//     public void Handle()
//     {
//         var handlers = Owner.Context.GetServices<IMetaHandler>();
//
//         foreach (var handler in handlers)
//         {
//             handler.HandleContent(this);
//         }
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlMeterElement : ReadOnlyHtmlElement, IHtmlMeterElement
// {
//     #region Fields
//
//     private readonly NodeList _labels;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlMeterElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Meter, prefix)
//     {
//         _labels = new NodeList();
//     }
//
//     #endregion
//
//     #region Properties
//
//     public INodeList Labels => _labels;
//
//     public Double Value
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value).ToDouble(0.0).Constraint(Minimum, Maximum);
//         set => this.SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double Maximum
//     {
//         get => this.GetOwnAttribute(AttributeNames.Max).ToDouble(1.0).Constraint(Minimum, Double.PositiveInfinity);
//         set => this.SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double Minimum
//     {
//         get => this.GetOwnAttribute(AttributeNames.Min).ToDouble(0.0);
//         set => this.SetOwnAttribute(AttributeNames.Min, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double Low
//     {
//         get => this.GetOwnAttribute(AttributeNames.Low).ToDouble(Minimum).Constraint(Minimum, Maximum);
//         set => this.SetOwnAttribute(AttributeNames.Low, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double High
//     {
//         get => this.GetOwnAttribute(AttributeNames.High).ToDouble(Maximum).Constraint(Low, Maximum);
//         set => this.SetOwnAttribute(AttributeNames.High, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double Optimum
//     {
//         get => this.GetOwnAttribute(AttributeNames.Optimum).ToDouble((Maximum + Minimum) * 0.5)
//             .Constraint(Minimum, Maximum);
//         set => this.SetOwnAttribute(AttributeNames.Optimum, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlModElement : ReadOnlyHtmlElement, IHtmlModElement
// {
//     #region ctor
//
//     public HtmlModElement(Document owner, String? name = null, String? prefix = null)
//         : base(owner, name ?? TagNames.Ins, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the value that contains a URI of a resource
//     /// explaining the change.
//     /// </summary>
//     public StringOrMemory Citation
//     {
//         get => this.GetOwnAttribute(AttributeNames.Cite);
//         set => this.SetOwnAttribute(AttributeNames.Cite, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the value that contains date-and-time string
//     /// representing a timestamp for the change.
//     /// </summary>
//     public StringOrMemory DateTime
//     {
//         get => this.GetOwnAttribute(AttributeNames.Datetime);
//         set => this.SetOwnAttribute(AttributeNames.Datetime, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlNoEmbedElement : ReadOnlyHtmlElement
// {
//     public HtmlNoEmbedElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.NoEmbed, prefix, NodeFlags.Special | NodeFlags.LiteralText)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlNoFramesElement : ReadOnlyHtmlElement
// {
//     public HtmlNoFramesElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.NoFrames, prefix, NodeFlags.Special | NodeFlags.LiteralText)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlNoNewlineElement : ReadOnlyHtmlElement
// {
//     public HtmlNoNewlineElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.NoBr, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlNoScriptElement : ReadOnlyHtmlElement
// {
//     public HtmlNoScriptElement(Document owner, String? prefix = null, Boolean? scripting = false)
//         : base(owner, TagNames.NoScript, prefix, GetFlags(scripting ?? owner.Context.IsScripting()))
//     {
//     }
//
//     private static NodeFlags GetFlags(Boolean scripting)
//     {
//         if (scripting)
//         {
//             return NodeFlags.Special | NodeFlags.LiteralText;
//         }
//
//         return NodeFlags.Special;
//     }
// }
//
// sealed class ReadOnlyHtmlObjectElement : ReadOnlyHtmlFormControlElement, IHtmlObjectElement
// {
//     #region Fields
//
//     private readonly ObjectRequestProcessor _request;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlObjectElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Object, prefix, NodeFlags.Scoped)
//     {
//         _request = new ObjectRequestProcessor(owner.Context);
//     }
//
//     #endregion
//
//     #region Properties
//
//     public IDownload? CurrentDownload => _request?.Download;
//
//     public StringOrMemory Source
//     {
//         get => this.GetUrlAttribute(AttributeNames.Data);
//         set => this.SetOwnAttribute(AttributeNames.Data, value);
//     }
//
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     public Boolean TypeMustMatch
//     {
//         get => this.GetBoolAttribute(AttributeNames.TypeMustMatch);
//         set => this.SetBoolAttribute(AttributeNames.TypeMustMatch, value);
//     }
//
//     public StringOrMemory UseMap
//     {
//         get => this.GetOwnAttribute(AttributeNames.UseMap);
//         set => this.SetOwnAttribute(AttributeNames.UseMap, value);
//     }
//
//     public Int32 DisplayWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth);
//         set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
//     }
//
//     public Int32 DisplayHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight);
//         set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
//     }
//
//     public Int32 OriginalWidth => _request?.Width ?? 0;
//
//     public Int32 OriginalHeight => _request?.Height ?? 0;
//
//     public IDocument? ContentDocument => null;
//
//     public IWindow? ContentWindow => null;
//
//     #endregion
//
//     #region Methods
//
//     protected override Boolean CanBeValidated()
//     {
//         return false;
//     }
//
//     #endregion
//
//     #region Internal Methods
//
//     internal override void SetupElement()
//     {
//         base.SetupElement();
//         UpdateSource(this.GetOwnAttribute(AttributeNames.Data));
//     }
//
//     internal void UpdateSource(String? value)
//     {
//         if (value != null)
//         {
//             var url = new Url(Source!);
//             this.Process(_request, url);
//         }
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlOptionElement : ReadOnlyHtmlElement, IHtmlOptionElement
// {
//     #region Fields
//
//     private Boolean? _selected;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlOptionElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Option, prefix,
//             NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Boolean IsDisabled
//     {
//         get => this.GetBoolAttribute(AttributeNames.Disabled);
//         set => this.SetBoolAttribute(AttributeNames.Disabled, value);
//     }
//
//     public IHtmlFormElement? Form => GetAssignedForm();
//
//     [AllowNull]
//     public StringOrMemory Label
//     {
//         get => this.GetOwnAttribute(AttributeNames.Label) ?? Text;
//         set => this.SetOwnAttribute(AttributeNames.Label, value);
//     }
//
//     public StringOrMemory Value
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value) ?? Text;
//         set => this.SetOwnAttribute(AttributeNames.Value, value);
//     }
//
//     public Int32 Index
//     {
//         get
//         {
//             if (Parent is HtmlOptionsGroupElement group)
//             {
//                 var i = 0;
//
//                 foreach (var child in group.ChildNodes)
//                 {
//                     if (Object.ReferenceEquals(child, this))
//                     {
//                         return i;
//                     }
//
//                     i++;
//                 }
//             }
//
//             return 0;
//         }
//     }
//
//     public StringOrMemory Text
//     {
//         get => TextContent.CollapseAndStrip();
//         set => TextContent = value;
//     }
//
//     public Boolean IsDefaultSelected
//     {
//         get => this.GetBoolAttribute(AttributeNames.Selected);
//         set => this.SetBoolAttribute(AttributeNames.Selected, value);
//     }
//
//     public Boolean IsSelected
//     {
//         get => _selected ?? IsDefaultSelected;
//         set => _selected = value;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlOptionsGroupElement : ReadOnlyHtmlElement, IHtmlOptionsGroupElement
// {
//     #region ctor
//
//     public HtmlOptionsGroupElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Optgroup, prefix,
//             NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Label
//     {
//         get => this.GetOwnAttribute(AttributeNames.Label);
//         set => this.SetOwnAttribute(AttributeNames.Label, value);
//     }
//
//     public Boolean IsDisabled
//     {
//         get => this.GetBoolAttribute(AttributeNames.Disabled);
//         set => this.SetBoolAttribute(AttributeNames.Disabled, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlOrderedListElement : ReadOnlyHtmlElement, IHtmlOrderedListElement
// {
//     #region ctor
//
//     public HtmlOrderedListElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Ol, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets if the order is reversed.
//     /// </summary>
//     public Boolean IsReversed
//     {
//         get => this.GetBoolAttribute(AttributeNames.Reversed);
//         set => this.SetBoolAttribute(AttributeNames.Reversed, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the start of the numbering.
//     /// </summary>
//     public Int32 Start
//     {
//         get => this.GetOwnAttribute(AttributeNames.Start).ToInteger(1);
//         set => this.SetOwnAttribute(AttributeNames.Start, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets a value within [ 1, a, A, i, I ].
//     /// </summary>
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlOutputElement : ReadOnlyHtmlFormControlElement, IHtmlOutputElement
// {
//     #region Fields
//
//     private String? _defaultValue;
//     private String? _value;
//     private SettableTokenList? _for;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlOutputElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Output, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory DefaultValue
//     {
//         get => _defaultValue ?? TextContent;
//         set => _defaultValue = value;
//     }
//
//     public override String TextContent
//     {
//         get => _value ?? _defaultValue ?? base.TextContent;
//         set => base.TextContent = value;
//     }
//
//     public StringOrMemory Value
//     {
//         get => TextContent;
//         set => _value = value;
//     }
//
//     public ISettableTokenList HtmlFor
//     {
//         get
//         {
//             if (_for is null)
//             {
//                 _for = new SettableTokenList(this.GetOwnAttribute(AttributeNames.For));
//                 _for.Changed += value => UpdateAttribute(AttributeNames.For, value);
//             }
//
//             return _for;
//         }
//     }
//
//     public StringOrMemory Type => TagNames.Output;
//
//     #endregion
//
//     #region Internal Methods
//
//     internal override void Reset()
//     {
//         _value = null;
//     }
//
//     internal void UpdateFor(String value)
//     {
//         _for?.Update(value);
//     }
//
//     #endregion
//
//     #region Helpers
//
//     protected override Boolean CanBeValidated()
//     {
//         return true;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlParagraphElement : ReadOnlyHtmlElement, IHtmlParagraphElement
// {
//     #region ctor
//
//     public HtmlParagraphElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.P, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlParamElement : ReadOnlyHtmlElement, IHtmlParamElement
// {
//     #region ctor
//
//     public HtmlParamElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Param, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Value
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value);
//         set => this.SetOwnAttribute(AttributeNames.Value, value);
//     }
//
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlPictureElement : ReadOnlyHtmlElement, IHtmlPictureElement
// {
//     public HtmlPictureElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Picture, prefix)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlPlaintextElement : ReadOnlyHtmlElement
// {
//     public HtmlPlaintextElement(Document owner, String prefix)
//         : base(owner, TagNames.Plaintext, prefix, NodeFlags.Special | NodeFlags.LiteralText)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlPreElement : ReadOnlyHtmlElement, IHtmlPreElement
// {
//     public HtmlPreElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Pre, prefix, NodeFlags.Special | NodeFlags.LineTolerance)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlProgressElement : ReadOnlyHtmlElement, IHtmlProgressElement
// {
//     #region Fields
//
//     private readonly NodeList _labels;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlProgressElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Progress, prefix)
//     {
//         _labels = new NodeList();
//     }
//
//     #endregion
//
//     #region Properties
//
//     public INodeList Labels => _labels;
//
//     public Boolean IsDeterminate => !String.IsNullOrEmpty(this.GetOwnAttribute(AttributeNames.Value));
//
//     public Double Value
//     {
//         get => this.GetOwnAttribute(AttributeNames.Value).ToDouble(0.0);
//         set => this.SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double Maximum
//     {
//         get => this.GetOwnAttribute(AttributeNames.Max).ToDouble(1.0);
//         set => this.SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo));
//     }
//
//     public Double Position => IsDeterminate ? Math.Max(Math.Min(Value / Maximum, 1.0), 0.0) : -1.0;
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlQuoteElement : ReadOnlyHtmlElement, IHtmlQuoteElement
// {
//     #region ctor
//
//     public HtmlQuoteElement(Document owner, String? name = null, String? prefix = null)
//         : base(owner, name ?? TagNames.Quote, prefix, name.Is(TagNames.BlockQuote) ? NodeFlags.Special : NodeFlags.None)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the citation.
//     /// </summary>
//     public StringOrMemory Citation
//     {
//         get => this.GetOwnAttribute(AttributeNames.Cite);
//         set => this.SetOwnAttribute(AttributeNames.Cite, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlRbElement : ReadOnlyHtmlElement
// {
//     public HtmlRbElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Rb, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlRpElement : ReadOnlyHtmlElement
// {
//     public HtmlRpElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Rp, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlRtcElement : ReadOnlyHtmlElement
// {
//     public HtmlRtcElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Rtc, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlRtElement : ReadOnlyHtmlElement
// {
//     public HtmlRtElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Rt, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlRubyElement : ReadOnlyHtmlElement
// {
//     public HtmlRubyElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Ruby, prefix)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlScriptElement : ReadOnlyHtmlElement
// {
//     #region Fields
//
//     private readonly Boolean _parserInserted;
//
//     private Boolean _started;
//     private Boolean _forceAsync;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlScriptElement(Document owner, String? prefix = null, Boolean parserInserted = false,
//         Boolean started = false)
//         : base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
//     {
//         _forceAsync = false;
//         _started = started;
//         _parserInserted = parserInserted;
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Source
//     {
//         get => this.GetOwnAttribute(AttributeNames.Src);
//         set => this.SetOwnAttribute(AttributeNames.Src, value);
//     }
//
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     public StringOrMemory CharacterSet
//     {
//         get => this.GetOwnAttribute(AttributeNames.Charset);
//         set => this.SetOwnAttribute(AttributeNames.Charset, value);
//     }
//
//     public StringOrMemory Text
//     {
//         get => TextContent;
//         set => TextContent = value;
//     }
//
//     public StringOrMemory CrossOrigin
//     {
//         get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
//         set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
//     }
//
//     public Boolean IsDeferred
//     {
//         get => this.GetBoolAttribute(AttributeNames.Defer);
//         set => this.SetBoolAttribute(AttributeNames.Defer, value);
//     }
//
//     public Boolean IsAsync
//     {
//         get => this.GetBoolAttribute(AttributeNames.Async);
//         set => this.SetBoolAttribute(AttributeNames.Async, value);
//     }
//
//     public StringOrMemory Integrity
//     {
//         get => this.GetOwnAttribute(AttributeNames.Integrity);
//         set => this.SetOwnAttribute(AttributeNames.Integrity, value);
//     }
//
//     #endregion
//
//     #region Methods
//
//     public override Node Clone(Document owner, Boolean deep)
//     {
//         var node = new HtmlScriptElement(owner, Prefix, _parserInserted, _started);
//         CloneElement(node, owner, deep);
//         node._forceAsync = _forceAsync;
//         return node;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlSelectElement : ReadOnlyHtmlFormControlElementWithState, IHtmlSelectElement
// {
//     #region Fields
//
//     private OptionsCollection? _options;
//     private HtmlCollection<IHtmlOptionElement>? _selected;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlSelectElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Select, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Index
//
//     public IHtmlOptionElement this[Int32 index]
//     {
//         get => Options.GetOptionAt(index);
//         set => Options.SetOptionAt(index, value);
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Int32 Size
//     {
//         get => this.GetOwnAttribute(AttributeNames.Size).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Size, value.ToString());
//     }
//
//     public Boolean IsRequired
//     {
//         get => this.GetBoolAttribute(AttributeNames.Required);
//         set => this.SetBoolAttribute(AttributeNames.Required, value);
//     }
//
//     public IHtmlCollection<IHtmlOptionElement> SelectedOptions =>
//         _selected ??= new HtmlCollection<IHtmlOptionElement>(Options.Where(m => m.IsSelected));
//
//     public Int32 SelectedIndex => Options.SelectedIndex;
//
//     public StringOrMemory Value
//     {
//         get
//         {
//             var options = Options;
//
//             foreach (var option in options)
//             {
//                 if (option.IsSelected)
//                 {
//                     return option.Value;
//                 }
//             }
//
//             return null;
//         }
//         set => UpdateValue(value!);
//     }
//
//     public Int32 Length => Options.Length;
//
//     public Boolean IsMultiple
//     {
//         get => this.GetBoolAttribute(AttributeNames.Multiple);
//         set => this.SetBoolAttribute(AttributeNames.Multiple, value);
//     }
//
//     public IHtmlOptionsCollection Options => _options ??= new OptionsCollection(this);
//
//     public StringOrMemory Type => IsMultiple ? InputTypeNames.SelectMultiple : InputTypeNames.SelectOne;
//
//     #endregion
//
//     #region Methods
//
//     public void AddOption(IHtmlOptionElement element, IHtmlElement? before = null)
//     {
//         Options.Add(element, before);
//     }
//
//     public void AddOption(IHtmlOptionsGroupElement element, IHtmlElement? before = null)
//     {
//         Options.Add(element, before);
//     }
//
//     public void RemoveOptionAt(Int32 index)
//     {
//         Options.Remove(index);
//     }
//
//     #endregion
//
//     #region Internal Methods
//
//     internal override FormControlState SaveControlState()
//     {
//         return new FormControlState(Name!, Type, Value);
//     }
//
//     internal override void RestoreFormControlState(FormControlState state)
//     {
//         if (state.Type.Is(Type) && state.Name.Is(Name))
//         {
//             Value = state.Value;
//         }
//     }
//
//     internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
//     {
//         var options = Options;
//         bool isAdded = false;
//
//         for (var i = 0; i < options.Length; i++)
//         {
//             var option = options.GetOptionAt(i);
//
//             if (option.IsSelected && !option.IsDisabled)
//             {
//                 dataSet.Append(Name!, option.Value, Type);
//                 isAdded = true;
//             }
//         }
//
//         if (!isAdded)
//         {
//             // Select default option if theres no selected options
//             var option = GetDefaultOptionOrNull();
//             if (option != null)
//             {
//                 dataSet.Append(Name!, option.Value, Type);
//             }
//         }
//     }
//
//     private IHtmlOptionElement? GetDefaultOptionOrNull()
//     {
//         var options = Options;
//
//         for (int i = 0; i < options.Length; i++)
//         {
//             var option = options.GetOptionAt(i);
//
//             if (!option.IsDisabled)
//             {
//                 return option;
//             }
//         }
//
//         return null;
//     }
//
//     internal override void SetupElement()
//     {
//         base.SetupElement();
//
//         var value = this.GetOwnAttribute(AttributeNames.Value);
//
//         if (value != null)
//         {
//             UpdateValue(value);
//         }
//     }
//
//     internal override void Reset()
//     {
//         var options = Options;
//         var selected = 0;
//         var maxSelected = 0;
//
//         for (var i = 0; i < options.Length; i++)
//         {
//             var option = options.GetOptionAt(i);
//             option.IsSelected = option.IsDefaultSelected;
//
//             if (option.IsSelected)
//             {
//                 maxSelected = i;
//                 selected++;
//             }
//         }
//
//         if (selected != 1 && !IsMultiple && options.Length > 0)
//         {
//             foreach (var option in options)
//             {
//                 option.IsSelected = false;
//             }
//
//             options[maxSelected].IsSelected = true;
//         }
//     }
//
//     internal void UpdateValue(String value)
//     {
//         var options = Options;
//
//         foreach (var option in options)
//         {
//             var selected = option.Value.Isi(value);
//             option.IsSelected = selected;
//         }
//     }
//
//     #endregion
//
//     #region Helpers
//
//     protected override Boolean CanBeValidated()
//     {
//         return !this.HasDataListAncestor();
//     }
//
//     protected override void Check(ValidityState state)
//     {
//         base.Check(state);
//         state.IsValueMissing = IsRequired && String.IsNullOrEmpty(Value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlSemanticElement : ReadOnlyHtmlElement
// {
//     public HtmlSemanticElement(Document owner, String name, String? prefix = null)
//         : base(owner, name, prefix, NodeFlags.Special)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlSlotElement : ReadOnlyHtmlElement, IHtmlSlotElement
// {
//     #region ctor
//
//     public HtmlSlotElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Slot, prefix)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     #endregion
//
//     #region Methods
//
//     public IEnumerable<INode> GetDistributedNodes()
//     {
//         var host = this.GetAncestor<IShadowRoot>()?.Host;
//
//         if (host != null)
//         {
//             var list = new List<INode>();
//
//             foreach (var node in host.ChildNodes)
//             {
//                 if (Object.ReferenceEquals(GetAssignedSlot(node), this))
//                 {
//                     if (node is HtmlSlotElement otherSlot)
//                     {
//                         list.AddRange(otherSlot.GetDistributedNodes());
//                     }
//                     else
//                     {
//                         list.Add(node);
//                     }
//                 }
//             }
//
//             return list;
//         }
//
//         return Array.Empty<INode>();
//     }
//
//     #endregion
//
//     #region Helpers
//
//     private static IElement? GetAssignedSlot(INode node)
//     {
//         return node.NodeType switch
//         {
//             NodeType.Text => ((IText)node).AssignedSlot,
//             NodeType.Element => ((IElement)node).AssignedSlot,
//             _ => default(IElement)
//         };
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlSmallElement : ReadOnlyHtmlElement
// {
//     public HtmlSmallElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Small, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlSourceElement : ReadOnlyHtmlElement, IHtmlSourceElement
// {
//     #region ctor
//
//     public HtmlSourceElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Source, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Source
//     {
//         get => this.GetUrlAttribute(AttributeNames.Src);
//         set => this.SetOwnAttribute(AttributeNames.Src, value);
//     }
//
//     public StringOrMemory Media
//     {
//         get => this.GetOwnAttribute(AttributeNames.Media);
//         set => this.SetOwnAttribute(AttributeNames.Media, value);
//     }
//
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     public StringOrMemory SourceSet
//     {
//         get => this.GetOwnAttribute(AttributeNames.SrcSet);
//         set => this.SetOwnAttribute(AttributeNames.SrcSet, value);
//     }
//
//     public StringOrMemory Sizes
//     {
//         get => this.GetOwnAttribute(AttributeNames.Sizes);
//         set => this.SetOwnAttribute(AttributeNames.Sizes, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlSpanElement : ReadOnlyHtmlElement, IHtmlSpanElement
// {
//     public HtmlSpanElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Span, prefix)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlStrikeElement : ReadOnlyHtmlElement
// {
//     public HtmlStrikeElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Strike, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlStrongElement : ReadOnlyHtmlElement
// {
//     public HtmlStrongElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Strong, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlStruckElement : ReadOnlyHtmlElement
// {
//     public HtmlStruckElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.S, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlStyleElement : ReadOnlyHtmlElement, IHtmlStyleElement
// {
//     #region Fields
//
//     private IStyleSheet? _sheet;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlStyleElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Style, prefix, NodeFlags.Special | NodeFlags.LiteralText)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Boolean IsScoped
//     {
//         get => this.GetBoolAttribute(AttributeNames.Scoped);
//         set => this.SetBoolAttribute(AttributeNames.Scoped, value);
//     }
//
//     public IStyleSheet? Sheet => _sheet;
//
//     public Boolean IsDisabled
//     {
//         get => this.GetBoolAttribute(AttributeNames.Disabled);
//         set
//         {
//             this.SetBoolAttribute(AttributeNames.Disabled, value);
//
//             if (_sheet != null)
//             {
//                 _sheet.IsDisabled = value;
//             }
//         }
//     }
//
//     public StringOrMemory Media
//     {
//         get => this.GetOwnAttribute(AttributeNames.Media);
//         set => this.SetOwnAttribute(AttributeNames.Media, value);
//     }
//
//     public StringOrMemory Type
//     {
//         get => this.GetOwnAttribute(AttributeNames.Type);
//         set => this.SetOwnAttribute(AttributeNames.Type, value);
//     }
//
//     #endregion
//
//     #region Internal Methods
//
//     internal override void SetupElement()
//     {
//         base.SetupElement();
//         UpdateSheet();
//     }
//
//     internal void UpdateMedia(String value)
//     {
//         if (_sheet != null)
//         {
//             _sheet.Media.MediaText = value;
//         }
//     }
//
//     #endregion
//
//     #region Helpers
//
//     protected override void NodeIsInserted(Node newNode)
//     {
//         base.NodeIsInserted(newNode);
//         UpdateSheet();
//     }
//
//     protected override void NodeIsRemoved(Node removedNode, Node? oldPreviousSibling)
//     {
//         base.NodeIsRemoved(removedNode, oldPreviousSibling);
//         UpdateSheet();
//     }
//
//     private void UpdateSheet()
//     {
//         var document = Owner;
//
//         if (document != null)
//         {
//             var context = Context;
//             var type = Type ?? MimeTypeNames.Css;
//             var engine = context.GetStyling(type);
//
//             if (engine != null)
//             {
//                 var task = CreateSheetAsync(engine, document);
//                 document.DelayLoad(task);
//             }
//         }
//     }
//
//     private async Task CreateSheetAsync(IStylingService engine, IDocument document)
//     {
//         var cancel = CancellationToken.None;
//         var response = VirtualResponse.Create(res => res.Content(TextContent).Address(default(Url)));
//         var options = new StyleOptions(document)
//         {
//             Element = this,
//             IsDisabled = IsDisabled,
//             IsAlternate = false,
//         };
//         var task = engine.ParseStylesheetAsync(response, options, cancel);
//         _sheet = await task.ConfigureAwait(false);
//         UpdateMedia(Media!);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableCaptionElement : ReadOnlyHtmlElement, IHtmlTableCaptionElement
// {
//     #region ctor
//
//     public HtmlTableCaptionElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Caption, prefix, NodeFlags.Special | NodeFlags.Scoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the value of the alignment attribute.
//     /// </summary>
//     public StringOrMemory Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align) ?? Keywords.Top;
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableColElement : ReadOnlyHtmlElement, IHtmlTableColumnElement
// {
//     #region ctor
//
//     public HtmlTableColElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Col, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the value of the horizontal alignment attribute.
//     /// </summary>
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the number of columns in a group or affected by a grouping.
//     /// </summary>
//     public Int32 Span
//     {
//         get => this.GetOwnAttribute(AttributeNames.Span).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Span, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the value of the vertical alignment attribute.
//     /// </summary>
//     public VerticalAlignment VAlign
//     {
//         get => this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
//         set => this.SetOwnAttribute(AttributeNames.Valign, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the value of the width attribute.
//     /// </summary>
//     public StringOrMemory Width
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width);
//         set => this.SetOwnAttribute(AttributeNames.Width, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableColgroupElement : ReadOnlyHtmlElement, IHtmlTableColumnElement
// {
//     #region ctor
//
//     public HtmlTableColgroupElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Colgroup, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the value of the horizontal alignment attribute.
//     /// </summary>
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the number of columns in a group or affected by a grouping.
//     /// </summary>
//     public Int32 Span
//     {
//         get => this.GetOwnAttribute(AttributeNames.Span).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Span, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the value of the vertical alignment attribute.
//     /// </summary>
//     public VerticalAlignment VAlign
//     {
//         get => this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
//         set => this.SetOwnAttribute(AttributeNames.Valign, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the value of the width attribute.
//     /// </summary>
//     public StringOrMemory Width
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width);
//         set => this.SetOwnAttribute(AttributeNames.Width, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableDataCellElement : ReadOnlyHtmlTableCellElement, IHtmlTableDataCellElement
// {
//     public HtmlTableDataCellElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Td, prefix)
//     {
//     }
// }
//
// abstract class ReadOnlyHtmlTableCellElement : ReadOnlyHtmlElement, IHtmlTableCellElement
// {
//     #region Fields
//
//     private SettableTokenList? _headers;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlTableCellElement(Document owner, String name, String? prefix)
//         : base(owner, name, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.Scoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Int32 Index
//     {
//         get
//         {
//             var parent = ParentElement;
//
//             while (parent != null && !(parent is IHtmlTableRowElement))
//             {
//                 parent = parent.ParentElement;
//             }
//
//             var row = parent as HtmlTableRowElement;
//             return row?.IndexOf(this) ?? -1;
//         }
//     }
//
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     public VerticalAlignment VAlign
//     {
//         get => this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
//         set => this.SetOwnAttribute(AttributeNames.Valign, value.ToString());
//     }
//
//     public StringOrMemory BgColor
//     {
//         get => this.GetOwnAttribute(AttributeNames.BgColor);
//         set => this.SetOwnAttribute(AttributeNames.BgColor, value);
//     }
//
//     public StringOrMemory Width
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width);
//         set => this.SetOwnAttribute(AttributeNames.Width, value);
//     }
//
//     public StringOrMemory Height
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height);
//         set => this.SetOwnAttribute(AttributeNames.Height, value);
//     }
//
//     public Int32 ColumnSpan
//     {
//         get => LimitColSpan(this.GetOwnAttribute(AttributeNames.ColSpan).ToInteger(1));
//         set => this.SetOwnAttribute(AttributeNames.ColSpan, value.ToString());
//     }
//
//     public Int32 RowSpan
//     {
//         get => LimitRowSpan(this.GetOwnAttribute(AttributeNames.RowSpan).ToInteger(1));
//         set => this.SetOwnAttribute(AttributeNames.RowSpan, value.ToString());
//     }
//
//     public Boolean NoWrap
//     {
//         get => this.GetOwnAttribute(AttributeNames.NoWrap).ToBoolean(false);
//         set => this.SetOwnAttribute(AttributeNames.NoWrap, value.ToString());
//     }
//
//     public StringOrMemory Abbr
//     {
//         get => this.GetOwnAttribute(AttributeNames.Abbr);
//         set => this.SetOwnAttribute(AttributeNames.Abbr, value);
//     }
//
//     public StringOrMemory Scope
//     {
//         get => this.GetOwnAttribute(AttributeNames.Scope);
//         set => this.SetOwnAttribute(AttributeNames.Scope, value);
//     }
//
//     public ISettableTokenList Headers
//     {
//         get
//         {
//             if (_headers is null)
//             {
//                 _headers = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Headers));
//                 _headers.Changed += value => UpdateAttribute(AttributeNames.Headers, value);
//             }
//
//             return _headers;
//         }
//     }
//
//     public StringOrMemory Axis
//     {
//         get => this.GetOwnAttribute(AttributeNames.Axis);
//         set => this.SetOwnAttribute(AttributeNames.Axis, value);
//     }
//
//     #endregion
//
//     #region Internal Methods
//
//     internal void UpdateHeaders(String value)
//     {
//         _headers?.Update(value);
//     }
//
//     #endregion
//
//     #region Helpers
//
//     private static Int32 LimitColSpan(Int32 value)
//     {
//         return value >= 1 && value <= 1000 ? value : 1;
//     }
//
//     private static Int32 LimitRowSpan(Int32 value)
//     {
//         return value >= 0 ? Math.Min(value, 65534) : 1;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableElement : ReadOnlyHtmlElement, IHtmlTableElement
// {
//     #region Fields
//
//     private HtmlCollection<IHtmlTableSectionElement>? _bodies;
//     private HtmlCollection<IHtmlTableRowElement>? _rows;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlTableElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Table, prefix,
//             NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public IHtmlTableCaptionElement? Caption
//     {
//         get => ChildNodes.OfType<IHtmlTableCaptionElement>().FirstOrDefault(m => m.LocalName.Is(TagNames.Caption));
//         set
//         {
//             DeleteCaption();
//
//             if (value != null)
//             {
//                 InsertChild(0, value);
//             }
//         }
//     }
//
//     public IHtmlTableSectionElement? Head
//     {
//         get => ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault(m => m.LocalName.Is(TagNames.Thead));
//         set
//         {
//             DeleteHead();
//
//             if (value != null)
//             {
//                 AppendChild(value);
//             }
//         }
//     }
//
//     public IHtmlCollection<IHtmlTableSectionElement> Bodies => _bodies ??=
//         new HtmlCollection<IHtmlTableSectionElement>(this, deep: false, predicate: m => m.LocalName.Is(TagNames.Tbody));
//
//     public IHtmlTableSectionElement? Foot
//     {
//         get => ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault(m => m.LocalName.Is(TagNames.Tfoot));
//         set
//         {
//             DeleteFoot();
//
//             if (value != null)
//             {
//                 AppendChild(value);
//             }
//         }
//     }
//
//     public IEnumerable<IHtmlTableRowElement> AllRows
//     {
//         get
//         {
//             var heads = ChildNodes.OfType<IHtmlTableSectionElement>().Where(m => m.LocalName.Is(TagNames.Thead));
//             var foots = ChildNodes.OfType<IHtmlTableSectionElement>().Where(m => m.LocalName.Is(TagNames.Tfoot));
//
//             foreach (var head in heads)
//             {
//                 foreach (var row in head.Rows)
//                 {
//                     yield return row;
//                 }
//             }
//
//             foreach (var child in ChildNodes)
//             {
//                 if (child is IHtmlTableSectionElement sectionEl)
//                 {
//                     if (sectionEl.LocalName.Is(TagNames.Tbody))
//                     {
//                         foreach (var row in sectionEl.Rows)
//                         {
//                             yield return row;
//                         }
//                     }
//                 }
//                 else if (child is IHtmlTableRowElement rowEl)
//                 {
//                     yield return rowEl;
//                 }
//             }
//
//             foreach (var foot in foots)
//             {
//                 foreach (var row in foot.Rows)
//                 {
//                     yield return row;
//                 }
//             }
//         }
//     }
//
//     public IHtmlCollection<IHtmlTableRowElement> Rows => _rows ??= new HtmlCollection<IHtmlTableRowElement>(AllRows);
//
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     public StringOrMemory BgColor
//     {
//         get => this.GetOwnAttribute(AttributeNames.BgColor);
//         set => this.SetOwnAttribute(AttributeNames.BgColor, value);
//     }
//
//     public UInt32 Border
//     {
//         get => this.GetOwnAttribute(AttributeNames.Border).ToInteger(0u);
//         set => this.SetOwnAttribute(AttributeNames.Border, value.ToString());
//     }
//
//     public Int32 CellPadding
//     {
//         get => this.GetOwnAttribute(AttributeNames.CellPadding).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.CellPadding, value.ToString());
//     }
//
//     public Int32 CellSpacing
//     {
//         get => this.GetOwnAttribute(AttributeNames.CellSpacing).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.CellSpacing, value.ToString());
//     }
//
//     public TableFrames Frame
//     {
//         get => this.GetOwnAttribute(AttributeNames.Frame).ToEnum(TableFrames.Void);
//         set => this.SetOwnAttribute(AttributeNames.Frame, value.ToString());
//     }
//
//     public TableRules Rules
//     {
//         get => this.GetOwnAttribute(AttributeNames.Rules).ToEnum(TableRules.All);
//         set => this.SetOwnAttribute(AttributeNames.Rules, value.ToString());
//     }
//
//     public StringOrMemory Summary
//     {
//         get => this.GetOwnAttribute(AttributeNames.Summary);
//         set => this.SetOwnAttribute(AttributeNames.Summary, value);
//     }
//
//     public StringOrMemory Width
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width);
//         set => this.SetOwnAttribute(AttributeNames.Width, value);
//     }
//
//     #endregion
//
//     #region Methods
//
//     public IHtmlTableRowElement InsertRowAt(Int32 index = -1)
//     {
//         var rows = Rows;
//         var newRow = (IHtmlTableRowElement)Owner.CreateElement(TagNames.Tr);
//
//         if (index >= 0 && index < rows.Length)
//         {
//             var row = rows[index];
//             row.ParentElement!.InsertBefore(newRow, row);
//         }
//         else if (rows.Length == 0)
//         {
//             var bodies = Bodies;
//
//             if (bodies.Length == 0)
//             {
//                 var tbody = Owner.CreateElement(TagNames.Tbody);
//                 AppendChild(tbody);
//             }
//
//             bodies[bodies.Length - 1].AppendChild(newRow);
//         }
//         else
//         {
//             rows[rows.Length - 1].ParentElement!.AppendChild(newRow);
//         }
//
//         return newRow;
//     }
//
//     public void RemoveRowAt(Int32 index)
//     {
//         var rows = Rows;
//
//         if (index >= 0 && index < rows.Length)
//         {
//             rows[index].Remove();
//         }
//     }
//
//     public IHtmlTableSectionElement CreateHead()
//     {
//         var head = Head;
//
//         if (head is null)
//         {
//             head = (IHtmlTableSectionElement)Owner.CreateElement(TagNames.Thead);
//             AppendChild(head);
//         }
//
//         return head;
//     }
//
//     public IHtmlTableSectionElement CreateBody()
//     {
//         var lastBody = Bodies.LastOrDefault();
//         var body = (IHtmlTableSectionElement)Owner.CreateElement(TagNames.Tbody);
//         var length = ChildNodes.Length;
//         var index = lastBody != null ? lastBody.Index() + 1 : length;
//
//         if (index == length)
//         {
//             AppendChild(body);
//         }
//         else
//         {
//             InsertChild(index, body);
//         }
//
//         return body;
//     }
//
//     public void DeleteHead()
//     {
//         Head?.Remove();
//     }
//
//     public IHtmlTableSectionElement CreateFoot()
//     {
//         var foot = Foot;
//
//         if (foot is null)
//         {
//             foot = (IHtmlTableSectionElement)Owner.CreateElement(TagNames.Tfoot);
//             AppendChild(foot);
//         }
//
//         return foot;
//     }
//
//     public void DeleteFoot()
//     {
//         Foot?.Remove();
//     }
//
//     public IHtmlTableCaptionElement CreateCaption()
//     {
//         var caption = Caption;
//
//         if (caption is null)
//         {
//             caption = (IHtmlTableCaptionElement)Owner.CreateElement(TagNames.Caption);
//             InsertChild(0, caption);
//         }
//
//         return caption;
//     }
//
//     public void DeleteCaption()
//     {
//         Caption?.Remove();
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableHeaderCellElement : ReadOnlyHtmlTableCellElement, IHtmlTableHeaderCellElement
// {
//     public HtmlTableHeaderCellElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Th, prefix)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlTableRowElement : ReadOnlyHtmlElement, IHtmlTableRowElement
// {
//     #region Fields
//
//     private HtmlCollection<IHtmlTableCellElement>? _cells;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlTableRowElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Tr, prefix, NodeFlags.Special | NodeFlags.ImplicitlyClosed)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     public VerticalAlignment VAlign
//     {
//         get => this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
//         set => this.SetOwnAttribute(AttributeNames.Valign, value.ToString());
//     }
//
//     public StringOrMemory BgColor
//     {
//         get => this.GetOwnAttribute(AttributeNames.BgColor);
//         set => this.SetOwnAttribute(AttributeNames.BgColor, value);
//     }
//
//     public IHtmlCollection<IHtmlTableCellElement> Cells =>
//         _cells ?? (_cells = new HtmlCollection<IHtmlTableCellElement>(this, deep: false));
//
//     public Int32 Index
//     {
//         get
//         {
//             var table = this.GetAncestor<IHtmlTableElement>();
//             return table?.Rows.Index(this) ?? -1;
//         }
//     }
//
//     public Int32 IndexInSection
//     {
//         get
//         {
//             var parent = ParentElement as IHtmlTableSectionElement;
//             return parent?.Rows.Index(this) ?? Index;
//         }
//     }
//
//     #endregion
//
//     #region Methods
//
//     public IHtmlTableCellElement InsertCellAt(Int32 index = -1, TableCellKind tableCellKind = TableCellKind.Td)
//     {
//         var cells = Cells;
//         var newCell =
//             (IHtmlTableCellElement)Owner.CreateElement(tableCellKind == TableCellKind.Td ? TagNames.Td : TagNames.Th);
//
//         if (index >= 0 && index < cells.Length)
//         {
//             InsertBefore(newCell, cells[index]);
//         }
//         else
//         {
//             AppendChild(newCell);
//         }
//
//         return newCell;
//     }
//
//     public void RemoveCellAt(Int32 index)
//     {
//         var cells = Cells;
//
//         if (index < 0)
//         {
//             index = cells.Length + index;
//         }
//
//         if (index >= 0 && index < cells.Length)
//         {
//             cells[index].Remove();
//         }
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTableSectionElement : ReadOnlyHtmlElement, IHtmlTableSectionElement
// {
//     #region Fields
//
//     private HtmlCollection<IHtmlTableRowElement>? _rows;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlTableSectionElement(Document owner, String? name = null, String? prefix = null)
//         : base(owner, name ?? TagNames.Tbody, prefix,
//             NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.HtmlTableSectionScoped)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public HorizontalAlignment Align
//     {
//         get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center);
//         set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
//     }
//
//     public IHtmlCollection<IHtmlTableRowElement> Rows =>
//         _rows ?? (_rows = new HtmlCollection<IHtmlTableRowElement>(this, deep: false));
//
//     public VerticalAlignment VAlign
//     {
//         get => this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
//         set => this.SetOwnAttribute(AttributeNames.Valign, value.ToString());
//     }
//
//     #endregion
//
//     #region Methods
//
//     public IHtmlTableRowElement InsertRowAt(Int32 index = -1)
//     {
//         var rows = Rows;
//         var newRow = (IHtmlTableRowElement)Owner.CreateElement(TagNames.Tr);
//
//         if (index >= 0 && index < rows.Length)
//         {
//             InsertBefore(newRow, rows[index]);
//         }
//         else
//         {
//             AppendChild(newRow);
//         }
//
//         return newRow;
//     }
//
//     public void RemoveRowAt(Int32 index)
//     {
//         var rows = Rows;
//
//         if (index >= 0 && index < rows.Length)
//         {
//             rows[index].Remove();
//         }
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTeletypeTextElement : ReadOnlyHtmlElement
// {
//     public HtmlTeletypeTextElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Tt, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlTemplateElement : ReadOnlyHtmlElement, IHtmlTemplateElement
// {
//     #region Fields
//
//     private readonly DocumentFragment _content;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlTemplateElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Template, prefix,
//             NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
//     {
//         _content = new DocumentFragment(owner);
//     }
//
//     #endregion
//
//     #region Properties
//
//     public IDocumentFragment Content => _content;
//
//     #endregion
//
//     #region Methods
//
//     public override Node Clone(Document owner, Boolean deep)
//     {
//         var template = new HtmlTemplateElement(owner);
//         CloneElement(template, owner, deep);
//         var clonedContent = template._content;
//
//         foreach (var child in _content.ChildNodes)
//         {
//             if (child is Node node)
//             {
//                 var clone = node.Clone(owner, deep);
//                 clonedContent.AddNode(clone);
//             }
//         }
//
//         return template;
//     }
//
//     public void PopulateFragment()
//     {
//         while (HasChildNodes)
//         {
//             var node = ChildNodes[0];
//             RemoveNode(0, node);
//             _content.AddNode(node);
//         }
//     }
//
//     #endregion
//
//     #region Helpers
//
//     protected override void ReplacedAll() => PopulateFragment();
//
//     protected override void NodeIsAdopted(Document oldDocument) => _content.Owner = oldDocument;
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTextAreaElement : ReadOnlyHtmlTextFormControlElement, IHtmlTextAreaElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML textarea element.
//     /// </summary>
//     public HtmlTextAreaElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Textarea, prefix, NodeFlags.LineTolerance)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the wrap HTML attribute, indicating how the control wraps text.
//     /// </summary>
//     public StringOrMemory Wrap
//     {
//         get => this.GetOwnAttribute(AttributeNames.Wrap);
//         set => this.SetOwnAttribute(AttributeNames.Wrap, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the default value of the input field.
//     /// </summary>
//     public override String DefaultValue
//     {
//         get => TextContent;
//         set => TextContent = value;
//     }
//
//     /// <summary>
//     /// Gets the codepoint length of the control's value.
//     /// </summary>
//     public Int32 TextLength => Value.Length;
//
//     /// <summary>
//     /// Gets or sets the rows HTML attribute, indicating
//     /// the number of visible text lines for the control.
//     /// </summary>
//     public Int32 Rows
//     {
//         get => this.GetOwnAttribute(AttributeNames.Rows).ToInteger(2);
//         set => this.SetOwnAttribute(AttributeNames.Rows, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the cols HTML attribute, indicating
//     /// the visible width of the text area.
//     /// </summary>
//     public Int32 Columns
//     {
//         get => this.GetOwnAttribute(AttributeNames.Cols).ToInteger(20);
//         set => this.SetOwnAttribute(AttributeNames.Cols, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets the type of input control (texarea).
//     /// </summary>
//     public StringOrMemory Type => TagNames.Textarea;
//
//     #endregion
//
//     #region Helpers
//
//     internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
//     {
//         ConstructDataSet(dataSet, Type);
//     }
//
//     internal override FormControlState SaveControlState()
//     {
//         return new FormControlState(Name!, Type, Value);
//     }
//
//     internal override void RestoreFormControlState(FormControlState state)
//     {
//         if (state.Type.Is(Type) && state.Name.Is(Name!))
//         {
//             Value = state.Value;
//         }
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTimeElement : ReadOnlyHtmlElement, IHtmlTimeElement
// {
//     #region ctor
//
//     public HtmlTimeElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Time, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory DateTime
//     {
//         get => this.GetOwnAttribute(AttributeNames.Datetime);
//         set => this.SetOwnAttribute(AttributeNames.Datetime, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTitleElement : ReadOnlyHtmlElement, IHtmlTitleElement
// {
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML title element.
//     /// </summary>
//     public HtmlTitleElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Title, prefix, NodeFlags.Special)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the text of the title.
//     /// </summary>
//     public StringOrMemory Text
//     {
//         get => TextContent;
//         set => TextContent = value;
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlTrackElement : ReadOnlyHtmlElement, IHtmlTrackElement
// {
//     #region Fields
//
//     private TrackReadyState _ready;
//
//     #endregion
//
//     #region ctor
//
//     /// <summary>
//     /// Creates a new HTML track element.
//     /// </summary>
//     public HtmlTrackElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Track, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//         _ready = TrackReadyState.None;
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the kind of the track.
//     /// </summary>
//     public StringOrMemory Kind
//     {
//         get => this.GetOwnAttribute(AttributeNames.Kind);
//         set => this.SetOwnAttribute(AttributeNames.Kind, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the media source.
//     /// </summary>
//     public StringOrMemory Source
//     {
//         get => this.GetUrlAttribute(AttributeNames.Src);
//         set => this.SetOwnAttribute(AttributeNames.Src, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the language of the source.
//     /// </summary>
//     public StringOrMemory SourceLanguage
//     {
//         get => this.GetOwnAttribute(AttributeNames.SrcLang);
//         set => this.SetOwnAttribute(AttributeNames.SrcLang, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the label text.
//     /// </summary>
//     public StringOrMemory Label
//     {
//         get => this.GetOwnAttribute(AttributeNames.Label);
//         set => this.SetOwnAttribute(AttributeNames.Label, value);
//     }
//
//     /// <summary>
//     /// Gets or sets if given track is the default track.
//     /// </summary>
//     public Boolean IsDefault
//     {
//         get => this.GetBoolAttribute(AttributeNames.Default);
//         set => this.SetBoolAttribute(AttributeNames.Default, value);
//     }
//
//     /// <summary>
//     /// Gets the ready state of the given track.
//     /// </summary>
//     public TrackReadyState ReadyState => _ready;
//
//     public ITextTrack? Track => null;
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlUnderlineElement : ReadOnlyHtmlElement
// {
//     public HtmlUnderlineElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.U, prefix, NodeFlags.HtmlFormatting)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlUnknownElement : ReadOnlyHtmlElement, IHtmlUnknownElement
// {
//     public HtmlUnknownElement(Document owner, String localName, String? prefix = null, NodeFlags flags = NodeFlags.None)
//         : base(owner, localName, prefix, flags)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlUnorderedListElement : ReadOnlyHtmlElement, IHtmlUnorderedListElement
// {
//     public HtmlUnorderedListElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Ul, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlVideoElement : ReadOnlyHtmlMediaElement<IVideoInfo>, IHtmlVideoElement
// {
//     #region Fields
//
//     private IVideoTrackList? _videos;
//
//     #endregion
//
//     #region ctor
//
//     public HtmlVideoElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Video, prefix)
//     {
//         _videos = null;
//     }
//
//     #endregion
//
//     #region Properties
//
//     public override IVideoTrackList? VideoTracks => _videos;
//
//     public Int32 DisplayWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth);
//         set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
//     }
//
//     public Int32 DisplayHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight);
//         set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
//     }
//
//     public Int32 OriginalWidth => Media?.Width ?? 0;
//
//     public Int32 OriginalHeight => Media?.Height ?? 0;
//
//     public StringOrMemory Poster
//     {
//         get => this.GetUrlAttribute(AttributeNames.Poster);
//         set => this.SetOwnAttribute(AttributeNames.Poster, value);
//     }
//
//     #endregion
// }
//
// sealed class ReadOnlyHtmlWbrElement : ReadOnlyHtmlElement
// {
//     public HtmlWbrElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Wbr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
//     {
//     }
// }
//
// sealed class ReadOnlyHtmlXmpElement : ReadOnlyHtmlElement
// {
//     public HtmlXmpElement(Document owner, String? prefix = null)
//         : base(owner, TagNames.Xmp, prefix, NodeFlags.Special | NodeFlags.LiteralText)
//     {
//     }
// }
//
// abstract class ReadOnlyHtmlFormControlElement : ReadOnlyHtmlElement
// {
//     #region Fields
//
//     // todo: ??
//     private readonly NodeList _labels;
//     // private String? _error;
//
//     #endregion
//
//     #region ctor
//
//     public ReadOnlyHtmlFormControlElement(ReadOnlyDocument owner, StringOrMemory name, StringOrMemory prefix, NodeFlags flags = NodeFlags.None)
//         : base(owner, name, prefix, flags | NodeFlags.Special)
//     {
//         _labels = new NodeList();
//     }
//
//     #endregion
//
//     #region Properties
//
//     public StringOrMemory Name
//     {
//         get => this.GetOwnAttribute(AttributeNames.Name);
//         set => this.SetOwnAttribute(AttributeNames.Name, value);
//     }
//
//     // public IHtmlFormElement? Form => GetAssignedForm();
//
//     public Boolean IsDisabled
//     {
//         get => this.GetBoolAttribute(AttributeNames.Disabled);
//         set => this.SetBoolAttribute(AttributeNames.Disabled, value);
//     }
//
//     public Boolean Autofocus
//     {
//         get => this.GetBoolAttribute(AttributeNames.AutoFocus);
//         set => this.SetBoolAttribute(AttributeNames.AutoFocus, value);
//     }
//
//     public INodeList Labels => _labels;
//
//     // public Boolean WillValidate => !IsDisabled && CanBeValidated();
//
//     #endregion
//
//     // #region Methods
//     //
//     // public override Node Clone(Document owner, Boolean deep)
//     // {
//     //     var node = (HtmlFormControlElement)base.Clone(owner, deep);
//     //     node.SetCustomValidity(_error);
//     //     return node;
//     // }
//     //
//     // #endregion
//     //
//     // #region Helpers
//     //
//     // protected virtual Boolean IsFieldsetDisabled()
//     // {
//     //     var fieldSets = this.GetAncestors().OfType<IHtmlFieldSetElement>();
//     //
//     //     foreach (var fieldSet in fieldSets)
//     //     {
//     //         if (fieldSet.IsDisabled)
//     //         {
//     //             var firstLegend = fieldSet.ChildNodes.FirstOrDefault(m => m is IHtmlLegendElement);
//     //             return firstLegend == null || !this.IsDescendantOf(firstLegend);
//     //         }
//     //     }
//     //
//     //     return false;
//     // }
//     //
//     // internal virtual void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
//     // { }
//     //
//     // internal virtual void Reset()
//     // { }
//     //
//     // protected virtual void Check(ValidityState state)
//     // {
//     //     ResetValidity(state);
//     // }
//     //
//     // protected void ResetValidity(ValidityState state)
//     // {
//     //     state.IsCustomError = !String.IsNullOrEmpty(_error);
//     // }
//     //
//     // protected abstract Boolean CanBeValidated();
//     //
//     // #endregion
// }
//
// abstract class ReadOnlyHtmlTextFormControlElement : ReadOnlyHtmlFormControlElement
// {
//     #region ctor
//
//     public ReadOnlyHtmlTextFormControlElement(ReadOnlyDocument owner, StringOrMemory name, StringOrMemory prefix, NodeFlags flags = NodeFlags.None)
//         : base(owner, name, prefix, flags)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     /// <summary>
//     /// Gets or sets the dirname HTML attribute.
//     /// </summary>
//     public StringOrMemory DirectionName
//     {
//         get => this.GetOwnAttribute(AttributeNames.DirName);
//         set => this.SetOwnAttribute(AttributeNames.DirName, value);
//     }
//
//     /// <summary>
//     /// Gets or sets the maxlength HTML attribute, indicating
//     /// the maximum number of characters the user can enter.
//     /// This constraint is evaluated only when the value changes.
//     /// </summary>
//     public Int32 MaxLength
//     {
//         get => this.GetOwnAttribute(AttributeNames.MaxLength).ToInteger(-1);
//         set => this.SetOwnAttribute(AttributeNames.MaxLength, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets or sets the minlength HTML attribute, indicating
//     /// the minimum number of characters the user can enter.
//     /// This constraint is evaluated only when the value changes.
//     /// </summary>
//     public Int32 MinLength
//     {
//         get => this.GetOwnAttribute(AttributeNames.MinLength).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.MinLength, value.ToString());
//     }
//
//     /// <summary>
//     /// Gets if the input field has a value (via attribute or directly).
//     /// </summary>
//     public Boolean HasValue => this.HasOwnAttribute(AttributeNames.Value);
//
//     /// <summary>
//     /// Gets or sets the placeholder HTML attribute, containing a hint to
//     /// the user about what to enter in the control.
//     /// </summary>
//     public StringOrMemory Placeholder
//     {
//         get => this.GetOwnAttribute(AttributeNames.Placeholder);
//         set => this.SetOwnAttribute(AttributeNames.Placeholder, value);
//     }
//
//     /// <summary>
//     /// Gets or sets if the field is required.
//     /// </summary>
//     public Boolean IsRequired
//     {
//         get => this.GetBoolAttribute(AttributeNames.Required);
//         set => this.SetBoolAttribute(AttributeNames.Required, value);
//     }
//
//     /// <summary>
//     /// Gets or sets if the textarea field is read-only.
//     /// </summary>
//     public Boolean IsReadOnly
//     {
//         get => this.GetBoolAttribute(AttributeNames.Readonly);
//         set => this.SetBoolAttribute(AttributeNames.Readonly, value);
//     }
//
//     #endregion
// }
//
// abstract class ReadOnlyHtmlFrameElementBase : ReadOnlyHtmlFrameOwnerElement
//     {
//         #region Fields
//
//         // private IBrowsingContext? _context;
//
//         #endregion
//
//         #region ctor
//
//         // public HtmlFrameElementBase(Document owner, String name, String? prefix, NodeFlags flags = NodeFlags.None)
//         //     : base(owner, name, prefix, flags | NodeFlags.Special)
//         // {
//         // }
//
//         public ReadOnlyHtmlFrameElementBase(ReadOnlyDocument owner, StringOrMemory name, StringOrMemory prefix, NodeFlags flags = NodeFlags.None)
//             : base(owner, name, prefix, flags | NodeFlags.Special)
//         {
//         }
//
//         #endregion
//
//         #region Properties
//
//
//         public StringOrMemory Name
//         {
//             get => this.GetOwnAttribute(AttributeNames.Name);
//             set => this.SetOwnAttribute(AttributeNames.Name, value);
//         }
//
//         public StringOrMemory Source
//         {
//             get => this.GetOwnAttribute(AttributeNames.Src);
//             set => this.SetOwnAttribute(AttributeNames.Src, value);
//         }
//
//         public StringOrMemory Scrolling
//         {
//             get => this.GetOwnAttribute(AttributeNames.Scrolling);
//             set => this.SetOwnAttribute(AttributeNames.Scrolling, value);
//         }
//
//         // public IDocument? ContentDocument => _request?.Document;
//
//         public StringOrMemory LongDesc
//         {
//             get => this.GetOwnAttribute(AttributeNames.LongDesc);
//             set => this.SetOwnAttribute(AttributeNames.LongDesc, value);
//         }
//
//         public StringOrMemory FrameBorder
//         {
//             get => this.GetOwnAttribute(AttributeNames.FrameBorder);
//             set => this.SetOwnAttribute(AttributeNames.FrameBorder, value);
//         }
//
//         // public IBrowsingContext NestedContext => _context ??= NewChildContext();
//
//         #endregion
//
//         #region Internal Methods
//
//         internal virtual String GetContentHtml()
//         {
//             return null!;
//         }
//
//         internal override void SetupElement()
//         {
//             base.SetupElement();
//         }
//
//
//         #endregion
//     }
//
// abstract class ReadOnlyHtmlFrameOwnerElement : ReadOnlyHtmlElement
// {
//     #region ctor
//
//     // public ReadOnlyHtmlFrameOwnerElement(ReadOnlyDocument owner, String name, String? prefix, NodeFlags flags = NodeFlags.None)
//     //     : base(owner, name, prefix, flags)
//     // {
//     // }
//
//     public ReadOnlyHtmlFrameOwnerElement(ReadOnlyDocument owner, StringOrMemory name, StringOrMemory prefix, NodeFlags flags = NodeFlags.None)
//         : base(owner, name, prefix, flags)
//     {
//     }
//
//     #endregion
//
//     #region Properties
//
//     public Boolean CanContainRangeEndpoint
//     {
//         get;
//         private set;
//     }
//
//     public Int32 DisplayWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
//     }
//
//     public Int32 DisplayHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
//     }
//
//     public Int32 MarginWidth
//     {
//         get => this.GetOwnAttribute(AttributeNames.MarginWidth).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.MarginWidth, value.ToString());
//     }
//
//     public Int32 MarginHeight
//     {
//         get => this.GetOwnAttribute(AttributeNames.MarginHeight).ToInteger(0);
//         set => this.SetOwnAttribute(AttributeNames.MarginHeight, value.ToString());
//     }
//
//     #endregion
// }
//
// abstract class ReadOnlyHtmlMediaElement<TResource> : ReadOnlyHtmlElement
//         where TResource : class, IMediaInfo
//     {
//         #region Fields
//
//         private ITextTrackList? _texts;
//
//         #endregion
//
//         #region ctor
//
//         public HtmlMediaElement(Document owner, String name, String? prefix)
//             : base(owner, name, prefix)
//         {
//         }
//
//         #endregion
//
//         #region Properties
//
//
//         public StringOrMemory Source
//         {
//             get => this.GetUrlAttribute(AttributeNames.Src);
//             set => this.SetOwnAttribute(AttributeNames.Src, value);
//         }
//
//         public StringOrMemory CrossOrigin
//         {
//             get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
//             set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
//         }
//
//         public StringOrMemory Preload
//         {
//             get => this.GetOwnAttribute(AttributeNames.Preload);
//             set => this.SetOwnAttribute(AttributeNames.Preload, value);
//         }
//
//
//         public TResource? Media => _request?.Resource;
//
//         public MediaReadyState ReadyState
//         {
//             get
//             {
//                 var controller = Controller;
//                 return controller is null ? MediaReadyState.Nothing : controller.ReadyState;
//             }
//         }
//
//         public Boolean IsSeeking
//         {
//             get;
//             protected set;
//         }
//
//         public StringOrMemory CurrentSource =>
//                 //TODO Check for Source elements
//                 Source;
//
//         public Double Duration => Controller?.Duration ?? 0.0;
//
//         public Double CurrentTime
//         {
//             get => Controller?.CurrentTime ?? 0.0;
//             set
//             {
//                 var controller = Controller;
//
//                 if (controller != null)
//                 {
//                     controller.CurrentTime = value;
//                 }
//
//                 //if (value < 0)
//                 //    _currentTime = 0;
//                 //else if (value > Duration)
//                 //    _currentTime = Duration;
//                 //else
//                 //    _currentTime = value;
//
//                 //var ev = new Event();
//                 //ev.Init(EventNames.DurationChange, true, true);
//                 //Dispatch(ev);
//             }
//         }
//
//         public Boolean IsAutoplay
//         {
//             get => this.GetBoolAttribute(AttributeNames.Autoplay);
//             set => this.SetBoolAttribute(AttributeNames.Autoplay, value);
//         }
//
//         public Boolean IsLoop
//         {
//             get => this.GetBoolAttribute(AttributeNames.Loop);
//             set => this.SetBoolAttribute(AttributeNames.Loop, value);
//         }
//
//         public Boolean IsShowingControls
//         {
//             get => this.GetBoolAttribute(AttributeNames.Controls);
//             set => this.SetBoolAttribute(AttributeNames.Controls, value);
//         }
//
//         public Boolean IsDefaultMuted
//         {
//             get => this.GetBoolAttribute(AttributeNames.Muted);
//             set => this.SetBoolAttribute(AttributeNames.Muted, value);
//         }
//
//         public Boolean IsPaused => PlaybackState == MediaControllerPlaybackState.Waiting && ReadyState >= MediaReadyState.CurrentData;
//
//         public Boolean IsEnded => PlaybackState == MediaControllerPlaybackState.Ended;
//
//         public DateTime StartDate => DateTime.Today;
//
//         public ITimeRanges? BufferedTime => Controller?.BufferedTime;
//
//         public ITimeRanges? SeekableTime => Controller?.SeekableTime;
//
//         public ITimeRanges? PlayedTime => Controller?.PlayedTime;
//
//         public StringOrMemory MediaGroup
//         {
//             get => this.GetOwnAttribute(AttributeNames.MediaGroup);
//             set => this.SetOwnAttribute(AttributeNames.MediaGroup, value);
//         }
//
//         public Double Volume
//         {
//             get => Controller?.Volume ?? 1.0;
//             set
//             {
//                 var controller = Controller;
//
//                 if (controller != null)
//                 {
//                     controller.Volume = value;
//                 }
//             }
//         }
//
//         public Boolean IsMuted
//         {
//             get => Controller?.IsMuted ?? false;
//             set
//             {
//                 var controller = Controller;
//
//                 if (controller != null)
//                 {
//                     controller.IsMuted = value;
//                 }
//             }
//         }
//
//         public IMediaController? Controller => _request?.Resource?.Controller;
//
//         public Double DefaultPlaybackRate
//         {
//             get => Controller?.DefaultPlaybackRate ?? 1.0;
//             set
//             {
//                 var controller = Controller;
//
//                 if (controller != null)
//                 {
//                     controller.DefaultPlaybackRate = value;
//                 }
//             }
//         }
//
//         public Double PlaybackRate
//         {
//             get => Controller?.PlaybackRate ?? 1.0;
//             set
//             {
//                 var controller = Controller;
//
//                 if (controller != null)
//                 {
//                     controller.PlaybackRate = value;
//                 }
//             }
//         }
//
//         public MediaControllerPlaybackState PlaybackState => Controller?.PlaybackState ?? MediaControllerPlaybackState.Waiting;
//
//         public IMediaError? MediaError
//         {
//             get;
//             private set;
//         }
//
//         public virtual IAudioTrackList? AudioTracks => null;
//
//         public virtual IVideoTrackList? VideoTracks => null;
//
//         public ITextTrackList? TextTracks
//         {
//             get => _texts;
//             protected set => _texts = value;
//         }
//
//         #endregion
//
//         #region Methods
//
//         public void Load()
//         {
//             var source = CurrentSource;
//             UpdateSource(source);
//         }
//
//         public void Play()
//         {
//             Controller?.Play();
//         }
//
//         public void Pause()
//         {
//             Controller?.Pause();
//         }
//
//         public StringOrMemory CanPlayType(String type)
//         {
//             var service = Context?.GetResourceService<TResource>(type);
//             //Other option would be probably.
//             return service != null ? "maybe" : String.Empty;
//         }
//
//         public ITextTrack AddTextTrack(String kind, String? label = null, String? language = null)
//         {
//             //TODO
//             return null!;
//         }
//
//         #endregion
//
//         #region Internal Methods
//
//         internal override void SetupElement()
//         {
//             base.SetupElement();
//         }
//
//
//
//         #endregion
//     }
//
//     internal static class Helpers {
//
//         internal static StringOrMemory GetOwnAttribute(this ReadOnlyElement element, String name)
//             => element.Attributes.GetNamedItem(null, name);
//
//         internal static void SetOwnAttribute(this ReadOnlyElement element, StringOrMemory name, StringOrMemory value = default, Boolean suppressCallbacks = false)
//             => element.Attributes.SetNamedItemWithNamespaceUri(name, value);
//
//         /// <summary>
//         /// Easy way of getting the current boolean value from attributes.
//         /// </summary>
//         /// <param name="element">The element to host the attribute.</param>
//         /// <param name="name">The name of the attribute.</param>
//         /// <returns>The attribute's boolean value.</returns>
//         internal static Boolean GetBoolAttribute(this ReadOnlyElement element, String name)
//         {
//             var value = element.GetOwnAttribute(name);
//             return value.IsNullOrEmpty;
//         }
//
//         /// <summary>
//         /// Easy way of setting the current boolean value of an attribute.
//         /// </summary>
//         /// <param name="element">The element to host the attribute.</param>
//         /// <param name="name">The name of the attribute.</param>
//         /// <param name="value">The attribute's value.</param>
//         internal static void SetBoolAttribute(this ReadOnlyElement element, String name, Boolean value)
//         {
//             if (value)
//             {
//                 element.SetOwnAttribute(name, String.Empty);
//             }
//             else
//             {
//                 element.Attributes.RemoveNamedItemOrDefault(name, true);
//             }
//         }
//
//         /// <summary>
//         /// Converts the given value to an integer (or not).
//         /// </summary>
//         /// <param name="value">The value to convert.</param>
//         /// <param name="defaultValue">The default value to consider (optional).</param>
//         /// <returns>The converted integer.</returns>
//         public static Int32 ToInteger(this StringOrMemory value, Int32 defaultValue = 0)
//         {
//             if (!String.IsNullOrEmpty(value) && Int32.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var converted))
//             {
//                 return converted;
//             }
//
//             return defaultValue;
//         }
//
//         /// <summary>
//         /// Converts the given value to an unsigned integer (or not).
//         /// </summary>
//         /// <param name="value">The value to convert.</param>
//         /// <param name="defaultValue">The default value to consider (optional).</param>
//         /// <returns>The converted unsigned integer.</returns>
//         public static UInt32 ToInteger(this StringOrMemory value, UInt32 defaultValue = 0)
//         {
//             if (!String.IsNullOrEmpty(value) && UInt32.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var converted))
//             {
//                 return converted;
//             }
//
//             return defaultValue;
//         }
//     }