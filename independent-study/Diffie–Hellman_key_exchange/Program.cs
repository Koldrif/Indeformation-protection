using System;
using Diffie_Hellman_key_exchange;
class Program
{

    static void Main(string[] args)
    {
        var random = new Random();

        int g = random.Next(1, 1_000_000);
        int p = random.Next(1, 1_000_000);

        var Alice = new Person("Alice", g, p);
        var Bob = new Person("Bob", g, p);

        Alice.SendPublicKeyTo(Bob);
        Bob.SendPublicKeyTo(Alice);

        Alice.GenerateFullKey();
        Bob.GenerateFullKey();

    }
}
