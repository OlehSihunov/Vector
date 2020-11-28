using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    class Program
    {
        static string Encode(string str, int rails)
        {
            if (rails < 2)
            {
                return "To few rails (rails>=2)";
            }
            bool direction = true;
            string[] arr = new string[rails];
            for (int i = 0, j = 0; i < str.Length; i++)
            {
                arr[j] += str[i];
                if ((j == rails - 1 && direction) || (j == 0 && !direction))
                {
                    direction = !direction;
                }
                if (direction)
                {
                    j++;
                }
                else
                {
                    j--;
                }
            }
            return String.Join("", arr);
        }

        static string Decode(string str, int rails)
        {
            if (rails < 2)
            {
                return "To few rails (rails>=2)";
            }
            bool direction = true;
            char[,] arr = new char[rails, str.Length];

            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    arr[i, j] = '\0';
                }

            }

            for (int i = 0, j = 0; i < str.Length; i++)
            {
                arr[j, i] = '*';

                if ((j == rails - 1 && direction) || (j == 0 && !direction))
                {
                    direction = !direction;
                }
                if (direction)
                {
                    j++;
                }
                else
                {
                    j--;
                }

            }

            int k = 0;
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    if (arr[i, j] == '*' && k < str.Length)
                    {
                        arr[i, j] = str[k++];
                    }
                }
            }

            string result = "";
            for (int j = 0; j < str.Length; j++)
            {
                for (int i = 0; i < rails; i++)
                {
                    if (arr[i, j] != '\0')
                    {
                        result += arr[i, j];
                    }
                }
            }
            return result;
        }


        static void Main(string[] args)
        {
            Console.Write("Enter string to decript: ");
            string str = Console.ReadLine();
            Console.Write("Enter number of rails: ");
            int rails = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            string encoded = Encode(str, rails);
            string decoded = Decode(encoded, rails);
            Console.WriteLine($"Your string: {str}");
            Console.WriteLine($"Encoded string: {encoded}");
            Console.WriteLine($"Decoded string: {decoded}");
            Console.ReadKey();
        }
    }
}
