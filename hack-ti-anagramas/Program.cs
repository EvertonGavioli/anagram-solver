using System;

namespace hack_ti_anagramas
{
    class Program
    {       
        static void Main(string[] args)
        {
            bool runAgain = true;

            while (runAgain)
            {
                Console.Clear();
                header();

                Console.WriteLine("Informe a palavra para criação dos anagramas:");

                var inputSentence = Console.ReadLine();

                inputSentence = inputSentence.ToUpper();

                if (!Util.isValid(Util.RemoveSpaceStringReader(inputSentence)))
                {
                    Console.WriteLine("Error: Caracteres informados inválidos, permitido somente de [A..Z], " + Environment.NewLine +
                                      "pressione 'Enter' para voltar.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Aguarde! Gerando anagramas...");

                var initialTime = DateTime.Now;

                var listAnagrams = AnagramSolver.GenerateAnagrams(inputSentence);

                var totalSeconds = (DateTime.Now - initialTime).TotalSeconds;

                Console.Clear();
                Console.WriteLine(string.Format("[{0}] Anagramas encontrados para: {1}", listAnagrams.Count, inputSentence));
                Console.WriteLine();

                foreach (var item in listAnagrams)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                Console.WriteLine("Total Seconds: " + totalSeconds.ToString("N2"));
                Console.WriteLine();
                Console.WriteLine("pressione 'Enter' para continuar || 'N' para encerrar");
                var encerrar = Console.ReadLine();
                
                if(encerrar.ToUpper() == "N")
                {
                    runAgain = false;
                }
            }
        }
        
        private static void header()
        {
            Console.WriteLine("**********************************************************");
            Console.WriteLine("******************** ANAGRAM SOLVER **********************");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("Author:   Everton Pereira Gavioli ************************");
            Console.WriteLine("E-mail:   vertopge@hotmail.com    ************************");
            Console.WriteLine("Linkedin: Everton Gavioli         ************************");
            Console.WriteLine("Github:   EvertonGavioli          ************************");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("**********************************************************");
            Console.WriteLine();
        }
    }
}
