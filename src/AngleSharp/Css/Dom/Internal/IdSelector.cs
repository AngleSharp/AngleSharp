namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class IdSelector : ISelector
    {
        private readonly String _id;

        public IdSelector(String id)
        {
            _id = id;
        }

        public Priority Specificity => Priority.OneId;

        public String Text => "#" + _id;

        public void Accept(ISelectorVisitor visitor) => visitor.Id(_id);

        public Boolean Match(IElement element, IElement scope) => element.Id.Is(_id);
    }
}
