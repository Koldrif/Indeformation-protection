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
    }

    public BigInteger GenerateVerifier(string userName, string password, byte[] salt)
    {
        // x = H(s | H(I | ":" | P)) | I = userName, P = password, s = salt

        var x = generatePrivateKey(userName, password, salt);

        var v = BigInteger.ModPow(g, x, N);

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
        // a = random()

        a = TestVectors.a;
        
        // A = g^a

        A = BigInteger.ModPow(g, a, N);

        return A;
    }

    public BigInteger ComputeSessionKey(string userName, string password, byte[] salt, BigInteger B)
    {
        var u = Helpers.CumputeU(H, A, B);

        var x = generatePrivateKey(userName, password, salt);

        var k = Helpers.ComputeK(g, N, H);
        
        // (a + ux)
        var exp = a + u * x;
        
        // (B - kg ^ x)
        var val = mod(B - (BigInteger.ModPow(g, x, N) * k % N), N);
        
        // S = (B - kg ^ x) ^ (a + ux)
        return BigInteger.ModPow(val, exp, N);


    }

    private BigInteger mod(BigInteger x, BigInteger y)
    {
        BigInteger res = x % y;
        return res < 0 ? res + y : res;
    }
}