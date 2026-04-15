using System;
using System.Collections.Generic;
using System.Text;

namespace PenduConsole
{
    public class Game
    {
        private string wordToGuess;
        private HashSet<char> guessedLetters;
        private int maxLives = 10;
        private int wrongGuesses = 0;

        public Game(string word)
        {
            wordToGuess = word.ToUpper();
            guessedLetters = new HashSet<char>();
        }

        public bool Start()
        {
            bool isGameOver = false;
            bool isWin = false;

            while (!isGameOver)
            {
                Console.Clear();
                Console.WriteLine("=== JEU DU PENDU ===");
                Display.DrawHangman(wrongGuesses);
                
                string hiddenWord = GetHiddenWord();
                Display.ShowGameState(hiddenWord, maxLives - wrongGuesses, guessedLetters);

                if (!hiddenWord.Contains("_")) { isWin = true; isGameOver = true; break; }
                if (wrongGuesses >= maxLives) { isGameOver = true; break; }

                Console.Write("\nEntrez une lettre ou le mot : ");
                string input = Console.ReadLine()?.ToUpper().Trim();

                if (string.IsNullOrEmpty(input)) continue;

                if (input.Length == 1 && char.IsLetter(input[0])) ProcessGuess(input[0]);
                else if (input.Length > 1) ProcessWordGuess(input);
            }

            return EndGame(isWin);
        }

        private string GetHiddenWord()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in wordToGuess)
            {
                if (c == ' ') sb.Append("  ");
                else if (guessedLetters.Contains(c)) sb.Append(c + " ");
                else sb.Append("_ ");
            }
            return sb.ToString().Trim();
        }

        private void ProcessGuess(char guess)
        {
            if (guessedLetters.Contains(guess)) return;
            guessedLetters.Add(guess);
            if (!wordToGuess.Contains(guess)) wrongGuesses++;
        }

        private void ProcessWordGuess(string guessedWord)
        {
            if (guessedWord == wordToGuess)
            {
                foreach (char c in wordToGuess) guessedLetters.Add(c);
            }
            else
            {
                wrongGuesses++;
                Console.WriteLine("MAUVAIS MOT !");
                System.Threading.Thread.Sleep(800);
            }
        }

        private bool EndGame(bool isWin)
        {
            Console.Clear();
            if (isWin) 
            {
                Display.ShowWinArt();
            }
            else 
            {
                Display.DrawHangman(wrongGuesses);
                Display.ShowLoseArt();
                Console.WriteLine($"\nLe mot était : {wordToGuess}");
            }

            Console.WriteLine("\n------------------------------------------");
            Console.WriteLine("  Appuyez sur [R] pour REJOUER");
            Console.WriteLine("  Appuyez sur [Q] pour QUITTER");
            Console.WriteLine("------------------------------------------");
            
            while (true)
            {
                // On lit la touche et on la transforme en majuscule pour comparer
                char key = Console.ReadKey(true).KeyChar;
                key = char.ToUpper(key); 

                if (key == 'R') return true;  // Renvoie "vrai" à Program.cs pour boucler
                if (key == 'Q') return false; // Renvoie "faux" pour arrêter
            }
        }
    }
}