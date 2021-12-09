using System;
using System.Collections;
using System.Collections.Generic;

namespace BigramAnalyze
{
    public class RateInfo : IComparable<RateInfo>
    {
        public string ngram;
        public int rate;

        public RateInfo(string ngram, int rate)
        {
            this.ngram = ngram;
            this.rate = rate;
        }
        
        // Sorting from biggest to lowest
        public int CompareTo(RateInfo other)
        {
            if (this.rate < other.rate)
            {
                return 1;
            }

            if (this.rate > other.rate)
            {
                return -1;
            }

            return 0;
        }
    }
}