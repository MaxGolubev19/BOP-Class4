using System.Text;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task2.Task2;

namespace Task2;

public class Tests
{
    [Test]
    public void Main1Test()
    {
        var tmpFileName = Path.GetTempFileName();
        try
        {
            File.Copy("../../../data/text-utf8.txt", tmpFileName, true);
            Main(new[] { tmpFileName, "utf-8", "utf-16" });
            That(File.ReadAllBytes(tmpFileName), Is.EqualTo(File.ReadAllBytes("../../../data/text-windows-1251.txt")));
        }
        finally
        {
            File.Delete(tmpFileName);            
        }
    }

    [Test]
    public void Main2Test()
    {
        var tmpFileName = Path.GetTempFileName();
        try
        {
            File.Copy("../../../data/text-windows-1251.txt", tmpFileName, true);
            Main(new[] { tmpFileName, "utf-16", "utf-8"});
            That(File.ReadAllBytes(tmpFileName), Is.EqualTo(File.ReadAllBytes("../../../data/text-utf8.txt")));
        }
        finally
        {
            File.Delete(tmpFileName);            
        }
    }

}
