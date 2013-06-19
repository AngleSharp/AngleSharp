using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Samples.ViewModels
{
    public class QueryViewModel : RequestViewModel
    {
        ObservableCollection<Element> source;
        String query;
        HTMLDocument document;
        Brush state;
        Int32 result;
        Int64 time;

        public QueryViewModel()
        {
            state = Brushes.LightGray;
            source = new ObservableCollection<Element>();
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

        public ObservableCollection<Element> Source
        {
            get { return source; }
        }

        protected override async Task Use(Uri url, HTMLDocument document, CancellationToken cancel)
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
                var sw = Stopwatch.StartNew();
                var elements = document.QuerySelectorAll(query);
                sw.Stop();
                source.Clear();

                foreach (var element in elements)
                    source.Add(element);

                State = Brushes.White;
                Time = sw.ElapsedMilliseconds;
                Result = elements.Length;
            }
            catch(DOMException)
            {
                State = Brushes.LightPink;
            }
        }
    }
}
