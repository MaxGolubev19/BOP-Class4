namespace Task1
{
    // Необходимо заменить на более подходящий тип (коллекцию), позволяющий
    // эффективно искать диапазон по заданному IP-адресу
    using IPRangesDatabase = Object;

    public class Task1
    {
        /*
        * Объекты этого класса создаются из строки, но хранят внутри помимо строки
        * ещё и целочисленное значение соответствующего адреса. Например, для адреса
         * 127.0.0.1 должно храниться число 1 + 0 * 2^8 + 0 * 2^16 + 127 * 2^24 = 2130706433.
        */
        internal record IPv4Addr (string StrValue) : IComparable<IPv4Addr>
        {
            internal uint IntValue = Ipstr2Int();

            private static uint Ipstr2Int()
            {
                throw new NotImplementedException();
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

        internal record class IPRange(IPv4Addr IpFrom, IPv4Addr IpTo)
        {
            public override string ToString()
            {
                return $"{IpFrom},{IpTo}";
            }
        }

        internal record class IPLookupArgs(string IpsFile, List<string> IprsFiles);
       
        internal static IPLookupArgs? ParseArgs(string[] args)
        {
            throw new NotImplementedException();
        }

        internal static List<string> LoadQuery(string filename) {
            throw new NotImplementedException();
        }

        internal static IPRangesDatabase LoadRanges(List<String> filenames) {
            throw new NotImplementedException();
        }

        internal static IPRange? FindRange(IPRangesDatabase ranges, IPv4Addr query) {
            throw new NotImplementedException();
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
                foreach (var ip in queries)
                {
                    var findRange = FindRange(ranges, new IPv4Addr(ip));
                    var result = TODO<string>();
                    Console.WriteLine($"{ip}: {result}");
                }
        }
        
        private static T TODO<T>()
        {
            throw new NotImplementedException();
        }
    }
}