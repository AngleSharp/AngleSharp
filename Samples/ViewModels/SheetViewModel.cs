namespace Samples.ViewModels
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class SheetViewModel : RequestViewModel
    {
        ObservableCollection<IElement> source;
        IElement selected;
        Uri local;
        ObservableCollection<CssRuleViewModel> tree;
        CancellationTokenSource cts;
        Task populate;

        public SheetViewModel()
	    {
            Status = "Nothing to display ...";
            source = new ObservableCollection<IElement>();
            tree = new ObservableCollection<CssRuleViewModel>();
	    }

        public ObservableCollection<IElement> Source
        {
            get { return source; }
        }

        public ObservableCollection<CssRuleViewModel> Tree
        {
            get { return tree; }
        }

        public IElement Selected
        {
            get { return selected; }
            set 
            {
                selected = value;     
                RaisePropertyChanged();

                if (populate != null && !populate.IsCompleted)
                    cts.Cancel();

                populate = PopulateTree();
            }
        }

        async Task PopulateTree()
        {
            tree.Clear();
            cts = new CancellationTokenSource();
            var content = String.Empty;
            var token = cts.Token;

            if (selected is IHtmlLinkElement)
            {
                var http = new HttpClient { BaseAddress = local };
                ProfilerViewModel.Data.Start("Response (CSS)", OxyPlot.OxyColors.Blue);
                var request = await http.GetAsync(((IHtmlLinkElement)selected).Href, cts.Token);
                content = await request.Content.ReadAsStringAsync();
                ProfilerViewModel.Data.Stop();
                token.ThrowIfCancellationRequested();
            }
            else if (selected is IHtmlStyleElement)
                content = ((IHtmlStyleElement)selected).TextContent;
            
            ProfilerViewModel.Data.Start("Parsing (CSS)", OxyPlot.OxyColors.Violet);
            var css = DocumentBuilder.Css(content);
            ProfilerViewModel.Data.Stop();

            for (int i = 0; i < css.Rules.Length; i++)
                tree.Add(new CssRuleViewModel(css.Rules[i]));
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            local = url;
            Selected = null;
            source.Clear();
            Status = "Looking for stylesheets ...";

            foreach (var sheet in document.QuerySelectorAll("link,style"))
                source.Add(sheet);

            await Task.Yield();
        }
    }
}
