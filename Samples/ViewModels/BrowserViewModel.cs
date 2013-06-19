using AngleSharp;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public class BrowserViewModel : RequestViewModel
    {
        ObservableCollection<TreeNodeViewModel> source;

        public BrowserViewModel ()
	    {
            Status = "Nothing to display ...";
            source = new ObservableCollection<TreeNodeViewModel>();
	    }

        public ObservableCollection<TreeNodeViewModel> Source
        {
            get { return source; }
        }

        public TreeNodeViewModel Root
        {
            set
            {
                source.Clear();
                source.Add(value);
            }
        }

        protected override async Task Use(Uri url, HTMLDocument document, CancellationToken cancel)
        {
            Status = "Constructing the DOM ...";
            Root = new TreeNodeViewModel(document);
            await Task.Yield();
        }
    }
}
