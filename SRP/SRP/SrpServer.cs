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
        this.H = H;
        this.g = g;
        this.N = N;
    }

    public BigInteger GenerateBValues(BigInteger v)
    {
        // b = random()

        b = TestVectors.b;

        var k = Helpers.ComputeK(g, N, H);

        // g^b % N
        var left = (k * v) % N;

        // B = kv + g^b
        var right = BigInteger.ModPow(g, b, N);

        return B;
    }
    
    public BigInteger ComputeSessionKey(BigInteger v, BigInteger A)
    {
        var u = Helpers.CumputeU(H, A, B);

        // (Av^u)
        var left = A * BigInteger.ModPow(v, u, N);

        // S = (Av^u) ^ b
        return BigInteger.ModPow(left, b, N);
    }
}