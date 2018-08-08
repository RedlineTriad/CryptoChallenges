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
            //Console.WriteLine(DecodeSingleCharacterXor(input, out int xor));
            //Console.WriteLine(xor);

            while (true)
            {
                //byte[] input = Convert.FromBase64String(PadBase64(Console.ReadLine()));
                //byte[] xor = HexToByteArray(Console.ReadLine());
                //byte[] result = FixedXOR(input, xor);
                Console.WriteLine(DecodeSingleCharacterXor(Console.ReadLine(), out long xor) + " " + xor);
                //Console.WriteLine(Convert.ToBase64String(result));
            }
        }

        static string HexToBase64(string hex)
        {
            return Convert.ToBase64String(HexToByteArray(hex));
        }

        static byte[] HexToByteArray(string hex)
        {
            byte[] output = new byte[(int)Math.Ceiling((double)hex.Length / 2)];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Convert.ToByte(hex.Substring(i * 2, Math.Min(hex.Length - (i * 2), 2)), 16);
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

        static string PadBase64(string base64)
        {
            return base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        }

        static byte[] FixedXOR(byte[] input, byte[] xor)
        {
            //Console.WriteLine(ByteArrayToBinary(input, " "));
            //Console.WriteLine(string.Join(" ", Enumerable.Repeat(ByteArrayToBinary(xor, " "), (int)Math.Ceiling((decimal)input.Length / xor.Length))));
            byte[] output = new byte[input.Length];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (byte)(input[i] ^ xor[i % xor.Length]);
            }
            //Console.WriteLine(ByteArrayToBinary(output, " "));
            //Console.WriteLine();
            return output;
        }

        static string DecodeSingleCharacterXor(string input, out long xor)
        {
            xor = 0;
            long xorTmp = 0;
            byte[] inputBytes = HexToByteArray(input);
            string currentBest = null;
            string currentSecondBest = null;
            string currentThirdBest = null;
            double error = double.PositiveInfinity;
            
            Parallel.For(int.MinValue, int.MaxValue, i =>
            {
                string current = Convert.ToBase64String(FixedXOR(inputBytes, BitConverter.GetBytes((short)i)));
                double currentError = EnglishLetterFrequencyCorrelation(current);
                //Console.WriteLine(current + " " + currentError + " " + i);
                Console.WriteLine(current + " " + i);
                if (currentError < error)
                {
                    //if (error < .4 || i % 1 == 0) Console.WriteLine(current +" "+ i);
                    xorTmp = i;
                    error = currentError;
                    currentThirdBest = currentSecondBest;
                    currentSecondBest = currentBest;
                    currentBest = current;
                }
            });
            xor = xorTmp;
            Console.WriteLine(currentThirdBest);
            Console.WriteLine(currentSecondBest);
            return currentBest;
        }
        static double EnglishLetterFrequencyCorrelation(string input)
        {
            input = input.ToLower();
            return
                Math.Abs(.08167 - (input.Count(c => c == 'a') / (double)input.Length)) +
                Math.Abs(.01492 - (input.Count(c => c == 'b') / (double)input.Length)) +
                Math.Abs(.02782 - (input.Count(c => c == 'c') / (double)input.Length)) +
                Math.Abs(.04253 - (input.Count(c => c == 'd') / (double)input.Length)) +
                Math.Abs(.12702 - (input.Count(c => c == 'e') / (double)input.Length)) +
                Math.Abs(.02228 - (input.Count(c => c == 'f') / (double)input.Length)) +
                Math.Abs(.02015 - (input.Count(c => c == 'g') / (double)input.Length)) +
                Math.Abs(.06094 - (input.Count(c => c == 'h') / (double)input.Length)) +
                Math.Abs(.06966 - (input.Count(c => c == 'i') / (double)input.Length)) +
                Math.Abs(.00153 - (input.Count(c => c == 'j') / (double)input.Length)) +
                Math.Abs(.00772 - (input.Count(c => c == 'k') / (double)input.Length)) +
                Math.Abs(.04025 - (input.Count(c => c == 'l') / (double)input.Length)) +
                Math.Abs(.02406 - (input.Count(c => c == 'm') / (double)input.Length)) +
                Math.Abs(.06749 - (input.Count(c => c == 'n') / (double)input.Length)) +
                Math.Abs(.07507 - (input.Count(c => c == 'o') / (double)input.Length)) +
                Math.Abs(.01929 - (input.Count(c => c == 'p') / (double)input.Length)) +
                Math.Abs(.00095 - (input.Count(c => c == 'q') / (double)input.Length)) +
                Math.Abs(.05987 - (input.Count(c => c == 'r') / (double)input.Length)) +
                Math.Abs(.06327 - (input.Count(c => c == 's') / (double)input.Length)) +
                Math.Abs(.09056 - (input.Count(c => c == 't') / (double)input.Length)) +
                Math.Abs(.02758 - (input.Count(c => c == 'u') / (double)input.Length)) +
                Math.Abs(.00978 - (input.Count(c => c == 'v') / (double)input.Length)) +
                Math.Abs(.02360 - (input.Count(c => c == 'w') / (double)input.Length)) +
                Math.Abs(.00150 - (input.Count(c => c == 'x') / (double)input.Length)) +
                Math.Abs(.01974 - (input.Count(c => c == 'y') / (double)input.Length)) +
                Math.Abs(.00074 - (input.Count(c => c == 'z') / (double)input.Length));
        }
    }
}
