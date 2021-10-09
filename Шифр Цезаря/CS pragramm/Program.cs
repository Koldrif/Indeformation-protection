using System;
using Encoders.CaesarCipher;
using System.IO;

namespace CS_pragramm
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write("Enter a name of the file: ");
            string fileName = Console.ReadLine();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "src", fileName + ".txt");
            string text = File.ReadAllText(path);
            var enc = new V1(3, "", lang: "rus");
            string encodedText = enc.EncodeText(text);
            File.WriteAllText(path + ".enc.txt", encodedText);
        }
    }
}
