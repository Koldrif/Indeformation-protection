using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Frequency_Analyse
{
    public class Analyzer
    {
        private string Text;
        private string OriginalText;

        private List<Character> FrequencyTable;
        public List<Character> FrequencyTableOfCurrentText;
        private List<Character> BiGramList;
        private List<Character> BiGramListOfCurrentText;

        public Analyzer(string fileName)
        {
            FrequencyTable = new List<Character>();
            
            this.Text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "src", $"{fileName}.txt"));
            
            
            BiGramList = new List<Character>();
            BiGramListOfCurrentText = new List<Character>();
            
            // Initialize Bi-Gram List
            for (int i = 1072; i < 1104; i++)
            {
                for (int j = 1072; j < 1104; j++)
                {
                    BiGramList.Add(new Character(
                        ((char)i).ToString() + ((char)j).ToString(), 0
                        ));
                }
            }

            BiGramListOfCurrentText.AddRange(BiGramList);
            
            #region MonoGrammInitialize

            this.FrequencyTable.Add(new Character("а", 40487008));
            this.FrequencyTable.Add(new Character("б", 8051767));	
            this.FrequencyTable.Add(new Character("в", 22930719));
            this.FrequencyTable.Add(new Character("г", 8564640));	
            this.FrequencyTable.Add(new Character("д", 15052118));
            this.FrequencyTable.Add(new Character("е", 42691213));
            this.FrequencyTable.Add(new Character("ё", 184928));
            this.FrequencyTable.Add(new Character("ж", 4746916));	
            this.FrequencyTable.Add(new Character("з", 8329904));	
            this.FrequencyTable.Add(new Character("и", 37153142));
            this.FrequencyTable.Add(new Character("й", 6106262));	
            this.FrequencyTable.Add(new Character("к", 17653469));
            this.FrequencyTable.Add(new Character("л", 22230174));
            this.FrequencyTable.Add(new Character("м", 16203060));
            this.FrequencyTable.Add(new Character("н", 33838881));
            this.FrequencyTable.Add(new Character("о", 55414481));
            this.FrequencyTable.Add(new Character("п", 14201572));
            this.FrequencyTable.Add(new Character("р", 23916825));
            this.FrequencyTable.Add(new Character("с", 27627040));
            this.FrequencyTable.Add(new Character("т", 31620970));
            this.FrequencyTable.Add(new Character("у", 13245712));
            this.FrequencyTable.Add(new Character("ф", 1335747));	
            this.FrequencyTable.Add(new Character("х", 4904176));	
            this.FrequencyTable.Add(new Character("ц", 2438807));	
            this.FrequencyTable.Add(new Character("ч", 7300193));	
            this.FrequencyTable.Add(new Character("ш", 3678738));	
            this.FrequencyTable.Add(new Character("щ", 1822476));	
            this.FrequencyTable.Add(new Character("ъ", 185452)); 
            this.FrequencyTable.Add(new Character("ы", 9595941));	
            this.FrequencyTable.Add(new Character("ь", 8784613));	
            this.FrequencyTable.Add(new Character("э", 1610107));	
            this.FrequencyTable.Add(new Character("ю", 3220715));	
            this.FrequencyTable.Add(new Character("я", 10139085));
            

            #endregion
            
            
            this.FrequencyTableOfCurrentText = new List<Character>();
            
            searchBiGramInOriginalText("Война и мир.txt");
            FillBiGramsOfCurrentText();
        
        }

        public string MonoAnalyzeText(string fileName)
        {
            
            //this.Text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"src/{fileName}.txt"));
            
            // Code for mono-grams
            foreach (var character in Text)
            {
                var currentChar = character.ToString();
                if (" ,\r\n-—?!́:;.'()«»".Contains(currentChar))
                {
                    continue;
                }

                if (ContainsChar(currentChar))
                {
                     findChar(currentChar).Frequency++;
                }
                else
                {
                    FrequencyTableOfCurrentText.Add(new Character(currentChar, 1));
                }
            }
            
            FrequencyTable.Sort();
            FrequencyTableOfCurrentText.Sort();

            #region PrintRegion
            /*
             * Вывод всех пар Символ-частота
             */
            for (int i = 0; i < FrequencyTableOfCurrentText.Count; i++)
            {
                Console.WriteLine($"{i + 1}.\t'{FrequencyTable[i].Char}' [{FrequencyTable[i].Frequency}]\t '{FrequencyTableOfCurrentText[i].Char}' [{FrequencyTableOfCurrentText[i].Frequency}]");
            }
            #endregion

            var decodedText = monoDecodeText();
            
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}_MonoAnalyze.txt"), decodedText);
            
            return decodedText;
        }

        private string monoDecodeText()
        {
            StringBuilder decodedText = new StringBuilder();
            foreach (var VARIABLE in Text)
            {
                var currentChar = VARIABLE.ToString();
                if (" ,\r\n-—?!́:;.'()«»".Contains(currentChar))
                {
                    decodedText.Append(currentChar);
                    continue;
                }
                
                decodedText.Append(
                    FrequencyTable[
                        FrequencyTableOfCurrentText.IndexOf(
                            findChar(
                                currentChar
                                )
                            )
                    ].Char);
            }

            return decodedText.ToString();
        }

        private bool ContainsChar(string character)
        {
            foreach (var VARIABLE in FrequencyTableOfCurrentText)
            {
                if (VARIABLE.Char == character)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool ContainsBiGram(string character)
        {
            foreach (var VARIABLE in BiGramListOfCurrentText)
            {
                if (VARIABLE.Char == character)
                {
                    return true;
                }
            }

            return false;
        }

        private Character findChar(string character)
        {
            foreach (var VARIABLE in FrequencyTableOfCurrentText)
            {
                if (character == VARIABLE.Char)
                {
                    return VARIABLE;
                }
            }

            return new Character("", -1);
        }

        private Character addBigramFrequency(string bigram)
        {
            foreach (var VARIABLE in BiGramList)
            {
                if (bigram == VARIABLE.Char)
                {
                    VARIABLE.Frequency++;
                    return VARIABLE;
                }
            }

            return new Character("", -1);
        }
        
        private Character addBigramFrequencyToCurrentText(string bigram)
        {
            foreach (var VARIABLE in BiGramListOfCurrentText)
            {
                if (bigram == VARIABLE.Char)
                {
                    VARIABLE.Frequency++;
                    return VARIABLE;
                }
            }

            return null;
        }
        
        private Character findBigram(string bigram)
        {
            foreach (var VARIABLE in BiGramListOfCurrentText)
            {
                if (bigram == VARIABLE.Char)
                {
                    return VARIABLE;
                }
            }

            return null;
        }

        private void searchBiGramInOriginalText(string fileName)
        {
            this.OriginalText = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"src", fileName));
            for (int i = 0; i+1 < OriginalText.Length; i++)
            {
                addBigramFrequency(OriginalText[i].ToString() + OriginalText[i + 1]);
            }
            
            BiGramList.Sort();
            
            //Save bigrams in a file

            var cacheDirectory = Path.Combine(Directory.GetCurrentDirectory(), "cache");

            if(!Directory.Exists(cacheDirectory)) Directory.CreateDirectory(cacheDirectory) ;
            
            using var streamWrite =
                new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "cache", "Bigrams.txt"));
            for (int i = 0; i < BiGramList.Count; i++)
            {
                streamWrite.WriteLine($"{i + 1}.\t'{BiGramList[i].Char}'\t{BiGramList[i].Frequency}");
            }
            
            
        }

        private void FillBiGramsOfCurrentText()
        {
            for (int i = 0; i + 1 < Text.Length; i++)
            {
                var temp = Text[i].ToString() + Text[i + 1];
                
                    addBigramFrequencyToCurrentText(temp);
            }
            
            BiGramListOfCurrentText.Sort();
            
            using var streamWrite =
                new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "cache", "BigramsOfEncodedText.txt"));
            for (int i = 0; i < BiGramListOfCurrentText.Count; i++)
            {
                streamWrite.WriteLine($"{i + 1}.\t'{BiGramListOfCurrentText[i].Char}'\t{BiGramListOfCurrentText[i].Frequency}");
            }
        }

        public string BigramsAnalyze()
        {
            StringBuilder decodedText = new StringBuilder();
            
            for (int i = 0; i+1 < Text.Length; i++)
            {
                string temp = Text[i].ToString() + Text[i + 1];
                
                if (" ,\r\n-—?!́:;.'()«»".Contains(temp[0]))
                {
                    decodedText.Append(temp[0]);
                    continue;
                }
                else if (" ,\r\n-—?!́:;.'()«»".Contains(temp[1]))
                {
                    decodedText.Append(temp[1]);
                    continue;
                }

                decodedText.Append(BiGramList[
                    BiGramListOfCurrentText.IndexOf(
                        findBigram(temp)
                    )
                ].Char);

                

            }

            var finalText = decodedText.ToString();
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), $"_BigramAnalyze.txt"), finalText);
            return finalText;
        }
    }
}