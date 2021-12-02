using System;
using System.Numerics;

namespace Diffie_Hellman_key_exchange
{
    public class Person
    {
        private string Name;
        private double g;
        private double p;
        private BigInteger PublicKey;
        private int PrivateKey;
        private BigInteger ForeignKey; // Bs | As
        private BigInteger Key;

        public string LastMessage;

        public Person(string Name, int g, int p)
        {
            var rand = new Random();
            this.Name = Name;
            this.g = g;
            this.p = p;

            this.PrivateKey =  (new PrimeNumberGenerator(rand.Next(1, 100_000))).GetPrimeNumber();  // a | b

            this.PublicKey = BigInteger.ModPow(new BigInteger(g), new BigInteger(PrivateKey), new BigInteger(p));//  Math.Pow(g, PrivateKey) % p); // As | Bs

            this.LastMessage = "";
            System.Console.WriteLine($"****Generated object*****\nName: {this.Name}\ng: {this.g}\np: {this.p}\nPrivateKey: {this.PrivateKey}\nPublicKey: {this.PublicKey}");
        }

        public void SendPublicKeyTo(Person other) // Возможно можно убрать ref
        {
            other.ForeignKey = this.PublicKey;
            System.Console.WriteLine($"{this.Name} send Public-key = {this.PublicKey} to {other.Name}");
        }

        public void GenerateFullKey()
        {
            this.Key = BigInteger.ModPow(ForeignKey, PrivateKey, new BigInteger(p));
            
            System.Console.WriteLine($"{this.Name} generated its Key = {this.Key}");
        }

        // public string DecodeMessage(string message) // Возможно можно убрать ref
        // {
        //     return "";
        // }

        // public void SendMessage(string message, ref Person other) // Возможно можно убрать ref
        // {

        // }

    }
}