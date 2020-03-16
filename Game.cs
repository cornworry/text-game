using System;
using System.Collections.Generic;
using System.Linq;
using TextGame.Flow;

namespace TextGame
{
    public class TextGame
    {
        private Stack<Func<Character, Character>> _frameStack { get; }

        public TextGame() 
        {
            _frameStack = new Stack<Func<Character, Character>>();
        }

        public void Start(ICampaign campaign)
        {
            Character character = null;
            _frameStack.Push(campaign.FirstFrame);
            while(true) {
                if (!_frameStack.TryPop(out var frameFunc)) {
                    frameFunc = campaign.DefaultFrame;
                }

                Frame.Do(() => character = frameFunc(character));
                if(character == null && !_frameStack.Any())
                {
                    return;
                }
            }
        }

        internal void Push(Func<Character, Character> frame)
        {
            _frameStack.Push(frame);
        }

        public void WriteHelpText()
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
