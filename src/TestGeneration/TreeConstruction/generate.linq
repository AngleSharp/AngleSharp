<Query Kind="Program" />

void Main()
{
	var testClass = new TestClass("TreeConstructionTests");
	var files = Directory.GetFiles(".", "*.dat");
	var output = "TreeConstruction.cs";
	
	foreach (var file in files)
	{
		var lines = File.ReadAllLines(file);
		
		for (var i = 0; i < lines.Length; i++)
		{		
			var current = new TestMethod();
			var tmp = new List<String>();
			
			while (lines[++i] != "#errors")
				tmp.Add(lines[i]);
				
			current.Source = String.Join("\n", tmp.ToArray());
			tmp.Clear();
			
			while (!lines[++i].StartsWith("#document"))
				tmp.Add(lines[i]);
			
			current.Errors = tmp.ToArray();
				
			if (lines[i] == "#document-fragment")
			{
				current.FragmentContext = lines[++i];
				i++;
			}
			
			var s = new Stack<TestElement>();
			
			while (++i < lines.Length && lines[i].StartsWith("|"))
			{
				var line = lines[i].Substring(2);
				var level = 0;
				var k = 0;
				
				while (line[k] == ' ')
				{
					k += 2;
					level++;
				}
				
				while (s.Count > level)
					s.Pop();
				
				line = line.Substring(k);
				TestNode node = null;
				
				if (line.StartsWith("<!DOCTYPE"))
				{
					var doctype = new TestDoctype();
					doctype.Name = line.GetTagContent().Substring(9);
					node = doctype;
				}
				else if (line.StartsWith("<!--"))
				{
					var comment = new TestComment();
					comment.Content = line.Substring(5).Replace(" -->", "");
					node = comment;
				}
				else if (line[0] == '<')
				{
					var element = new TestElement();
					var tag = line.GetTagContent();
					var idx = tag.IndexOf(' ');
					
					if(idx >= 0)
						element.Namespace = tag.Substring(0, idx);
					
					element.Tag = tag.Substring(idx + 1);
					node = element;
				}
				else if (line == "content")
				{
					node = new TestTemplate();
				}
				else if (line[0] == '"')
				{
					var text = new TestText();
					var sb = new StringBuilder();
					var finished = false;
					line = line.Substring(1);
					
					while (true)
					{
						for(var j = 0; j < line.Length; j++)
						{
							if(line[j] != '"')
								sb.Append(line[j]);
							else
								finished = true;
						}
						
						if (finished)
							break;
						
						sb.Append("\n");
						line = lines[++i];
					}
					
					text.Content = sb.ToString();
					node = text;
				}
				else
				{
					var idx = line.IndexOf('=');
					var key = line.Substring(0, idx);
					var value = line.Substring(idx + 1).Trim(new char[] { '"' });
					var sp = s.Peek();
					sp.Attributes.Add(new KeyValuePair<String, String>(key, value));
					continue;
				}
				
				if (s.Count > 0)
				{
					var sp = s.Peek();
					node.Parent = sp.ObjectName;
					node.Index = sp.Children.Count;
					
					if (node is TestTemplate)
						sp.Properties.Add(node);
					else
						sp.Children.Add(node);
				}
				else
				{
					node.Parent = "doc";
					node.Index = current.Elements.Count;
					current.Elements.Add(node);
				}
					
				if (node is TestElement)
					s.Push((TestElement)node);
			}
			
			testClass.AddTest(current);
		}
	}
	
	var content = testClass.ToCode();
	content.Dump();
	File.WriteAllText(output, content);
}

static class Extensions
{
	public static IEnumerable<String> ToCode(this IEnumerable<TestMethod> methods)
	{
		var nr = 0;
		return methods.Select(m => m.ToFullCode("TestMethod" + (nr++)));
	}

	public static String GetTagContent(this String line)
	{
		return line.Trim(new []{ '<', '>' });
	}
	
