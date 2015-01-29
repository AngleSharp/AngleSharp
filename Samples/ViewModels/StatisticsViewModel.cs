namespace Samples.ViewModels
{
    using AngleSharp.Dom;
    using OxyPlot;
    using OxyPlot.Series;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class StatisticsViewModel : RequestViewModel
    {
        PlotModel mostElements;
        PlotModel mostClasses;
        PlotModel mostWords;
        PlotModel various;
        PlotModel mostAttributes;

        public PlotModel MostElements
        {
            get { return mostElements; }
            set
            {
                mostElements = value;
                RaisePropertyChanged();
            }
        }

        public PlotModel MostAttributes
        {
            get { return mostAttributes; }
            set
            {
                mostAttributes = value;
                RaisePropertyChanged();
            }
        }

        public PlotModel MostClasses
        {
            get { return mostClasses; }
            set
            {
                mostClasses = value;
                RaisePropertyChanged();
            }
        }

        public PlotModel MostWords
        {
            get { return mostWords; }
            set
            {
                mostWords = value;
                RaisePropertyChanged();
            }
        }

        public PlotModel Various
        {
            get { return various; }
            set
            {
                various = value;
                RaisePropertyChanged();
            }
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            var elements = new Dictionary<String, Int32>();
            var attributes = new Dictionary<String, Int32>();
            var classes = new Dictionary<String, Int32>();
            var words = new Dictionary<String, Int32>();
            var various = new Dictionary<String, Int32>();

            Status = "Gathering statistics ...";

            various.Add("Images", document.Images.Length);
            various.Add("Scripts", document.Scripts.Length);
            various.Add("Stylesheets", document.StyleSheets.Length);
            various.Add("Plugins", document.Plugins.Length);
            various.Add("Forms", document.Forms.Length);

            await Task.Run(() => Inspect(document.DocumentElement, elements, classes, attributes));
            cancel.ThrowIfCancellationRequested();
            await Task.Run(() => Words(document.DocumentElement.TextContent.ToCharArray(), words));
            cancel.ThrowIfCancellationRequested();

            MostElements = CreatePieChart("Most elements", elements);
            MostClasses = CreatePieChart("Most classes", classes);
            MostWords = CreatePieChart("Most words", words);
            MostAttributes = CreatePieChart("Most attributes", attributes);
        }

        PlotModel CreatePieChart(String title, Dictionary<String, Int32> data)
        {
            var pm = new PlotModel(title);
            var ps = new PieSeries();
            pm.PlotMargins = new OxyThickness(0);
            pm.Padding = new OxyThickness(0);

            if (data.Count > 0)
            {
                var ranking = data.OrderByDescending(m => m.Value).Take(8);

                foreach (var element in ranking)
                    ps.Slices.Add(new PieSlice(element.Key, element.Value));
            }

            ps.InnerDiameter = 0.2;
            ps.ExplodedDistance = 0;
            ps.Stroke = OxyColors.White;
            ps.StrokeThickness = 1.0;
            ps.AngleSpan = 360;
            ps.StartAngle = 0;

            pm.Series.Add(ps);
            return pm;
        }

        void Words(Char[] content, Dictionary<String, Int32> words)
        {
            var index = 0;
            var length = 0;

            for (var i = 0; i < content.Length; i++)
            {
                if (!Char.IsLetter(content[i]))
                {
                    length = i - index;

                    if (length > 1)
                    {
                        var word = new String(content, index, length);

                        if (words.ContainsKey(word))
                            words[word]++;
                        else
                            words.Add(word, 1);
                    }

                    index = i + 1;
                }
            }
        }

        void Inspect(IElement element, Dictionary<String, Int32> elements, Dictionary<String, Int32> classes, Dictionary<String, Int32> attributes)
        {
            if (elements.ContainsKey(element.LocalName))
                elements[element.LocalName]++;
            else
                elements.Add(element.LocalName, 1);

            foreach (var cls in element.ClassList)
            {
                if (classes.ContainsKey(cls))
                    classes[cls]++;
                else
                    classes.Add(cls, 1);
            }

            foreach (var attr in element.Attributes)
            {
                if (attributes.ContainsKey(attr.Name))
                    attributes[attr.Name]++;
                else
                    attributes.Add(attr.Name, 1);
            }

            foreach (var child in element.Children)
                Inspect(child, elements, classes, attributes);
        }
    }
}
