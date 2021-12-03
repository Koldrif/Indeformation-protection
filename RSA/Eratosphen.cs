namespace RSA
{
    public static class Eratosphen
    {
        public static List<bool> EratoArray = new List<bool>();

        public static void Initialize()
        {
            EratoArray = new List<bool>();
        }

        public static List<int> GetNumbers(int min_number, int max_number)
        {

            // Check for maximum length
            if (max_number > EratoArray.Count)
            {
                generateSequence(max_number + 1 - EratoArray.Count);
            }

            // refill Eratosphene
            fillEratosphen();

            // Create temp list for prime numbers
            var primeNumbers = new List<int>();


            // Get prime numbers in range
            for (int i = min_number; i <=max_number; i++)
            {
                if (EratoArray[i])
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers;
        }

        public static int GetRandomPrimeNumber(int min_number, int max_number)
        {
            var list = GetNumbers(min_number, max_number);
            var rand = new Random();
            return list[rand.Next(list.Count - 1)];
        }

        private static void fillEratosphen()
        {
            for (int i = 2; i * i < EratoArray.Count; i++)
            {
                if (EratoArray[i] == true)
                {
                    for (int j = i * i; j < EratoArray.Count; j += i)
                    {
                        EratoArray[j] = false;
                    }
                }
            }
        }



        static void generateSequence(int numbersToAdd)
        {
            for (int i = 0; i < numbersToAdd; i++)
            {
                EratoArray.Add(true);
            }
        }


    }
}