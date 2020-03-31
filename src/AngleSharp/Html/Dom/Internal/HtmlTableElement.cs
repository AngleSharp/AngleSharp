namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the HTML table element.
    /// </summary>
    sealed class HtmlTableElement : HtmlElement, IHtmlTableElement
    {
        #region Fields

        private HtmlCollection<IHtmlTableSectionElement> _bodies;
        private HtmlCollection<IHtmlTableRowElement> _rows;

        #endregion

        #region ctor

        public HtmlTableElement(Document owner, String prefix = null)
            : base(owner, TagNames.Table, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        public IHtmlTableCaptionElement Caption
        {
            get => ChildNodes.OfType<IHtmlTableCaptionElement>().FirstOrDefault(m => m.LocalName.Is(TagNames.Caption));
            set { DeleteCaption(); InsertChild(0, value); }
        }

        public IHtmlTableSectionElement Head
        {
            get => ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault(m => m.LocalName.Is(TagNames.Thead));
            set { DeleteHead(); AppendChild(value); }
        }

        public IHtmlCollection<IHtmlTableSectionElement> Bodies => _bodies ?? (_bodies = new HtmlCollection<IHtmlTableSectionElement>(this, deep: false, predicate: m => m.LocalName.Is(TagNames.Tbody)));

        public IHtmlTableSectionElement Foot
        {
            get => ChildNodes.OfType<IHtmlTableSectionElement>().FirstOrDefault(m => m.LocalName.Is(TagNames.Tfoot));
            set { DeleteFoot(); AppendChild(value); }
        }

        public IEnumerable<IHtmlTableRowElement> AllRows
        {
            get
            {
                var heads = ChildNodes.OfType<IHtmlTableSectionElement>().Where(m => m.LocalName.Is(TagNames.Thead));
                var foots = ChildNodes.OfType<IHtmlTableSectionElement>().Where(m => m.LocalName.Is(TagNames.Tfoot));

                foreach (var head in heads)
                {
                    foreach (var row in head.Rows)
                    {
                        yield return row;
                    }
                }

                foreach (var child in ChildNodes)
                {
                    if (child is IHtmlTableSectionElement)
                    {
                        var body = (IHtmlTableSectionElement)child;

                        if (body.LocalName.Is(TagNames.Tbody))
                        {
                            foreach (var row in body.Rows)
                            {
                                yield return row;
                            }
                        }
                    }
                    else if (child is IHtmlTableRowElement)
                    {
                        yield return (IHtmlTableRowElement)child;
                    }
                }

                foreach (var foot in foots)
                {
                    foreach (var row in foot.Rows)
                    {
                        yield return row;
                    }
                }
            }
        }

        public IHtmlCollection<IHtmlTableRowElement> Rows => _rows ?? (_rows = new HtmlCollection<IHtmlTableRowElement>(AllRows));

        public HorizontalAlignment Align
        {
            get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
            set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
        }

        public String BgColor
        {
            get => this.GetOwnAttribute(AttributeNames.BgColor);
            set => this.SetOwnAttribute(AttributeNames.BgColor, value);
        }

        public UInt32 Border
        {
            get => this.GetOwnAttribute(AttributeNames.Border).ToInteger(0u);
            set => this.SetOwnAttribute(AttributeNames.Border, value.ToString());
        }

        public Int32 CellPadding
        {
            get => this.GetOwnAttribute(AttributeNames.CellPadding).ToInteger(0);
            set => this.SetOwnAttribute(AttributeNames.CellPadding, value.ToString());
        }

        public Int32 CellSpacing
        {
            get => this.GetOwnAttribute(AttributeNames.CellSpacing).ToInteger(0);
            set => this.SetOwnAttribute(AttributeNames.CellSpacing, value.ToString());
        }

        public TableFrames Frame
        {
            get => this.GetOwnAttribute(AttributeNames.Frame).ToEnum(TableFrames.Void);
            set => this.SetOwnAttribute(AttributeNames.Frame, value.ToString());
        }

        public TableRules Rules
        {
            get => this.GetOwnAttribute(AttributeNames.Rules).ToEnum(TableRules.All);
            set => this.SetOwnAttribute(AttributeNames.Rules, value.ToString());
        }

        public String Summary
        {
            get => this.GetOwnAttribute(AttributeNames.Summary);
            set => this.SetOwnAttribute(AttributeNames.Summary, value);
        }

        public String Width
        {
            get => this.GetOwnAttribute(AttributeNames.Width);
            set => this.SetOwnAttribute(AttributeNames.Width, value);
        }

        #endregion

        #region Methods

        public IHtmlTableRowElement InsertRowAt(Int32 index = -1)
        {
            var rows = Rows;
            var newRow = Owner.CreateElement(TagNames.Tr) as IHtmlTableRowElement;

            if (index >= 0 && index < rows.Length)
            {
                var row = rows[index];
                row.ParentElement.InsertBefore(newRow, row);
            }
            else if (rows.Length == 0)
            {
                var bodies = Bodies;

                if (bodies.Length == 0)
                {
                    var tbody = Owner.CreateElement(TagNames.Tbody);
                    AppendChild(tbody);
                }

                bodies[bodies.Length - 1].AppendChild(newRow);
            }
            else
            {
                rows[rows.Length - 1].ParentElement.AppendChild(newRow);
            }

            return newRow;
        }

        public void RemoveRowAt(Int32 index)
        {
            var rows = Rows;

            if (index >= 0 && index < rows.Length)
            {
                rows[index].Remove();
            }
        }

        public IHtmlTableSectionElement CreateHead()
        {
            var head = Head;

            if (head == null)
            {
                head = Owner.CreateElement(TagNames.Thead) as IHtmlTableSectionElement;
                AppendChild(head);
            }

            return head;
        }

        public IHtmlTableSectionElement CreateBody()
        {
            var lastBody = Bodies.LastOrDefault();
            var body = Owner.CreateElement(TagNames.Tbody) as IHtmlTableSectionElement;
            var length = ChildNodes.Length;
            var index = lastBody != null ? lastBody.Index() + 1 : length;

            if (index == length)
            {
                AppendChild(body);
            }
            else
            {
                InsertChild(index, body);
            }

            return body;
        }

        public void DeleteHead()
        {
            Head?.Remove();
        }

        public IHtmlTableSectionElement CreateFoot()
        {
            var foot = Foot;

            if (foot == null)
            {
                foot = Owner.CreateElement(TagNames.Tfoot) as IHtmlTableSectionElement;
                AppendChild(foot);
            }

            return foot;
        }

        public void DeleteFoot()
        {
            Foot?.Remove();
        }

        public IHtmlTableCaptionElement CreateCaption()
        {
            var caption = Caption;

            if (caption == null)
            {
                caption = Owner.CreateElement(TagNames.Caption) as IHtmlTableCaptionElement;
                InsertChild(0, caption);
            }

            return caption;
        }

        public void DeleteCaption()
        {
            Caption?.Remove();
        }

        #endregion
    }
}
