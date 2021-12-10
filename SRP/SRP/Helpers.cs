using System.Globalization;
using System.Numerics;
using System.Collections;
using System.Text;


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
        Console.WriteLine("Computing k = H(N, g)...");
        var nBytes = N.ToByteArray(true, true);
        

        var gBytes = PadBytes(BitConverter.GetBytes(g).Reverse().ToArray(), nBytes.Length);
        Console.WriteLine($"N in bytes:\n{Helpers.printByteArray(nBytes)}");
        Console.WriteLine($"g in bytes:\n{Helpers.printByteArray(gBytes)}");

        var kBytes = H(nBytes.Concat(gBytes).ToArray());

        Console.WriteLine($"k in bytes:\n{Helpers.printByteArray(kBytes)}");

        var k = new BigInteger(kBytes, isBigEndian: true);

        Console.WriteLine("Computed k = H(N, g)\nk = {k}");

        return k;
    }

    private static byte[] PadBytes(byte[] bytes, int Length)
    {
        var paddedBytes = new byte[Length];
        Array.Copy(bytes, 0, paddedBytes,Length-bytes.Length, bytes.Length);

        return paddedBytes;
    }

    public static BigInteger CumputeU(Func<byte[], byte[]> H, BigInteger A, BigInteger B)
    {
        Console.WriteLine("Computing u...");
        var u = H(A.ToByteArray(true, true)
                .Concat(B.ToByteArray(true, true))
                .ToArray())
                .ToSrpBigInt();
        Console.WriteLine("Computed u = {u}");
        return u;
    }
    
    public static BigInteger ComputeClientProof(
        BigInteger N,
        Func<byte[], byte[]> H,
        BigInteger A,
        BigInteger B,
        BigInteger S)
    {
        Console.WriteLine("Computing CLIENT proof...\nM1 = H( A | B | S )");
        var padLength = N.ToByteArray(true, true).Length;

        // M1 = H( A | B | S )
        var m1 = H((PadBytes(A.ToByteArray(true, true), padLength))
                .Concat(PadBytes(B.ToByteArray(true, true), padLength))
                .Concat(PadBytes(S.ToByteArray(true, true), padLength))
                .ToArray())
            .ToSrpBigInt();

        Console.WriteLine($"Proof computed. Proof is:\n{m1}");

        return m1;
    }

    public static BigInteger ComputeServerProof(BigInteger N, Func<byte[], byte[]> H, BigInteger A, BigInteger M1, BigInteger S)
    {
        var padLength = N.ToByteArray(true, true).Length;

        // M2 = H( A | M1 | S )
        Console.WriteLine("Computing SERVER proof...\nM2 = H( A | M1 | S )");
        var m2 = H((PadBytes(A.ToByteArray(true, true), padLength))
                .Concat(PadBytes(M1.ToByteArray(true, true), padLength))
                .Concat(PadBytes(S.ToByteArray(true, true), padLength))
                .ToArray())
            .ToSrpBigInt();
        
        Console.WriteLine($"Proof computed. Proof is:\n{m2}");

        return m2;
    }

    public static void ServerColorLog()
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }

    public static void ClientColorLog()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    public static void DefaultColorLog()
    {
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static string printByteArray(IEnumerable arr)
    {
        var strBuild = new StringBuilder();
        foreach (var VARIABLE in arr)
        {
            strBuild.Append(VARIABLE);
        }

        return strBuild.ToString();
    }
}