using FluentAssertions;

namespace Oma.WndwCtrl.CliOutputParser.Tests;

public class CliOutputParserImplTests
{
    private const string _testInputPing = """
                                          $ ping xkcd.com

                                          Pinging xkcd.com [151.101.64.67] with 32 bytes of data:
                                          Reply from 151.101.64.67: bytes=32 time=8ms TTL=59
                                          Reply from 151.101.64.67: bytes=32 time=9ms TTL=59
                                          Reply from 151.101.64.67: bytes=32 time=8ms TTL=59
                                          Reply from 151.101.64.67: bytes=32 time=8ms TTL=59

                                          Ping statistics for 151.101.64.67:
                                              Packets: Sent = 4, Received = 4, Lost = 0 (0% loss),
                                          Approximate round trip times in milli-seconds:
                                              Minimum = 8ms, Maximum = 9ms, Average = 8ms
                                          """;

    private const string _testInputNested = """
                                            1 2 3
                                            4 5 6
                                            7 8 9
                                            """;

    private readonly CliOutputParserImpl _instance;

    public CliOutputParserImplTests()
    {
        _instance = new();
    }

    [Fact]
    public void ShouldParseTransformationSuccessfully()
    {
        const string transformationInput = """
                                           Anchor.From("Pinging xkcd.com");
                                           Anchor.To("Ping statistics");
                                           Regex.Match($"time=(\d+)ms");
                                           Regex.YieldGroup(1); 
                                           Values.Average();
                                           """;

        var action = () => _instance.Parse(transformationInput, _testInputPing);

        action.Should().NotThrow();
    }

    [Fact]
    public void ShouldFailOnExtraneousInput()
    {
        const string transformationInput = """
                                           Anchor.From("Pinging xkcd.com").To("Ping statistics");
                                           Regex.Match($"time=(\d+)ms").YieldGroup(1); 
                                           Values.Average2();
                                           """;

        var action = () => _instance.Parse(transformationInput, _testInputPing);

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void ShouldApplyAnchors()
    {
        const string transformationInput = """
                                           Anchor.From("statistics");
                                           Anchor.To("151.101.64.67");
                                           """;

        string output = string.Empty;

        var action = () => { output = _instance.Parse(transformationInput, _testInputPing); };

        action.Should().NotThrow();
        output.Should().Be("statistics for 151.101.64.67");
    }

    [Fact]
    public void ShouldHandleNestedTransformations()
    {
        const string transformationInput = """
                                           Regex.Match($"^.*$");
                                           Regex.Match($"\d");
                                           Values.First();
                                           Values.First();
                                           """;

        string output = string.Empty;

        var action = () => { output = _instance.Parse(transformationInput, _testInputNested); };

        action.Should().NotThrow();
        output.Should().Be("1");
    }
}