using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace hack_ti_anagramas
{
    public class ReadFileTXT
    {
        public static List<string> ReadValidWordsFile()
        {
            string FILE_NAME = Path.Combine(Environment.CurrentDirectory, "validWords.txt");

            try
            {
                return System.IO.File.ReadAllLines(FILE_NAME).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Falha ao ler arquivo de palavras válidas" + Environment.NewLine +
                                  "pressione qualquer tecla para encerrar");
                Console.ReadLine();

                Environment.Exit(1);
                return null;
            }
        }
    }
}