	public static String Tab(this String str, Int32 count = 1)
	{
       var lines = str.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
	   var tabs = new String('\t', count);

       for (int i = 0; i < lines.Length; i++)
           lines[i] = tabs + lines[i];

       return String.Join(Environment.NewLine, lines);
	}
}

sealed class TestMethod
{
	public TestMethod()
	{
		Elements = new List<TestNode>();
	}

	public String Source
	{
		get;
		set;
	}
	
	public String FragmentContext
	{
		get;
		set;
	}
	
	public String[] Errors
	{
		get;
		set;
	}
	
	public List<TestNode> Elements
	{
		get;
		private set;
	}
	
	public String ToFullCode(String name)
	{
		var sb = new StringBuilder();
		sb.AppendLine("[Test]")
		  .Append("public void ").Append(name).AppendLine("()")
		  .AppendLine("{")
		  .Append(this.ToCode().Tab())
		  .AppendLine("}");
		  
		return sb.ToString();
	}
	
	public String ToCode()
	{
		var sb = new StringBuilder();
		
		if(String.IsNullOrEmpty(FragmentContext))
		{
			sb.Append(@"var doc = DocumentBuilder.Html(@""SOURCE"");
			"
		.Replace("SOURCE", Source.Replace("\"", "\"\""))
			);
		}
		else
		{
			sb.Append(@"var doc = DocumentBuilder.HtmlFragment(@""SOURCE"", Factory.HtmlElements.Create(""CONTEXT"", null));
			"
		.Replace("SOURCE", Source)
		.Replace("CONTEXT", FragmentContext)
			);
		}
		
		foreach(var element in Elements)
			sb.AppendLine(element.ToCode());
		
		return sb.ToString();
	}
}

abstract class TestNode
{
	public String Parent
	{
		get;
		set;
	}
	
	public Int32 Index
	{
		get;
		set;
	}
	
	public abstract String ObjectName
	{
		get;
	}

	public abstract String ToCode();
}

sealed class TestClass : TestNode
{
	readonly List<TestMethod> _methods;
	readonly String _name;

	public TestClass(String name)
	{
		_name = name;
		_methods = new List<TestMethod>();
	}

	public override String ObjectName
	{
		get { return _name; }
	}
	
	public void AddTest(TestMethod method)
	{
		_methods.Add(method);
	}
	
	String Content
	{
		get { return String.Join(Environment.NewLine, _methods.ToCode()); }
	}

	public override String ToCode()
	{
		return @"using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;
using NUnit.Framework;
using System;

namespace AngleSharp.Core.Tests
{
	[TestFixture]
	public class CLASSNAME
	{
CONTENT
	}
}"
			.Replace("CLASSNAME", _name)
			.Replace("CONTENT", Content.Tab(2))
;
	}
}

sealed class TestComment : TestNode
{
	public String Content
	{
		get;
		set;
	}
	
	public override String ObjectName
	{
		get { return Parent + "Comment" + Index; }
	}
	
	public override String ToCode()
	{
		return @"
		var MYTAG = PARENT.ChildNodes[INDEX];
		Assert.AreEqual(NodeType.Comment, MYTAG.NodeType);
		Assert.AreEqual(@""CONTENT"", MYTAG.TextContent);
		"
		  .Replace("CONTENT", Content)
		  .Replace("MYTAG", ObjectName)
		  .Replace("PARENT", Parent)
		  .Replace("INDEX", Index.ToString());
	}
}

sealed class TestDoctype : TestNode
{
	public String Name
	{
		get;
		set;
	}
	
	public override String ObjectName
	{
		get { return Parent + "Type" + Index; }
	}
	
	public override String ToCode()
	{
		return @"
		var MYTAG = PARENT.ChildNodes[INDEX] as DocumentType;
		Assert.IsNotNull(MYTAG);
		Assert.AreEqual(NodeType.DocumentType, MYTAG.NodeType);
		Assert.AreEqual(@""CONTENT"", MYTAG.Name);"
		  .Replace("CONTENT", Name)
		  .Replace("MYTAG", ObjectName)
		  .Replace("PARENT", Parent)
		  .Replace("INDEX", Index.ToString());
	}
}

