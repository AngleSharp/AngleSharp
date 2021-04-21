var target = Argument("target", "Default");
var projectName = "AngleSharp";
var solutionName = "AngleSharp.Core";
var frameworks = new Dictionary<String, String>
{
    { "net46", "net46" },
    { "net461", "net461" },
    { "net472", "net472" },
    { "netstandard2.0", "netstandard2.0" },
    { "netcoreapp3.1", "netcoreapp3.1" },
};

#load tools/anglesharp.cake

RunTarget(target);
