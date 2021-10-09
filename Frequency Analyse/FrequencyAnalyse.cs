using System.IO;
using System.Collections.Generic;

namespace FrequencyAnalyse
{
    public class Analyzer
    {
        public string Text;

        public Dictionary<string, int> FrequencyTable = new Dictionary<string, int>();

        public Analyzer(string fileName)
        {
            Text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName));
        }



    }
}