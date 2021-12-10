using System.Numerics;
using System.Text;

namespace SRP;

public class SrpClient
{
    private readonly Func<byte[], byte[]> H;
    private readonly int g;
    private readonly BigInteger N;
    private BigInteger A;
    private BigInteger a;

    public SrpClient(Func<byte[], byte[]> H, int g, BigInteger N)
    {
        this.H = H;
        this.g = g;
        this.N = N;
        Helpers.ClientColorLog();
        Console.WriteLine($"Created client with:\n\tg: {g}\n\tN: {N}");
        Helpers.DefaultColorLog();
    }

    public BigInteger GenerateVerifier(string userName, string password, byte[] salt)
    {
        // x = H(s | H(I | ":" | P)) | I = userName, P = password, s = salt
        
        Helpers.ClientColorLog();

        Console.WriteLine($"Generating x...");

        var x = generatePrivateKey(userName, password, salt);

        Console.WriteLine($"Generated x.\nx = H(s | H(I | ':' | P)) | I = userName, P = password, s = salt");
        Console.WriteLine($"{x} = H({salt} | H({userName} | ':' | {password})");
        
        // v = g^x
        var v = BigInteger.ModPow(g, x, N);

        Console.WriteLine($"Generated v = g^x\n{v} = {g}^{x}");
        
        Helpers.DefaultColorLog();

        return v;

    }

    private BigInteger generatePrivateKey(string userName, string password, byte[] salt)
    {
        var x = H(
            salt.
            Concat(
            H(
            Encoding
            .UTF8
            .GetBytes($"{userName}:{password}")))
            .ToArray());

        return x.ToSrpBigInt();
    }

    public BigInteger GenerateAValues()
    {
        Helpers.ClientColorLog();

        Console.WriteLine($"Creating A value...");
        
        // a = random()

        a = TestVectors.a;

        Console.WriteLine($"a = {a}");
        
        // A = g^a
        
        
        A = BigInteger.ModPow(g, a, N);

        Console.WriteLine($"A = g^a\n{A} = {g} ^ {a}");
        
        Helpers.DefaultColorLog();

        return A;
    }

    public BigInteger ComputeSessionKey(string userName, string password, byte[] salt, BigInteger B)
    {
        Helpers.ClientColorLog();

        Console.WriteLine($"Generating CLIENT session key...");
        
        var u = Helpers.CumputeU(H, A, B);
        
        

        var x = generatePrivateKey(userName, password, salt);

        var k = Helpers.ComputeK(g, N, H);

        Console.WriteLine($"Created u = {u}.\nCreated x = {x}\nCreated k = {k}\nCreating ");
        
        // (a + ux)
        var exp = a + u * x;
        
        // (B - kg ^ x)
        var val = mod(B - (BigInteger.ModPow(g, x, N) * k % N), N);

        // S = (B - kg ^ x) ^ (a + ux)
        var s = BigInteger.ModPow(val, exp, N);
        
        Console.WriteLine($"Computing S = (B - kg ^ x) ^ (a + ux)...\n{s} = {val}^{exp}");
        
        Helpers.DefaultColorLog();
        return s;


    }

    private BigInteger mod(BigInteger x, BigInteger y)
    {
        BigInteger res = x % y;
        return res < 0 ? res + y : res;
    }
    
    public BigInteger GenerateClientProof(BigInteger B, BigInteger S)
    {
        return Helpers.ComputeClientProof(N, H, A, B, S);
    }

    public bool ValidateServerProof(BigInteger M2, BigInteger M1, BigInteger S)
    {
        return M2 == Helpers.ComputeServerProof(N, H, A, M1, S);
    }
}