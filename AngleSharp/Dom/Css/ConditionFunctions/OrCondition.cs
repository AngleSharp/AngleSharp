namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;
    using System.Linq;

    sealed class OrCondition : CssNode, IConditionFunction
    {
        public Boolean Check()
        {
            var conditions = Children.OfType<IConditionFunction>();

            foreach (var condition in conditions)
            {
                if (condition.Check())
                {
                    return true;
                }
            }

            return false;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var conditions = Children.OfType<IConditionFunction>();
            var first = true;

            foreach (var condition in conditions)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    writer.Write(" or ");
                }

                condition.ToCss(writer, formatter);
            }
        }
    }
}
