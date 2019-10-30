var target = Argument("target", "Default");
var projectName = "AngleSharp";
var solutionName = "AngleSharp.Core";
var frameworks = new Dictionary<String, String>
{
    { "net461", "net461" },
    { "netstandard1.3", "netstandard1.3" },
    { "netstandard2.0", "netstandard2.0" },
};

#load tools/anglesharp.cake

RunTarget(target);
