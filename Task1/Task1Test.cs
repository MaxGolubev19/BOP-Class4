using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

public class Tests
{
    [Test]
    public void IPv4AddrTest()
    {
        That(new IPv4Addr("127.0.0.1").IntValue, Is.EqualTo(2130706433u));
        That(new IPv4Addr("0.0.0.1").IntValue, Is.EqualTo(1u));
        That(new IPv4Addr("1.1.1.1").IntValue, Is.EqualTo(16843009u));
        That(new IPv4Addr("255.255.255.255").IntValue, Is.EqualTo(4294967295u));
    }

    [Test]
    public void CompareIPv4AddrTest()
    {
        That(new IPv4Addr("127.0.0.1"), Is.LessThan(new IPv4Addr("127.0.0.2")));
        That(new IPv4Addr("127.0.0.0"), Is.GreaterThan(new IPv4Addr("126.255.255.255")));
    }
    
    [Test]
    public void ParseArgsTest()
    {
        var expected = new IPLookupArgs("../../../data/query.ips", new List<string> { "../../../data/1.iprs", "../../../data/2.iprs" });
        That(ParseArgs(new []{"../../../data/query.ips", "../../../data/1.iprs", "../../../data/2.iprs"}), Is.EqualTo(expected));
        That(ParseArgs(new []{"../../../data/query.ips"}), Is.Null);
        That(ParseArgs(Array.Empty<string>()), Is.Null);
    }

    [Test]
    public void LoadQueryTest()
    {
        That(LoadQuery("../../../data/query.ips").ToList(), Has.Count.EqualTo(5));
    }
    
    [Test]
    public void LoadRangesTest()
    {
        var ranges = LoadRanges(new List<string> { "../../../data/1.iprs", "../../../data/2.iprs" });
        That(ranges, Has.Count.EqualTo(108));
    }
    
    [Test]
    public void LoadRangesEmptyTest()
    {
        var ranges = LoadRanges(new List<string>());
        That(FindRange(ranges, new IPv4Addr("60.161.226.166")), Is.Null);
    }
    
    [Test]
    public void LoadFindRangeTest()
    {
        var ranges = LoadRanges(new List<string>{"../../../data/1.iprs"});
        That(FindRange(ranges, new IPv4Addr("60.161.226.166")), Is.Not.Null);
        That(FindRange(ranges, new IPv4Addr("127.0.0.1")), Is.Null);
    }
}