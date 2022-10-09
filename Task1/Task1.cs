using static Task1.Task1;

namespace Task1
{
    using NUnit.Framework;
    using System.Net.NetworkInformation;
    using System.Text;
    // Необходимо заменить на более подходящий тип (коллекцию), позволяющий
    // эффективно искать диапазон по заданному IP-адресу
    using IPRangesDatabase = List<IPRange>;

    public class Task1
    {
        /*
        * Объекты этого класса создаются из строки, но хранят внутри помимо строки
        * ещё и целочисленное значение соответствующего адреса. Например, для адреса
         * 127.0.0.1 должно храниться число 1 + 0 * 2^8 + 0 * 2^16 + 127 * 2^24 = 2130706433.
        */
        internal record IPv4Addr (string StrValue) : IComparable<IPv4Addr>
        {
            internal uint IntValue = Ipstr2Int(StrValue);

            private static uint Ipstr2Int(string StrValue)
            {
                var strNumbers = StrValue.Split('.');
                uint uintValue = 0;

                for (int i = 0; i < strNumbers.Length; i++)
                {
                    uintValue += uint.Parse(strNumbers[strNumbers.Length - i - 1]) * (uint)Math.Pow(2, i * 8);
                }

                return uintValue;
            }

            // Благодаря этому методу мы можем сравнивать два значения IPv4Addr
            public int CompareTo(IPv4Addr other)
            {
                return IntValue.CompareTo(other.IntValue);
            }

            public override string ToString()
            {
                return StrValue;
            }
        }

        //Класс для хранения диапазонов
        internal record class IPRange(IPv4Addr IpFrom, IPv4Addr IpTo)
        {
            public override string ToString()
            {
                return $"{IpFrom}, {IpTo}";
            }
        }

        //Класс для хранения входных данных
        internal record class IPLookupArgs(string IpsFile, List<string> IprsFiles)
        {
            public virtual bool Equals(IPLookupArgs other)
            {
                if (other == null)
                    return false;
                return IpsFile.Equals(other.IpsFile) && Enumerable.SequenceEqual(IprsFiles, other.IprsFiles);
            }
        }
       
        //Преобразование входных данных
        internal static IPLookupArgs? ParseArgs(string[] args)
        {
            if (args.Count() < 2)
                return null;

            var ipsFile = args[0];
            List<string> iprsFiles = new ArraySegment<string>(args, 1, args.Length - 1).ToList();

            return new IPLookupArgs(ipsFile, iprsFiles);
        }

        //Создание списка ip-адресов
        internal static List<string> LoadQuery(string filename)
        {
            return new List<string>(File.ReadAllLines(filename));
        }

        //Создание коллекции диапазонов
        internal static IPRangesDatabase LoadRanges(List<String> filenames) 
        {
            var ipRangesDatabase = new IPRangesDatabase();

            foreach (var filename in filenames)
            {
                var ipRanges = new List<string>(File.ReadAllLines(filename));

                foreach (var strIpRange in ipRanges)
                {
                    var strIps = strIpRange.Split(',');

                    var ipRange = new IPRange(new IPv4Addr(strIps[0]), new IPv4Addr(strIps[1]));

                    ipRangesDatabase.Add(ipRange);
                }
            }

            return ipRangesDatabase;
        }

        //Поиск диапазона, в котором лежит ip
        internal static IPRange? FindRange(IPRangesDatabase ranges, IPv4Addr query) 
        {
            foreach (var range in ranges)
            {
                if (query.CompareTo(range.IpFrom) >= 0 && query.CompareTo(range.IpTo) <= 0)
                    return range;
            }

            return null;
        }
        
        public static void Main(string[] args)
        {
            var ipLookupArgs = ParseArgs(args);
            if (ipLookupArgs == null)
            {
                return;
            }

            var queries = LoadQuery(ipLookupArgs.IpsFile);
            var ranges = LoadRanges(ipLookupArgs.IprsFiles);
            var file = args[0];

            File.WriteAllText(file, string.Empty);

            foreach (var ip in queries)
            {
                var findRange = FindRange(ranges, new IPv4Addr(ip));
                var result = PrintResult(ip, findRange);

                File.AppendAllText(file, result + Environment.NewLine);
            }
        }
        
        //Форматирование результата
        private static string PrintResult(string ip, IPRange? findRange)
        {
            if (findRange == null)
                return $"{ip}: NO";
            else
                return $"{ip}: YES ({findRange})";
        }
    }
}