class TestTemplate : TestElement
{	
	public TestTemplate()
	{
		Tag = "Content";
	}

	public override String ObjectName
	{
		get { return Parent + Tag; }
	}
	
	public override String ToCode()
	{
		var sb = new StringBuilder();
		var myTag = ObjectName;
		
		sb.Append(@"
		var MYTAG = ((HTMLTemplateElement)PARENT).Content;"
			.Replace("PARENT", Parent)
			.Replace("MYTAG", myTag)
		);
		
		sb.Append(@"
		Assert.AreEqual(NUMBERNODES, MYTAG.ChildNodes.Length);
		Assert.AreEqual(NodeType.DocumentFragment, MYTAG.NodeType);
		"
			.Replace("NUMBERNODES", Children.Count.ToString())
			.Replace("MYTAG", myTag)
		);
		
		foreach(var child in Children)
			sb.AppendLine(child.ToCode());
		
		return sb.ToString();
	}
}

class TestElement : TestNode
{
	public TestElement()
	{
		Children = new List<TestNode>();
		Properties = new List<TestNode>();
		Attributes = new List<KeyValuePair<String, String>>();
	}
	
	static string Sanatize(String tag)
	{
		if (tag.Contains("-"))
			return tag.Replace("-", "");
			
		return tag;
	}
	
	public override String ObjectName
	{
		get { return Parent + Sanatize(Tag) + Index; }
	}
	
	public String Tag
	{
		get;
		set;
	}
	
	public String Namespace
	{
		get;
		set;
	}
	
	public List<KeyValuePair<String, String>> Attributes
	{
		get;
		set;
	}
	
	public List<TestNode> Children
	{
		get;
		private set;
	}
	
	public List<TestNode> Properties
	{
		get;
		private set;
	}
	
	public override String ToCode()
	{
		var sb = new StringBuilder();
		var myTag = ObjectName;
		
		sb.Append(@"
		var MYTAG = PARENT.ChildNodes[INDEX];"
			.Replace("PARENT", Parent)
			.Replace("MYTAG", myTag)
			.Replace("INDEX", Index.ToString())
		);
		
		sb.Append(@"
		Assert.AreEqual(NUMBERNODES, MYTAG.ChildNodes.Length);
		Assert.AreEqual(NUMBERATTS, ((Element)MYTAG).Attributes.Count);
		Assert.AreEqual(""TAG"", MYTAG.NodeName);
		Assert.AreEqual(NodeType.Element, MYTAG.NodeType);
		"
			.Replace("NUMBERNODES", Children.Count.ToString())
			.Replace("NUMBERATTS", Attributes.Count.ToString())
			.Replace("MYTAG", myTag)
			.Replace("TAG", Tag)
		);
		
		foreach(var attribute in Attributes)
			sb.Append(@"
		Assert.IsNotNull(((Element)MYTAG).GetAttribute(""NAME""));
		Assert.AreEqual(""VALUE"", ((Element)MYTAG).GetAttribute(""NAME""));
		"
			.Replace("NAME", attribute.Key)
			.Replace("VALUE", attribute.Value)
			.Replace("MYTAG", myTag));
		
		foreach(var child in Children)
			sb.Append(child.ToCode());
		
		foreach(var property in Properties)
			sb.Append(property.ToCode());
		
		return sb.ToString();
	}
}

sealed class TestText : TestNode
{
	public String Content
	{
		get;
		set;
	}
	
	public override String ObjectName
	{
		get { return Parent + "Text" + Index; }
	}
	
	public override String ToCode()
	{
		return @"
		var MYTAG = PARENT.ChildNodes[INDEX];
		Assert.AreEqual(NodeType.Text, MYTAG.NodeType);
		Assert.AreEqual(""CONTENT"", MYTAG.TextContent);
		"
			.Replace("MYTAG", ObjectName)
			.Replace("CONTENT", Content.Replace("\r\n", "\\n").Replace("\n", "\\n"))
		    .Replace("PARENT", Parent)
		    .Replace("INDEX", Index.ToString());
	}
}