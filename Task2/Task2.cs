using System.Text;

namespace Task2
{
    public class Task2
    {
        //����� ������ � �������
        public static void PrintError(string textError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textError);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //������ ���������
        public static void CodingError(string codingName)
        {
            PrintError($"������! ��������� \"{codingName}\" �� �������!");
        }

        //������ �����
        public static void FileError(string fileName)
        {
            PrintError($"������! ����� \"{fileName}\" �� ����������!");
        }

        //�������� ������������� ���������
        public static bool CheckCoding(string codingName, System.Text.EncodingInfo[] allCodings)
        {
            if (!allCodings.Any(code => code.Name == codingName))
            {
                CodingError(codingName);
                return false;
            }

            return true;
        }

        //�������� ������������� �����
        public static bool CheckFile(string fileName)
        {
            var fileExists = File.Exists(fileName);

            if (!fileExists)
                FileError(fileName);

            return fileExists;
        }

        public static void Main(string[] args)
        {
            var fileName = args[0];
            var codingIn = args[1];
            var codingOut = args[2];
            var allCodings = Encoding.GetEncodings();

            //�������� ������������ ������� ������
            if (CheckFile(fileName) &
                CheckCoding(codingIn, allCodings) &
                CheckCoding(codingOut, allCodings))
            {
                string textFile = "";

                //������ �����
                using (var reader = new StreamReader(fileName, Encoding.GetEncoding(codingIn)))
                {
                    textFile = reader.ReadToEnd();
                }

                //������ � ����
                using (var writer = new StreamWriter(fileName, false, Encoding.GetEncoding(codingOut)))
                {
                    writer.Write(textFile);
                }
            }
        }
    }
}
