using System.Globalization;
using System.Numerics;

namespace SRP;

public static class Helpers
{
    public static byte[] ToBytes(this string hex)
    {
        var hexAsBytes = new byte[hex.Length / 2];

        for (var i = 0; i < hex.Length; i += 2)
        {
            hexAsBytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }

        return hexAsBytes;
    }

    public static BigInteger ToSrpBigInt(this byte[] bytes)
    {
        return new BigInteger(bytes, true, true);
    }

    public static BigInteger ToSrpBigInt(this string hex)
    {
        return BigInteger.Parse($"0{hex}", NumberStyles.HexNumber);
    }

    public static BigInteger ComputeK(int g, BigInteger N, Func<byte[], byte[]> H)
    {
        // k = H(N, g)

        var nBytes = N.ToByteArray(true, true);

        var gBytes = PadBytes(BitConverter.GetBytes(g).Reverse().ToArray(), nBytes.Length);

        var k = H(nBytes.Concat(gBytes).ToArray());

        return new BigInteger(k, isBigEndian: true);
    }

    private static byte[] PadBytes(byte[] bytes, int Length)
    {
        var paddedBytes = new byte[Length];
        Array.Copy(bytes, 0, paddedBytes,Length-bytes.Length, bytes.Length);

        return paddedBytes;
    }

    public static BigInteger CumputeU(Func<byte[], byte[]> H, BigInteger A, BigInteger B)
    {
        return H(A.ToByteArray(true, true)
                .Concat(B.ToByteArray(true, true))
                .ToArray())
                .ToSrpBigInt();
    }
    
    public static BigInteger ComputeClientProof(
        BigInteger N,
        Func<byte[], byte[]> H,
        BigInteger A,
        BigInteger B,
        BigInteger S)
    {
        var padLength = N.ToByteArray(true, true).Length;

        // M1 = H( A | B | S )
        return H((PadBytes(A.ToByteArray(true, true), padLength))
                .Concat(PadBytes(B.ToByteArray(true, true), padLength))
                .Concat(PadBytes(S.ToByteArray(true, true), padLength))
                .ToArray())
            .ToSrpBigInt();
    }

    public static BigInteger ComputeServerProof(BigInteger N, Func<byte[], byte[]> H, BigInteger A, BigInteger M1, BigInteger S)
    {
        var padLength = N.ToByteArray(true, true).Length;

        // M2 = H( A | M1 | S )
        return H((PadBytes(A.ToByteArray(true, true), padLength))
                .Concat(PadBytes(M1.ToByteArray(true, true), padLength))
                .Concat(PadBytes(S.ToByteArray(true, true), padLength))
                .ToArray())
            .ToSrpBigInt();
    }
}