namespace Samples.ViewModels
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media;

    public class QueryViewModel : RequestViewModel
    {
        ObservableCollection<IElement> source;
        String query;
        IDocument document;
        Brush state;
        Int32 result;
        Int64 time;

        public QueryViewModel()
        {
            state = Brushes.LightGray;
            source = new ObservableCollection<IElement>();
            query = "*";
        }

        public Int32 Result
        {
            get { return result; }
            set
            {
                result = value;
                RaisePropertyChanged();
            }
        }

        public Int64 Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged();
            }
        }

        public String Query
        {
            get { return query; }
            set
            {
                query = value;
                ChangeQuery();
                RaisePropertyChanged();
            }
        }

        public Brush State
        {
            get { return state; }
            set
            {
                state = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IElement> Source
        {
            get { return source; }
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            State = Brushes.LightGray;
            this.document = document;
            ChangeQuery();
            await Task.Yield();
        }

        void ChangeQuery()
        {
            if (document == null)
                return;

            State = Brushes.LightGreen;

            try
            {
                ProfilerViewModel.Data.Start("Query", OxyPlot.OxyColors.SteelBlue);
                var sw = Stopwatch.StartNew();
                var elements = document.QuerySelectorAll(query);
                sw.Stop();
                ProfilerViewModel.Data.Stop();
                source.Clear();

                foreach (var element in elements)
                    source.Add(element);

                State = Brushes.White;
                Time = sw.ElapsedMilliseconds;
                Result = elements.Length;
            }
            catch(DomException)
            {
                State = Brushes.LightPink;
            }
        }
    }
}
