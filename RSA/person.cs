using System.Numerics;

namespace RSA
{
    public class Person
    {
        string Name;
        BigInteger p;
        BigInteger q;
        BigInteger n; // might be M
        BigInteger eulerFunc;
        BigInteger e;
        BigInteger d;
        public Key PublicKey;

        Key privateKey;

        public BigInteger Message;
        public BigInteger EncMessage;
        public BigInteger ForeignMessage;
        public BigInteger DecodedForeignMessage;



        public Person(string name)
        {
            Name = name;
            p = new BigInteger(Eratosphen.GetRandomPrimeNumber(3, 10000));
            q = new BigInteger(Eratosphen.GetRandomPrimeNumber(3, 10000));
            n = BigInteger.Multiply(p, q);
            eulerFunc = BigInteger.Multiply(BigInteger.Subtract(p, new BigInteger(1)), BigInteger.Subtract(q, new BigInteger(1)));
            e = createE();
            d = CreateD();
            PublicKey = new Key(e, n);
            privateKey = new Key(d, n);
            System.Console.WriteLine($"Created person:\n{this.ToString()}");
        }

        public override string ToString()
        {
            return $"Name: {Name}\nP = [{p}]\tQ = [{q}]\nPhi: {eulerFunc}\nExp: {e}\nD: {d}\nPublic key: {PublicKey}\nPrivate key: {privateKey}";
        }

        private BigInteger createE() // Might work with mistakes
        {
            for (int i = 3; i < eulerFunc; i++)
            {
                if (gcd(new BigInteger(i), eulerFunc) == 1 && Eratosphen.EratoArray[i])
                {
                    return new BigInteger(i);
                }
            }

            return new BigInteger(-7);
        }

        private BigInteger gcd(BigInteger a, BigInteger b)
        {
            BigInteger c;
            while (b != 0)
            {
                c = BigInteger.Remainder(a, b);
                // c = a % b;
                a = b;
                b = c;
            }
            return BigInteger.Abs(a);


        }

        private BigInteger CreateD()
        {
            for (var i = new BigInteger(2); i < eulerFunc; i++)
            {
                var mult = BigInteger.Multiply(i, e);
                if (BigInteger.Remainder(mult, eulerFunc) == 1)
                {
                    return i;
                }
            }
            return new BigInteger(-7);
        }

        private void CreateMessage(BigInteger mes, Person other)
        {
            Message = mes;
            EncMessage = BigInteger.ModPow(Message, other.PublicKey.first, other.PublicKey.second);
            // EncMessage = BigInteger.Remainder(BigInteger.Pow(Message, (int)PublicKey.first), PublicKey.second);
            System.Console.WriteLine($"{Name}, created message: {Message}\n\tEncoded version is: {EncMessage}");
        }

        public void SendMessage(BigInteger mes, Person other)
        {
            this.CreateMessage(mes, other);
            other.ForeignMessage = EncMessage;
            System.Console.WriteLine($"Message send to {other.Name}");
        }

        public void DecodeMessage()
        {
            DecodedForeignMessage = BigInteger.ModPow(ForeignMessage, privateKey.first, privateKey.second);
            System.Console.WriteLine($"Foreign message was decoded: {DecodedForeignMessage}");
        }


    }
}