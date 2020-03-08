using System;
using System.Collections.Generic;
using TextGame.Entities;
using TextGame.Flow;

namespace TextGame
{
    class Game
    {
        private static Character _character;
        public static Stack<Func<Character, Character>> FrameStack { get; } = new Stack<Func<Character, Character>>();

        static void Main(string[] args)
        {
            while(true) {
                Frame.Do(TitleScreen.Play);
                _character = Frame.Do(Character.GatherCharacter);

                ICampaign campaign = new ExampleCampaign.ExampleCampaign();
                campaign.Acts.ForEach(FrameStack.Push);
                while(_character != null)
                {
                    if (!FrameStack.TryPop(out var frame)) {
                        _character = HandleInput(_character);
                        continue;
                    }

                    Frame.Do(() => _character = frame(_character));
                }
            }
        }

        public static Character HandleInput(Character character) {
            var input = GetInput("What now?");
            character.Context.HandleInput(character, input);
            return character;
        }

        public static void WriteHelpText()
        {
            Console.WriteLine("Help text coming soon...", ConsoleColor.Green);
        }
        
        public static string GetInput(string prompt){
            WriteLine(prompt, ConsoleColor.Green);
            Console.Write(" > ");
            return Console.ReadLine();
        }

        public static void Write(string text, ConsoleColor color = ConsoleColor.Gray) 
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.Gray) 
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
