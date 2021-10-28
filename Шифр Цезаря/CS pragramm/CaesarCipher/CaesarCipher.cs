using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encoders.CaesarCipher
{
    class V1
    {
        private readonly string language;
        private readonly List<char> alphabet;
        private List<char> encodedAlphabet;
        private string text;
        private readonly int offset;
        private readonly string key;

        public V1(int offset, string key, string lang = "eng")
        {
            this.language = lang;
            switch (lang.ToLower())
            {
                case "eng":
                    alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                    encodedAlphabet = new List<char>();
                    encodedAlphabet.AddRange(alphabet);
                    break;
                case "rus":
                    alphabet = new List<char> { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
                    encodedAlphabet = new List<char>();
                    encodedAlphabet.AddRange(alphabet);
                    break;
                default:
                    break;
            }
            this.offset = offset;
            this.key = key;
        }

        public string EncodeText(string text)
        {
            this.text = text.ToLower();
            addKeyToAlphbet();
            rotateAlphabet();
            return encode();
        }

        private void rotateAlphabet()
        {
            List<char> tempList = null;

            tempList = encodedAlphabet.GetRange(encodedAlphabet.Count() - this.offset, offset); // v
            encodedAlphabet.RemoveRange(encodedAlphabet.Count() - tempList.Count(), tempList.Count());
            encodedAlphabet.InsertRange(0, tempList);

        }

        private void addKeyToAlphbet()
        {
            List<char> tempList = this.key.ToList();

            encodedAlphabet.RemoveAll((charachter) => key.Contains(charachter));
            encodedAlphabet.InsertRange(0, tempList);
        }

        private string encode()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < this.text.Length; i++)
            {
                if (!alphabet.Contains(text[i]))
                {
                    //if (text[i] == ' ')
                    //{
                    stringBuilder.Append(text[i]);
                    //}
                    continue;
                }
                stringBuilder.Append(encodedAlphabet[alphabet.IndexOf(text[i])]);
            }

            return stringBuilder.ToString();
        }
    }
}
