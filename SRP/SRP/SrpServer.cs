using System.Numerics;

namespace SRP;

public class SrpServer
{
    private readonly Func<byte[], byte[]> H;
    private readonly int g;
    private readonly BigInteger N;

    private BigInteger b;
    private BigInteger B;

    public SrpServer(Func<byte[], byte[]> H, int g, BigInteger N)
    {
        Helpers.ServerColorLog();
        this.H = H;
        this.g = g;
        this.N = N;
        Console.WriteLine($"Created server with:\n\tg: {g}\n\tN: {N}");
        Helpers.DefaultColorLog();
    }

    public BigInteger GenerateBValues(BigInteger v)
    {
        Helpers.ServerColorLog();
        // b = random()

        b = TestVectors.b;

        Console.WriteLine($"Generating B value...\nB = kv + g^b");
        var k = Helpers.ComputeK(g, N, H);

        // g^b % N
        var left = (k * v) % N;
        Console.WriteLine($"Computing k*v value...\n k*v = {left}");

        // B = kv + g^b
        var right = BigInteger.ModPow(g, b, N);
        
        Console.WriteLine($"Computing g^b value...\n k*v = {right}");

        B = (left + right) % N;

        Console.WriteLine($"{B} = {left} + {right}");
        
        Helpers.DefaultColorLog();
        return B;
    }

    public BigInteger ComputeSessionKey(BigInteger v, BigInteger A)
    {
        Helpers.ServerColorLog();

        Console.WriteLine("Generating Session key = (Av^u) ^ b...");
        var u = Helpers.CumputeU(H, A, B);

        // (Av^u)
        var left = A * BigInteger.ModPow(v, u, N);

        // S = (Av^u) ^ b
        var s = BigInteger.ModPow(left, b, N);

        Console.WriteLine($"Session key generated.\n S = {s} = {left}^{b}");
        Helpers.DefaultColorLog();
        return s;
    }
    
    public bool ValidateClientProof(BigInteger M1, BigInteger A, BigInteger S)
    {
        return M1 == Helpers.ComputeClientProof(N, H, A, B, S);
    }

    public BigInteger GenerateServerProof(BigInteger A, BigInteger M1, BigInteger S)
    {
        return Helpers.ComputeServerProof(N, H, A, M1, S);
    }
}