﻿namespace AngleSharp.Css.Dom
{
    using System;
    using System.Linq;

    /// <summary>
    /// Base class for all nth-child (or related) selectors.
    /// </summary>
    abstract class ChildSelector
    {
        #region Fields

        #endregion

        #region ctor

        public ChildSelector(String name, Int32 step, Int32 offset, ISelector kind)
        {
            Name = name;
            Step = step;
            Offset = offset;
            Kind = kind;
        }

        #endregion

        #region Properties

        public Priority Specificity
        {
            get
            {
                var specificity = Priority.OneClass;

                if (IncludeParameterInSpecificity)
                {
                    specificity += Kind is ListSelector list
                        ? list.Max(x => x.Specificity)
                        : Kind.Specificity;
                }

                return specificity;
            }
        }

        protected virtual Boolean IncludeParameterInSpecificity => false;

        public String Text
        {
            get
            {
                var a = Step.ToString();
                var b = String.Empty;
                var c = String.Empty;

                if (Offset > 0)
                {
                    b = "+";
                    c = (+Offset).ToString();
                }
                else if (Offset < 0)
                {
                    b = "-";
                    c = (-Offset).ToString();
                }

                return String.Format(":{0}({1}n{2}{3})", Name, a, b, c);
            }
        }

        public String Name { get; }

        public Int32 Step { get; }

        public Int32 Offset { get; }

        public ISelector Kind { get; }

        #endregion

        #region Methods

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Child(Name, Step, Offset, Kind);
        }

        #endregion
    }
}
