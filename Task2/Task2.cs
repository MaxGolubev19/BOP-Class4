using System.Text;

namespace Task2
{
    public class Task2
    {
        //Вывод ошибки в консоль
        public static void PrintError(string textError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textError);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Ошибка кодировки
        public static void CodingError(string codingName)
        {
            PrintError($"Ошибка! Кодировка \"{codingName}\" не найдена!");
        }

        //Ошибка файла
        public static void FileError(string fileName)
        {
            PrintError($"Ошибка! Файла \"{fileName}\" не существует!");
        }

        //Проверка существования кодировки
        public static bool CheckCoding(string codingName, System.Text.EncodingInfo[] allCodings)
        {
            if (!allCodings.Any(code => code.Name == codingName))
            {
                CodingError(codingName);
                return false;
            }

            return true;
        }

        //Проверка существования файла
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

            //Проверка правильности входных данных
            if (CheckFile(fileName) &
                CheckCoding(codingIn, allCodings) &
                CheckCoding(codingOut, allCodings))
            {
                string textFile = "";

                //Чтение файла
                using (var reader = new StreamReader(fileName, Encoding.GetEncoding(codingIn)))
                {
                    textFile = reader.ReadToEnd();
                }

                //Запись в файл
                using (var writer = new StreamWriter(fileName, false, Encoding.GetEncoding(codingOut)))
                {
                    writer.Write(textFile);
                }
            }
        }
    }
}
