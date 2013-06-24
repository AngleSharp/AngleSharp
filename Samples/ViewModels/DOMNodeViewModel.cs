using AngleSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Samples.ViewModels
{
    public class DOMNodeViewModel : BaseViewModel
    {
        ObservableCollection<DOMNodeViewModel> children;
        String name;
        String typeName;
        String value;
        Boolean selected;
        Boolean expanded;
        Boolean populated;
        Type type;
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
                var hv = true;
                populated = true;
                type = element.GetType();
                typeName = FindName(type);

                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                    .Where(m => m.GetCustomAttributes(typeof(DOMAttribute), false).Length > 0)
                    .OrderBy(m => m.Name);

                foreach (var property in properties)
                {
                    hv = false;

                    switch(property.GetIndexParameters().Length)
                    {
                        case 0:
                            children.Add(new DOMNodeViewModel(property.GetValue(element), FindName(property), this));
                            break;
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
                            }
                            break;
                    }
                }

                if (hv) Value = element.ToString();
            }
        }

        String FindName(MemberInfo member)
        {
            var objs = member.GetCustomAttributes(typeof(DOMAttribute), false);

            if (objs.Length == 0)
                return member.Name;

            return ((DOMAttribute)objs[0]).OfficialName;
        }
    }
}
