using System.Collections.Generic;

namespace Diffie_Hellman_key_exchange
{
    public class PrimeNumberGenerator
    {

        List<bool> arr;

        public PrimeNumberGenerator(int n = 1000)
        {


            arr = fillArr(n);

            for (int i = 2; i * i < n; i++)
            {
                if (arr[i] == true)
                {
                    for (int j = i * i; j < n; j += i)
                    {
                        arr[j] = false;
                    }
                }
            }
        }




        private List<bool> fillArr(int num)
        {
            List<bool> arr = new List<bool>();
            for (int i = 0; i < num; i++)
            {
                arr.Add(true);
            }

            return arr;
        }

        public int GetPrimeNumber(int lower = 0, int upper = 0)
        {
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                if (arr[i] == true)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}