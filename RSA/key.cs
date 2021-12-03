using System.Numerics;

namespace RSA
{
    public class Key
    {
        public BigInteger first;
        public BigInteger second;

        public Key(BigInteger f, BigInteger s)
        {
            first = f;
            second = s;
        }

        override public string ToString() 
        {
            return $"{'{'}{first}, {second}{'}'}";
        }
    }
}