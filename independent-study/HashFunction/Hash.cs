using System.Text;
using System.Collections.Generic;

namespace Hash
{
    public static class GenerateFunction
    {
        public static string GenerateHash(string input)
        {
            Encoding encoding = Encoding.BigEndianUnicode;
            var byteArray = encoding.GetBytes(input);

            if (byteArray.Length < 256)
            {
                int lastIndex = byteArray.Length;
                Array.Resize<byte>(ref byteArray, 256);
                for (int i = lastIndex - 1; i < 256; i++)
                {
                    byteArray[i] = (byte)((int)byteArray[i % lastIndex] + ((int)byteArray[i] % i) % byte.MaxValue);
                }
            }


            /*
                Write your hash function here
            */
            // -----------

            byteArray = rotateAlphbet(byteArray);
            for (int i = 0; i + 1 < byteArray.Length; i += 2)
            {
                byteArray[i] = (byte)~((byte.MaxValue - (int)byteArray[i] - (i * (int)byteArray[i + 1]) | ((byte.MaxValue - (int)byteArray[i + 1] - (i * (int)byteArray[i])) % (int)byte.MaxValue)) % (int)byte.MaxValue);
                byteArray[i + 1] = (byte)~((byte.MaxValue - (int)byteArray[i + 1] - (i * (int)byteArray[i]) | ((byte.MaxValue - (int)byteArray[i] - (i * (int)byteArray[i + 1])))) % (int)byte.MaxValue);
                swap(ref byteArray[i], ref byteArray[i + 1]);
            }
            byteArray = rotateAlphbet(byteArray);


            // -----------
            /*
            foreach (var item in byteArray)
            {
                System.Console.Write($"[{item}] ");
            }
            System.Console.WriteLine();
            */
            return BitConverter.ToString(byteArray);
            return encoding.GetString(byteArray);
        }
        static byte[] rotateAlphbet(byte[] array)
        {

            for (int j = 0; j < ((int)array[0] + (int)array[array.Length - 1]); j++)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    int a = ((int)array[i] | i | (array.Length - i)) % array.Length, b = (i | array.Length) % array.Length;
                    if (a == b)
                    {
                        continue;
                    }
                    swap(ref array[(a | (int)array[i]) % array.Length], ref array[(b | (int)array[array.Length - 1 - i]) % array.Length]);
                }
            }
            return array;

        }

        static void swap<T>(ref T el1, ref T el2)
        {
            T temp = el1;
            el1 = el2;
            el2 = temp;
        }
    }

}