using System;

namespace PenduConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Support des accents
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            WordBank wordBank = new WordBank();
            bool keepPlaying = true;

            while (keepPlaying)
            {
                string wordToGuess = wordBank.GetRandomWord();
                Game game = new Game(wordToGuess);
                
                // On récupère ici le résultat du choix (R ou Q)
                keepPlaying = game.Start(); 
            }

            Console.Clear();
            Console.WriteLine("Merci d'avoir joué ! À bientôt.");
            System.Threading.Thread.Sleep(1500);
        }
    }
}