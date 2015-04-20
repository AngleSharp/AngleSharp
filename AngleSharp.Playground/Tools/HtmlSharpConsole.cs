namespace AngleSharp.Playground.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom;

    class HtmlSharpConsole
    {
        readonly Stack<INode> previous;
        INode current;
        String[] methods;
        String[] properties;

        public HtmlSharpConsole(INode current)
        {
            previous = new Stack<INode>();
            Current = current;
        }

        public INode Current
        {
            get { return current; }
            private set
            {
                current = value;
                var type = current.GetType();
                methods = type.GetMethods().Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_")).Select(m => m.Name).ToArray();
                properties = type.GetProperties().Select(m => m.Name).ToArray();
            }
        }

        public void Capture()
        {
            while (true)
            {
                Console.Write(current.NodeName);
                Console.Write(" >> ");

                var input = Console.ReadLine().Trim();

                if (input.Equals("exit"))
                {
                    break;
                }
                else if (String.IsNullOrEmpty(input))
                {
                    continue;
                }
                else if (input.Equals("cls"))
                {
                    Console.Clear();
                    continue;
                }
                else if (input.Equals("help"))
                {
                    Console.WriteLine("Type `help methods`, `help properties` or `help all`.");
                    continue;
                }
                else if (input.Equals("help methods"))
                {
                    PrintMethods();
                    continue;
                }
                else if (input.Equals("help properties"))
                {
                    PrintProperties();
                    continue;
                }
                else if (input.Equals("help all"))
                {
                    PrintMethods();
                    Console.WriteLine();
                    PrintProperties();
                    continue;
                }
                else if (input.Equals("back"))
                {
                    if (previous.Count > 0)
                        Current = previous.Pop();

                    continue;
                }

                if (input.Contains('('))
                {
                    var index = input.IndexOf('(');
                    var name = input.Substring(0, index);
                    var args = input.Substring(index + 1, input.Length - 2 - index);

                    if (methods.Contains(name))
                    {
                        var list = GetArgs(args);

                        try
                        {
                            var value = current.GetType().GetMethod(name).Invoke(current, list);
                            Output(value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        continue;
                    }
                }
                else
                {
                    if (properties.Contains(input))
                    {
                        var value = current.GetType().GetProperty(input).GetValue(current);
                        Output(value);
                        continue;
                    }
                }

                Console.WriteLine("The given command was not found ...");
            }
        }

        private void Output(Object value)
        {
            var node = value as INode;

            if (node != null)
            {
                previous.Push(node);
                Current = node;
            }

            Console.WriteLine(value);
        }

        private void PrintProperties()
        {
            Console.WriteLine("Available properties:");
            Console.WriteLine("---------------------");

            for (int i = 0; i < properties.Length; i++)
                Console.WriteLine(properties[i]);
        }

        private void PrintMethods()
        {
            Console.WriteLine("Available methods:");
            Console.WriteLine("------------------");

            for (int i = 0; i < methods.Length; i++)
            {
                Console.Write(methods[i]);
                Console.WriteLine("()");
            }
        }

        private Object[] GetArgs(String args)
        {
            var list = new List<Object>();
            var index = 0;
            var str = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (!str)
                {
                    if (args[i] == '"')
                    {
                        index = i + 1;
                        str = true;
                    }
                    else if (args[i] == ',')
                    {
                        if (index != 1)
                        {
                            var element = args.Substring(index, i - index);
                            list.Add(GetArg(element));
                        }

                        index = i + 1;
                    }
                }
                else if (args[i] == '"')
                {
                    list.Add(args.Substring(index, i - index));
                    index = -1;
                    str = false;
                }
            }


            if (index != -1 && index < args.Length)
            {
                if (!str)
                    list.Add(GetArg(args.Substring(index, args.Length - index)));
                else
                    list.Add(args.Substring(index, args.Length - index));
            }
            
            return list.ToArray();
        }

        private Object GetArg(String arg)
        {
            var integer = 0;
            var number = 0.0;

            if (Int32.TryParse(arg, out integer))
                return integer;
            else if (Double.TryParse(arg, out number))
                return number;

            return new Object();
        }
    }
}
