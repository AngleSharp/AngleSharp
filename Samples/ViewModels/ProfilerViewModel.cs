namespace Samples.ViewModels
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProfilerViewModel : RequestViewModel
    {
        static ProfilerViewModel _data;

        Collection<Item> _items;
        Stopwatch _sw;
        Item _tmp;

        ProfilerViewModel()
        {
            _sw = new Stopwatch();
            _items = new Collection<Item>();
        }

        public static ProfilerViewModel Data
        {
            get { return _data ?? (_data = new ProfilerViewModel()); }
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            await Task.Yield();
        }

        public Collection<Item> Items
        {
            get { return _items; }
        }

        public void Start(String label, OxyPlot.OxyColor color)
        {
            _tmp = new Item { Label = label, Color = color };
            _sw.Restart();
        }

        public void Stop()
        {
            _sw.Stop();
            _tmp.Value = (Double)_sw.ElapsedMilliseconds;
            var item = _items.Where(m => m.Label == _tmp.Label).SingleOrDefault();

            if(item != null)
                _items.Remove(item);

            _items.Add(_tmp);
        }

        public class Item
        {
            public String Label
            {
                get;
                set;
            }

            public Double Value
            {
                get;
                set;
            }

            public OxyPlot.OxyColor Color
            {
                get;
                set;
            }
        }
    }
}
