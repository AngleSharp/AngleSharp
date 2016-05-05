<Query Kind="Program" />

void Main()
{
	var testClass = new TestClass("SimpleEncodingTests");
	var files = Directory.GetFiles(".", "*.dat");
	var output = "SimpleEncoding.cs";
	
	foreach (var file in files)
	{
		var lines = File.ReadAllLines(file);
		
		for (var i = 0; i < lines.Length; i++)
		{		
			var current = new TestMethod();
			var tmp = new List<String>();
			
			while (lines[i] != "#data")
				i++;
			
			while (lines[++i] != "#encoding")
				tmp.Add(lines[i]);
				
			current.Source = String.Join("\n", tmp.ToArray());
			current.Encoding = lines[++i];			
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
		return methods.Select(m => m.ToFullCode());
	}
	
	public static String Tab(this String str, Int32 count = 1)
	{
       var lines = str.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
	   var tabs = new String('\t', count);

       for (int i = 0; i < lines.Length; i++)
           lines[i] = tabs + lines[i];

       return String.Join(Environment.NewLine, lines);
	}
	
	public static String ToIdent(this String str)
	{
		var textInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
		str = textInfo.ToTitleCase(str);
		return str.Replace("-", "");
	}
}

sealed class TestMethod
{
	public String Source
	{
		get;
		set;
	}
	
	public String Encoding
	{
		get;
		set;
	}
	
	public String ToFullCode()
	{
		var sb = new StringBuilder();
		sb.AppendLine("[Test]")
		  .Append("public void CorrectlyDetect").Append(Encoding.ToIdent()).Append("EncodingFromSourceWith").Append(Source.Length).AppendLine("Characters()")
		  .AppendLine("{")
		  .Append(this.ToCode().Tab())
		  .AppendLine("}");
		  
		return sb.ToString();
	}
	
	public String ToCode()
	{
		var sb = new StringBuilder();
		
		sb.AppendLine(@"var doc = NewDocument(@""SOURCE"");"
		  .Replace("SOURCE", Source.Replace("\"", "\"\"")));
		 
		sb.AppendLine(@"Assert.AreEqual(@""ENCODING"", doc.CharacterSet);"
		  .Replace("ENCODING", Encoding));
		
		return sb.ToString();
	}
}

sealed class TestClass
{
	readonly List<TestMethod> _methods;
	readonly String _name;

	public TestClass(String name)
	{
		_name = name;
		_methods = new List<TestMethod>();
	}
	
	public void AddTest(TestMethod method)
	{
		_methods.Add(method);
	}
	
	String Content
	{
		get { return String.Join(Environment.NewLine, _methods.ToCode()); }
	}

	public String ToCode()
	{
		return @"using NUnit.Framework;
using System.Globalization;

namespace AngleSharp.Core.Tests
{
	[TestFixture]
	public class CLASSNAME
	{
        static IDocument NewDocument(String source)
        {
            var configuration = new Configuration { Culture = new CultureInfo(""en-US"") };
            var content = Encoding.UTF8.GetBytes(source);
            var stream = new MemoryStream(content);
            return DocumentBuilder.Html(stream, configuration);
        }
		
CONTENT
	}
}"
			.Replace("CLASSNAME", _name)
			.Replace("CONTENT", Content.Tab(2))
;
	}
}