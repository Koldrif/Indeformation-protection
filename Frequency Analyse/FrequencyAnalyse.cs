using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frequency_Analyse;

namespace FrequencyAnalyse
{
    public class Analyzer
    {
        private string Text;

        private List<Character> FrequencyTable;
        public List<Character> FrequencyTableOfCurrentText;
        private List<Character> biGrammList;

        public Analyzer()
        {
            FrequencyTable = new List<Character>();
            
            
            biGrammList = new List<Character>();
            // Initialize Bi-Gram List
            for (int i = 1072; i < 1104; i++)
            {
                for (int j = 1072; j < 1104; j++)
                {
                    biGrammList.Add(new Character(
                        ((char)i).ToString() + ((char)j).ToString(), 0
                        ));
                }
            }

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


        }

        public string AnalyzeText(string fileName)
        {
            this.Text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"src/{fileName}.txt"));
            
            
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

            return decodeText();
        }

        private string decodeText()
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
    }
}