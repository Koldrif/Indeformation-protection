// See https://aka.ms/new-console-template for more information
using SRP;
using System.Numerics;

var client = new SrpClient(TestVectors.H, TestVectors.g, TestVectors.N);
var server = new SrpServer(TestVectors.H, TestVectors.g, TestVectors.N);

// generate password verifier to store 
BigInteger v = client.GenerateVerifier(TestVectors.I, TestVectors.P, TestVectors.s);

var A = client.GenerateAValues();

var B = server.GenerateBValues(v);

var clientS = client.ComputeSessionKey(TestVectors.I, TestVectors.P, TestVectors.s, B);
var serverS = server.ComputeSessionKey(v, A);

var M1 = client.GenerateClientProof(B, clientS);
if (!server.ValidateClientProof(M1, A, serverS)) throw new Exception();

var M2 = server.GenerateServerProof(A, M1, serverS);
if (!client.ValidateServerProof(M2, M1, clientS)) throw new Exception();

Console.WriteLine("SRP success!");