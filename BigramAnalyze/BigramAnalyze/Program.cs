using System;
using System.IO;
using System.Text;


namespace BigramAnalyze
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToSourceText = Path.Combine(Directory.GetCurrentDirectory(), "Война и мир.txt");
            var pathToEncText = Path.Combine(Directory.GetCurrentDirectory(), "Война и мир.txt.enc.txt");
            var decoder = new Decoder(pathToSourceText, pathToEncText);
            
            Console.WriteLine(decoder.decodeText());
        }
    }
}