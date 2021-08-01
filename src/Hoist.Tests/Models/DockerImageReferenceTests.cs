using FluentAssertions;
using Hoist.Models;
using Xunit;

namespace Hoist.Tests.Models
{
    public class DockerImageReferenceTests
    {
        [Theory]
        [ClassData(typeof(DockerImageReferenceTestData))]
        public void ShouldParseTheory(string input, DockerImageReference expected)
        {
            var actual = DockerImageReference.Parse(input, null);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
