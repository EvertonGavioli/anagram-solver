using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace hack_ti_anagramas
{
    public class Util
    {
        public static bool isValid(String str)
        {
            return Regex.IsMatch(str, @"^[A-Z]+$");
        }

        public static string RemoveSpaceStringReader(string input)
        {
            var s = new StringBuilder(input.Length);
            using (var reader = new StringReader(input))
            {
                int i = 0;
                char c;
                for (; i < input.Length; i++)
                {
                    c = (char)reader.Read();
                    if (!char.IsWhiteSpace(c))
                    {
                        s.Append(c);
                    }
                }
            }

            return s.ToString();
        }
    }
}
