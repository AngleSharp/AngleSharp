namespace Samples.ViewModels
{
    using AngleSharp.Dom;
    using AngleSharp.Scripting.JavaScript;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    sealed class ReplViewModel : RequestViewModel
    {
        readonly String _prompt;
        readonly ObservableCollection<String> _items;
        readonly JavaScriptEngine _engine;
        IDocument _document;
        Boolean _readOnly;

        public ReplViewModel()
        {
            _readOnly = false;
            _engine = new JavaScriptEngine();
            _prompt = "$ ";
            _items = new ObservableCollection<String>();
            ClearCommand = new RelayCommand(() => Clear());
            ResetCommand = new RelayCommand(() => Reset());
            ExecuteCommand = new RelayCommand(cmd => Run(cmd.ToString()));
        }

        protected override async Task Use(Uri url, IDocument document, CancellationToken cancel)
        {
            Reset();
            _document = document;
            await Task.Yield();
        }

        public Boolean IsReadOnly
        {
            get { return _readOnly; }
            private set
            {
                _readOnly = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<String> Items
        {
            get { return _items; }
        }

        public String Prompt
        {
            get { return _prompt; }
        }

        public ICommand ClearCommand
        {
            get;
            private set;
        }

        public ICommand ResetCommand
        {
            get;
            private set;
        }

        public ICommand ExecuteCommand
        {
            get;
            private set;
        }

        void Clear()
        {
            _items.Clear();
        }

        void Reset()
        {
            _engine.Reset();
            Clear();
        }

        void Run(String command)
        {
            _items.Add(_prompt + command);
            _engine.Evaluate(command, new ScriptOptions
            {
                Document = _document,
                Context = _document.DefaultView
            });
            var lines = _engine.Result.ToString();

            foreach (var line in lines.Split(new [] { "\n" }, StringSplitOptions.None))
                _items.Add(line);
        }
    }
}