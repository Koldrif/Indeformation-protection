// See https://aka.ms/new-console-template for more information
using RSA;

using System.Numerics;

Eratosphen.Initialize();
var Alice = new Person("Alice");
var Bob = new Person("Bob");

Alice.SendMessage(322, Bob);

Bob.DecodeMessage();

System.Console.WriteLine("Check result\n Alice message is: " + Alice.Message);
System.Console.WriteLine("Check result\n Bob decoded message is: " + Bob.DecodedForeignMessage);


