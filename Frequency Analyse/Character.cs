using System;

namespace Frequency_Analyse
{
    public class Character : IComparable<Character>
    {

        public string Char;
        public int Frequency;

        public Character(string character, int frequency)
        {
            this.Char = character;
            this.Frequency = frequency;
        }


        public int CompareTo(Character other)
        {
            if (other == null)
            {
                return 1;
            }

            return Frequency.CompareTo(other.Frequency);
        }
    }
}