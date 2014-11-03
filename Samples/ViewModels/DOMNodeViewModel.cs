namespace Samples.ViewModels
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    public class DOMNodeViewModel : BaseViewModel
    {
        ObservableCollection<DOMNodeViewModel> children;
        String name;
        String typeName;
        String value;
        Boolean selected;
        Boolean expanded;
        Boolean populated;
        Object element;
        DOMNodeViewModel parent;

        public DOMNodeViewModel(Object nodeElement, String nodeName = "document", DOMNodeViewModel nodeParent = null)
        {
            element = nodeElement;
            parent = nodeParent;
            children = new ObservableCollection<DOMNodeViewModel>();
            name = nodeName;

            if (nodeElement == null)
            {
                populated = true;
                typeName = "<null>";
            }
            else if (nodeParent == null)
            {
                CreateChildren();
                IsExpanded = true;
                IsSelected = true;
            }
        }

        public String Name
        {
            get { return name; }
        }

        public String Value
        {
            get { return value; }
            set
            {
                this.value = value;
                RaisePropertyChanged();
            }
        }

        public DOMNodeViewModel Parent
        {
            get { return parent; }
        }

        public String TypeName
        {
            get { return typeName; }
        }

        public ObservableCollection<DOMNodeViewModel> Children
        {
            get { return children; }
        }

        public Boolean IsSelected
        {
            get { return selected; }
            set
            {
                selected = value;
                RaisePropertyChanged();
            }
        }

        public Boolean IsExpanded
        {
            get { return expanded; }
            set
            {
                expanded = value;

                foreach (var child in children)
                    child.CreateChildren();

                RaisePropertyChanged();
            }
        }

        void CreateChildren()
        {
            if (!populated)
            {
                var type = element.GetType();
                typeName = type.Name;
                SetMembers(type);
                populated = true;
            }
        }

        void SetMembers(Type type)
        {
            if (type.GetCustomAttribute<DomNameAttribute>() == null)
            {
                foreach (var contract in type.GetInterfaces())
                    SetMembers(contract);
            }
            else
                SetProperties(type.GetProperties());
        }

        void SetProperties(IEnumerable<PropertyInfo> properties)
        {
            var hv = true;

            foreach (var property in properties)
            {
                var names = property.GetCustomAttributes<DomNameAttribute>();

                foreach (var name in names.Select(m => m.OfficialName))
                {
                    hv = false;

                    switch (property.GetIndexParameters().Length)
                    {
                        case 0:
                        {
                            var value = property.GetValue(element);
                            children.Add(new DOMNodeViewModel(value, name, this));
                            break;
                        }
                        case 1:
                        {
                            if (element is IEnumerable)
                            {
                                var collection = (IEnumerable)element;
                                var index = 0;
                                var idx = new object[1];

                                foreach (var item in collection)
                                {
                                    idx[0] = index;
                                    children.Add(new DOMNodeViewModel(item, "[" + index.ToString() + "]", this));
                                    index++;
                                }
                            }
                            break;
                        }
                    }
                }
            }

            if (hv) 
                Value = element.ToString();
        }
    }
}
