using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(DecodeSingleCharacterXor(input, out int xor));
            Console.WriteLine(xor);

            while (true)
            {
                Console.ReadLine();
                Console.ReadLine();
            }
        }

        static string HexToBase64(string hex)
        {
            return Convert.ToBase64String(HexToByteArray(hex));
        }

        static byte[] HexToByteArray(string hex)
        {
            byte[] output = new byte[hex.Length / 2];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return output;
        }

        static string ByteArrayToHex(byte[] input)
        {
            StringBuilder hex = new StringBuilder(input.Length * 2);
            foreach (byte b in input) hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        static string ByteArrayToBinary(byte[] input, string space = "")
        {
            return string.Join(space, input.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        static byte[] FixedXOR(byte[] input, byte[] xor)
        {
            Console.WriteLine(ByteArrayToBinary(input, " "));
            Console.WriteLine(string.Join(" ", Enumerable.Repeat(ByteArrayToBinary(xor, " "), (int)Math.Ceiling((decimal)input.Length / xor.Length))));
            byte[] output = new byte[input.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (byte)(input[i] ^ xor[i % xor.Length]);
            }
            Console.WriteLine(ByteArrayToBinary(output, " "));
            Console.WriteLine();
            return output;
        }

        static string DecodeSingleCharacterXor(string input, out int xor)
        {
            xor = 0;
            string currentBest = null;
            double score = double.NegativeInfinity;
            for (byte i = byte.MinValue; i < byte.MaxValue; i++)
            {
                string current = Convert.ToBase64String(FixedXOR(HexToByteArray(input), new[] { i }));
                double currentScore = EnglishLetterFrequencyCorrelation(current);
                if (currentScore > score)
                {
                    xor = i;
                    currentBest = current;
                    score = currentScore;
                }
            }
            return currentBest;
        }
        static double EnglishLetterFrequencyCorrelation(string input)
        {
            return
                (input.Where(l => l == 'a').Count() - 1) * 0.08167 +
                (input.Where(l => l == 'b').Count() - 1) * 0.01492 +
                (input.Where(l => l == 'c').Count() - 1) * 0.02782 +
                (input.Where(l => l == 'd').Count() - 1) * 0.04253 +
                (input.Where(l => l == 'e').Count() - 1) * 0.1270 +
                (input.Where(l => l == 'f').Count() - 1) * 0.02228 +
                (input.Where(l => l == 'g').Count() - 1) * 0.02015 +
                (input.Where(l => l == 'h').Count() - 1) * 0.06094 +
                (input.Where(l => l == 'i').Count() - 1) * 0.06966 +
                (input.Where(l => l == 'j').Count() - 1) * 0.00153 +
                (input.Where(l => l == 'k').Count() - 1) * 0.00772 +
                (input.Where(l => l == 'l').Count() - 1) * 0.04025 +
                (input.Where(l => l == 'm').Count() - 1) * 0.02406 +
                (input.Where(l => l == 'n').Count() - 1) * 0.06749 +
                (input.Where(l => l == 'o').Count() - 1) * 0.07507 +
                (input.Where(l => l == 'p').Count() - 1) * 0.01929 +
                (input.Where(l => l == 'q').Count() - 1) * 0.00095 +
                (input.Where(l => l == 'r').Count() - 1) * 0.05987 +
                (input.Where(l => l == 's').Count() - 1) * 0.06327 +
                (input.Where(l => l == 't').Count() - 1) * 0.09056 +
                (input.Where(l => l == 'u').Count() - 1) * 0.02758 +
                (input.Where(l => l == 'v').Count() - 1) * 0.00978 +
                (input.Where(l => l == 'w').Count() - 1) * 0.02360 +
                (input.Where(l => l == 'x').Count() - 1) * 0.00150 +
                (input.Where(l => l == 'y').Count() - 1) * 0.01974 +
                (input.Where(l => l == 'z').Count() - 1) * 0.00074;
        }
    }
}
