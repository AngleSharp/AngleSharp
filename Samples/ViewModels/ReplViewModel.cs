namespace Samples.ViewModels
{
    using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

    sealed class ReplViewModel : BaseViewModel
    {
        readonly String _prompt;
        readonly ObservableCollection<String> _items;
        Boolean _readOnly;

        public ReplViewModel()
        {
            _readOnly = false;
            _prompt = "$ ";
            _items = new ObservableCollection<String>();
            ClearCommand = new RelayCommand(() => Clear());
            ResetCommand = new RelayCommand(() => Reset());
            ExecuteCommand = new RelayCommand(cmd => Run(cmd.ToString()));
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
            //TODO Reset global context
            Clear();
        }

        void Run(String command)
        {
            _items.Add(_prompt + command);
            var lines = "OUTPUT";

            foreach (var line in lines.Split(new [] { Environment.NewLine }, StringSplitOptions.None))
                _items.Add(line);
        }
    }
}
