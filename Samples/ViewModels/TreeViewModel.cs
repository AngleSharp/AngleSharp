namespace Samples.ViewModels
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;

    public class TreeViewModel : RequestViewModel
    {
        ObservableCollection<TreeNodeViewModel> nodes;

        public TreeViewModel()
        {
            nodes = new ObservableCollection<TreeNodeViewModel>();
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
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
