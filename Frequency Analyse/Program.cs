using System;
using System.IO;
using Frequency_Analyse;

namespace FA_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var analyzer = new Analyzer("Война и мир.txt.enc");
            var fileName = "Война и мир.txt.enc";
            var path = Path.Combine(Directory.GetCurrentDirectory());
            //var decodedText = analyzer.MonoAnalyzeText(fileName);
            //File.WriteAllText(Path.Combine(path, $"{fileName}.out.txt"), decodedText);
            var decodedText = analyzer.BigramsAnalyze();
            Console.WriteLine(decodedText);
        }
    }
}
