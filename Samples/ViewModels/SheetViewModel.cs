using AngleSharp;
using AngleSharp.DOM;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public class SheetViewModel : RequestViewModel
    {
        ObservableCollection<IStyleSheet> source;
        IStyleSheet selected;
        Uri local;
        ObservableCollection<CssRuleViewModel> tree;
        CancellationTokenSource cts;
        Task populate;

        public SheetViewModel()
	    {
            Status = "Nothing to display ...";
            source = new ObservableCollection<IStyleSheet>();
            tree = new ObservableCollection<CssRuleViewModel>();
	    }

        public ObservableCollection<IStyleSheet> Source
        {
            get { return source; }
        }

        public ObservableCollection<CssRuleViewModel> Tree
        {
            get { return tree; }
        }

        public IStyleSheet Selected
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

            if (String.IsNullOrEmpty(selected.Href))
                content = selected.OwnerNode.TextContent;
            else
            {
                var http = new HttpClient { BaseAddress = local };
                ProfilerViewModel.Data.Start("Response (CSS)", OxyPlot.OxyColors.Blue);
                var request = await http.GetAsync(selected.Href, cts.Token);
                content = await request.Content.ReadAsStringAsync();
                ProfilerViewModel.Data.Stop();
                token.ThrowIfCancellationRequested();
            }

            ProfilerViewModel.Data.Start("Parsing (CSS)", OxyPlot.OxyColors.Violet);
            var css = DocumentBuilder.Css(content);
            ProfilerViewModel.Data.Stop();

            for (int i = 0, j = 0; i < css.Rules.Length; i++, j++)
            {
                tree.Add(new CssRuleViewModel(css.Rules[i]));

                if (j == 80)
                {
                    j = 0;
                    await Task.Delay(1, cts.Token);
                }
            }
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            local = url;
            Selected = null;
            source.Clear();
            Status = "Looking for stylesheets ...";

            for (int i = 0; i < document.StyleSheets.Length; i++)
            {
                var s = document.StyleSheets[i];
                source.Add(s);
            }

            await Task.Yield();
        }
    }
}
