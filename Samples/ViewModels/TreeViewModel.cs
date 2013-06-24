using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public class TreeViewModel : RequestViewModel
    {
        ObservableCollection<TreeNodeViewModel> nodes;

        public TreeViewModel()
        {
            nodes = new ObservableCollection<TreeNodeViewModel>();
        }

        protected override async Task Use(Uri url, HTMLDocument document, CancellationToken cancel)
        {
            nodes.Clear();
            Status = "Constructing tree ...";
            var elements = TreeNodeViewModel.SelectFrom(document.ChildNodes);

            foreach (var element in elements)
            {
                element.Parent = nodes;
                nodes.Add(element);
            }

            await Task.Yield();
        }

        public ObservableCollection<TreeNodeViewModel> Tree
        {
            get { return nodes; }
        }
    }
}
