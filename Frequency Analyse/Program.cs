using System;
using System.IO;
using FrequencyAnalyse;

namespace FA_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new Analyzer();
            var fileName = "Война и мир.txt.enc";
            var path = Path.Combine(Directory.GetCurrentDirectory());
            var decodedText = analyzer.AnalyzeText(fileName);
            File.WriteAllText(Path.Combine(path, $"{fileName}.out.txt"), decodedText);
        }
    }
}
