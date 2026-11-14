using Xunit;
using System.Xml.Linq;
using Orders.Application;
using Orders.Domain;
using Orders.Infrastructure;

namespace Orders.ArchitectureTests;

public sealed class DependencyRulesTests
{
    [Theory]
    [InlineData("Orders.Domain", "")]
    [InlineData("Orders.Application", "Orders.Domain")]
    [InlineData("Orders.Infrastructure", "Orders.Application,Orders.Domain")]
    [InlineData("Orders.Api", "Orders.Application,Orders.Domain,Orders.Infrastructure")]
    public void ProjectReferences_MatchApprovedLayers(string projectName, string expectedReferences)
    {
        string root = FindRepositoryRoot();
        XDocument project = XDocument.Load(Path.Combine(root, "src", projectName, projectName + ".csproj"));

        string[] actual = project.Descendants("ProjectReference")
            .Select(element => (string?)element.Attribute("Include"))
            .Where(path => path is not null)
            .Select(path => Path.GetFileNameWithoutExtension(path!))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        string[] expected = expectedReferences.Length == 0
            ? []
            : expectedReferences.Split(',').OrderBy(name => name, StringComparer.Ordinal).ToArray();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Domain_DoesNotReferenceApplicationOrInfrastructure()
    {
        string[] references = typeof(Order).Assembly.GetReferencedAssemblies()
            .Select(assembly => assembly.Name ?? string.Empty)
            .Where(name => name.StartsWith("Orders.", StringComparison.Ordinal))
            .ToArray();

        Assert.Empty(references);
    }

    [Fact]
    public void Application_ReferencesDomainOnly()
    {
        string[] references = typeof(OrderService).Assembly.GetReferencedAssemblies()
            .Select(assembly => assembly.Name ?? string.Empty)
            .Where(name => name.StartsWith("Orders.", StringComparison.Ordinal))
            .ToArray();

        Assert.Equal(new[] { "Orders.Domain" }, references.OrderBy(name => name).ToArray());
    }

    [Fact]
    public void Infrastructure_ImplementsApplicationContract()
    {
        Assert.True(typeof(IOrderRepository).IsAssignableFrom(typeof(InMemoryOrderRepository)));
    }

    private static string FindRepositoryRoot()
    {
        DirectoryInfo? directory = new(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "DotNetConf26.slnx")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Repository root not found.");
    }
}
