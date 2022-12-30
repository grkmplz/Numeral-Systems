using System;
using System.Linq;

namespace Project_01_NumeralSystems
{
    class Program
    {
        public static string Reverse(ref string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string Convert_toDecimal(string getinput, int b)
        {
            string answer = "", fraction = "";
            int whole, remain;
            var char_abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
            bool addFraction = false;

            if (getinput.Contains(","))
            {
                addFraction = true;
                fraction = "0" + getinput.Substring(getinput.IndexOf(','));
                getinput = getinput.Remove(getinput.IndexOf(','));
                if (!fraction.Substring(2).All(char.IsDigit))
                {
                    throw new Exception("Invalid number !! Please try again ");
                }
            }

            if (!getinput.All(char.IsDigit))
            {
                throw new Exception("Invalid number !! Please try again ");
            }

            whole = int.Parse(getinput);
            while (whole != 0)
            {
                remain = whole % b;
                whole /= b;
                if (remain > 9)
                {
                    answer += char_abc[remain - 10];
                }
                else
                {
                    answer += remain;
                }
            }

            answer = Reverse(ref answer);
            if (addFraction)
            {
                answer += "," + ConvertDecimalFraction(double.Parse(fraction), b);
            }

            return answer;
        }
        public static string ConvertDecimalFraction(double getinput, int b)
        {
            string answer = "";
            var char_abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

            while (!(getinput == 0))
            {
                getinput *= b;
                if (getinput >= 10)
                {
                    answer += char_abc[Convert.ToInt32(getinput) - 10].ToString();
                }
                else
                {
                    answer += Math.Floor(getinput).ToString();
                }
                if (getinput > 0)
                {
                    getinput %= 1;
                }
            }

            return answer;

        }
        public static string ToDecimal(string getinput, int b) 
        {
            string fraction = "";
            double numb = 0, dig = 0, pow;
            var char_abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

            if (getinput.Contains(","))
            {
                fraction = getinput.Substring(getinput.IndexOf(','));
                getinput = getinput.Remove(getinput.IndexOf(','));
            }
            if (b == 8 && (getinput.Contains("8") || (getinput.Contains("9") || !getinput.All(char.IsDigit))))
            {
                throw new Exception("Invalid number !! Please try again ");
            }

            pow = getinput.Length - 1;
            getinput += fraction;
            for (int i = 0; i < getinput.Length; i++)
            {
                if(char.IsDigit(getinput[i]))
                {
                    dig = double.Parse(getinput[i].ToString());
                }
                else if(getinput[i] == ',')
                {
                    continue;
                }
                else
                {
                    dig = 10 + Array.IndexOf(char_abc, char.ToUpper(getinput[i]));
                    if(dig >= b)
                    {
                        throw new Exception("Incorrect number !! Please try again ");
                    }
                }

                numb += dig * Math.Pow(b, pow);
                pow--;
            }

            return numb.ToString();
        }

        public static int Base_Detector(string num)
        {
            char[] Number_listS = num.ToCharArray();

            if (Number_listS[0] == '0')
            {
                if (Number_listS[1] == 'x')
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }

      

        static void Main(string[] args)
        {
            string dec_val="", bin_val="", oct_val="", hex_val="";
            string valinput;
            do
            {
                Console.Clear();
                Console.WriteLine("Write a number to apply the conversion \nNot: (use coma(,) not dot() between integer and fraction part): ");
                string getinput = Console.ReadLine();

                switch (Base_Detector(getinput)) // 1 - for decimal, 2 - for octal, 3 - for hexadecimal;
                {
                    case 1:
                        {
                            dec_val = getinput;
                            bin_val = Convert_toDecimal(dec_val, 2);
                            oct_val = Convert_toDecimal(dec_val, 8);
                            hex_val = Convert_toDecimal(dec_val, 16);
                            break;
                        }
                    case 2:
                        {
                            dec_val = ToDecimal(getinput.Substring(1), 8);
                            bin_val = Convert_toDecimal(dec_val, 2);
                            oct_val = getinput.Substring(1);
                            hex_val = Convert_toDecimal(dec_val, 16);
                            break;
                        }
                    case 3:
                        {
                            dec_val = ToDecimal(getinput.Substring(2), 16);
                            bin_val = Convert_toDecimal(dec_val, 2);
                            oct_val = Convert_toDecimal(dec_val, 8);
                            hex_val = getinput.Substring(2);
                            break;
                        }
                }

                Console.WriteLine("Decimal Value is : " + dec_val);
                Console.WriteLine("Binary Value is : " + bin_val);
                Console.WriteLine("Octal Value is : 0" + oct_val);
                Console.WriteLine("Hexadecimal Value is : 0x" + hex_val);

                Console.WriteLine("\nPleasae type any to try again.. or exit");
                valinput = Console.ReadLine();

            } while (valinput != "exit");

         

        }
    }
}
