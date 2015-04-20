namespace AngleSharp.Playground.Tools
{
    using System;
    using System.Linq;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;

    class ConsoleForm
    {
        IHtmlFormElement _form;
        private string p;

        public ConsoleForm(IDocument document)
            : this(document.Forms.FirstOrDefault() ?? document.CreateElement<IHtmlFormElement>())
        {
        }

        public ConsoleForm(IHtmlFormElement form)
        {
            _form = form;
        }

        public void FillInteractive()
        {
            Console.WriteLine("---------");

            foreach (var element in _form.Elements)
            {
                if (element is IHtmlInputElement)
                {
                    var input = (IHtmlInputElement)element;
                    var type = input.Type;

                    if (type == "hidden")
                    {
                        continue;
                    }

                    if (type == "checkbox")
                    {
                        Console.WriteLine("Current value for {0}: {1}", input.Name, input.IsChecked);

                        if (RequestNewValue())
                        {
                            Console.Write("Nothing for false, something for true: ");
                            input.IsChecked = !String.IsNullOrEmpty(Console.ReadLine());
                        }

                        continue;
                    }

                    Console.WriteLine("Current value for {0}: {1}", input.Name, input.Value);

                    if (RequestNewValue())
                    {
                        Console.Write("Enter value: ");
                        input.Value = Console.ReadLine();
                    }
                }
            }

            Console.WriteLine("---------");
        }

        static Boolean RequestNewValue()
        {
            while (true)
            {
                Console.Write("New value? (y)es (n)o: ");
                var value = Console.ReadLine();

                if (value.Equals("y", StringComparison.OrdinalIgnoreCase) || value.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (value.Equals("n", StringComparison.OrdinalIgnoreCase) || value.Equals("no", StringComparison.OrdinalIgnoreCase))
                    return false;

                Console.WriteLine("Invalid value. Please try again ...");
            }
        }

        public void Submit()
        {
            var document = _form.Submit().Result;
            Console.WriteLine(document.ToHtml());
            Console.ReadLine();
        }
    }
}
