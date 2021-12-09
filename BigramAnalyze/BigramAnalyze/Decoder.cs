using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace BigramAnalyze
{
    public class Decoder
    {
        private Dictionary<string, int> encMonogram;
        private Dictionary<string, int> decMonogram;
        private Dictionary<string, int> encBigram;
        private Dictionary<string, int> decBigram;

        private string sourceText;
        private string encodedText;

        private List<RateInfo> encMonogramList;
        private List<RateInfo> decMonogramList;
        private List<RateInfo> encBigramList;
        private List<RateInfo> decBigramList;

        public Decoder(string pathToSourceText, string pathToEncodedText)
        {
            encMonogram = new Dictionary<string, int>();
            decMonogram = new Dictionary<string, int>();
            encBigram = new Dictionary<string, int>();
            decBigram = new Dictionary<string, int>();

            encMonogramList = new List<RateInfo>();
            decMonogramList = new List<RateInfo>();
            encBigramList = new List<RateInfo>();
            decBigramList = new List<RateInfo>();

            sourceText = File.ReadAllText(pathToSourceText).ToLower();
            encodedText = File.ReadAllText(pathToEncodedText).ToLower();

            #region Creating And Filing Monograms

            #region Fill decoded monogram

            decMonogram.Add("а", 40487008);
            decMonogram.Add("б", 8051767);
            decMonogram.Add("в", 22930719);
            decMonogram.Add("г", 8564640);
            decMonogram.Add("д", 15052118);
            decMonogram.Add("е", 42691213);
            decMonogram.Add("ж", 4746916);
            decMonogram.Add("з", 8329904);
            decMonogram.Add("и", 37153142);
            decMonogram.Add("й", 6106262);
            decMonogram.Add("к", 17653469);
            decMonogram.Add("л", 22230174);
            decMonogram.Add("м", 16203060);
            decMonogram.Add("н", 33838881);
            decMonogram.Add("о", 55414481);
            decMonogram.Add("п", 14201572);
            decMonogram.Add("р", 23916825);
            decMonogram.Add("с", 27627040);
            decMonogram.Add("т", 31620970);
            decMonogram.Add("у", 13245712);
            decMonogram.Add("ф", 1335747);
            decMonogram.Add("х", 4904176);
            decMonogram.Add("ц", 2438807);
            decMonogram.Add("ч", 7300193);
            decMonogram.Add("ш", 3678738);
            decMonogram.Add("щ", 1822476);
            decMonogram.Add("ъ", 185452);
            decMonogram.Add("ы", 9595941);
            decMonogram.Add("ь", 8784613);
            decMonogram.Add("э", 1610107);
            decMonogram.Add("ю", 3220715);
            decMonogram.Add("я", 10139085);

            #endregion


            fillAllEncodedMonograms();
            fromDictToSortedList(decMonogram, decMonogramList);

            #endregion

            #region Creating And Filing Bigrams

            fillAllDecBigrams();
            fillAllEncBigrams();

            #endregion

            cutListUpTo(encBigramList, 15);
            cutListUpTo(decBigramList, 15);


            Console.WriteLine();
        }


        private void fillAllEncodedMonograms()
        {
            foreach (var ch in encodedText)
            {
                if (" ,\r\n-—?!́:;.'()«»".Contains(ch))
                {
                    continue;
                }

                var character = ch.ToString();
                if (encMonogram.ContainsKey(character))
                {
                    encMonogram[character]++;
                }
                else
                {
                    encMonogram.Add(character, 1);
                }
            }

            fromDictToSortedList(encMonogram, encMonogramList);
        }

        private void fillAllDecBigrams()
        {
            for (int i = 0; i + 1 < sourceText.Length; i++)
            {
                if (" ,\r\n-—?!́:;.'()«»".Contains(sourceText[i]) ||
                    " ,\r\n-—?!́:;.'()«»".Contains(sourceText[i + 1]))
                {
                    continue;
                }

                var bigrmaOchka = $"{sourceText[i]}{sourceText[i + 1]}";
                if (decBigram.ContainsKey(bigrmaOchka))
                {
                    decBigram[bigrmaOchka]++;
                }
                else
                {
                    decBigram.Add(bigrmaOchka, 1);
                }
            }

            fromDictToSortedList(decBigram, decBigramList);
        }

        private void fillAllEncBigrams()
        {
            for (int i = 0; i + 1 < encodedText.Length; i++)
            {
                if (" ,\r\n-—?!́:;.'()«»".Contains(encodedText[i]) ||
                    " ,\r\n-—?!́:;.'()«»".Contains(encodedText[i + 1]))
                {
                    continue;
                }

                var bigrmaOchka = $"{encodedText[i]}{encodedText[i + 1]}";
                if (encBigram.ContainsKey(bigrmaOchka))
                {
                    encBigram[bigrmaOchka]++;
                }
                else
                {
                    encBigram.Add(bigrmaOchka, 1);
                }
            }

            fromDictToSortedList(encBigram, encBigramList);
        }

        public string decodeText()
        {
            var stringBuider = new StringBuilder();
            for (int i = 0; i < encodedText.Length; i++)
            {
                if (" ,\r\n-—?!́:;.'()«»".Contains(encodedText[i]))
                {
                    stringBuider.Append(encodedText[i]);
                }
                else
                {
                    if (!" ,\r\n-—?!́:;.'()«»".Contains(encodedText[i + 1]))
                    {
                        int bigramIndex = IndexOfEncBigram($"{encodedText[i]}{encodedText[i + 1]}");
                        if (bigramIndex >= 0)
                        {
                            stringBuider.Append(decBigramList[bigramIndex].ngram);
                            i++;
                            continue;
                        }
                        else
                        {
                            int monogramIndex = IndexOfEncMonogram(encodedText[i].ToString());
                            if (monogramIndex >= 0)
                            {
                                stringBuider.Append(decMonogramList[monogramIndex].ngram);
                            }
                        }
                    }
                    else
                    {
                        int monogramIndex = IndexOfEncMonogram(encodedText[i].ToString());
                        if (monogramIndex >= 0)
                        {
                            stringBuider.Append(decMonogramList[monogramIndex].ngram);
                        }
                    }
                }
            }

            return stringBuider.ToString();
        }

        private int IndexOfEncMonogram(string monogram)
        {
            for (int i = 0; i < encMonogramList.Count; i++)
            {
                if (monogram == encMonogramList[i].ngram)
                {
                    return i;
                }
            }

            return -1;
        }

        private int IndexOfEncBigram(string bigram)
        {
            for (int i = 0; i < encBigramList.Count; i++)
            {
                if (bigram == encBigramList[i].ngram)
                {
                    return i;
                }
            }

            return -1;
        }


        #region Utility

        private void cutListUpTo(List<RateInfo> list, int count)
        {
            list.RemoveRange(count, list.Count - count);
        }

        private void fromDictToSortedList(Dictionary<string, int> dic, List<RateInfo> list)
        {
            foreach (var element in dic)
            {
                list.Add(new RateInfo(element.Key, element.Value));
            }

            list.Sort();
        }

        #endregion
    }
}