namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class UnorderedOptionsConverter<T1, T2> : IValueConverter<Tuple<T1, T2>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            _first = first;
            _second = second;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) && list.Count == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3> : IValueConverter<Tuple<T1, T2, T3>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            _first = first;
            _second = second;
            _third = third;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) && _third.VaryAll(list) && list.Count == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4> : IValueConverter<Tuple<T1, T2, T3, T4>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) && 
                _third.VaryAll(list) && _fourth.VaryAll(list) && list.Count == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5> : IValueConverter<Tuple<T1, T2, T3, T4, T5>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) &&
                _third.VaryAll(list) && _fourth.VaryAll(list) &&
                _fifth.VaryAll(list) && list.Count == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6> : IValueConverter<Tuple<T1, T2, T3, T4, T5, T6>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) &&
                _third.VaryAll(list) && _fourth.VaryAll(list) &&
                _fifth.VaryAll(list) && _sixth.VaryAll(list) &&
                list.Count == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7> : IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;
        readonly IValueConverter<T7> _seventh;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
            _seventh = seventh;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) &&
                _third.VaryAll(list) && _fourth.VaryAll(list) &&
                _fifth.VaryAll(list) && _sixth.VaryAll(list) &&
                _seventh.VaryAll(list) && list.Count == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7, T8> : IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;
        readonly IValueConverter<T7> _seventh;
        readonly IValueConverter<T8> _eighth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
            _seventh = seventh;
            _eighth = eighth;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryAll(list) && _second.VaryAll(list) && 
                _third.VaryAll(list) && _fourth.VaryAll(list) &&
                _fifth.VaryAll(list) && _sixth.VaryAll(list) &&
                _seventh.VaryAll(list) && _eighth.VaryAll(list) &&
                list.Count == 0;
        }
    }
}
