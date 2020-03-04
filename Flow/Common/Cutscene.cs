using System;

namespace TextGame.Flow.Common
{
    public class Cutscene
    {
        private Func<Character, Character> _effects;

        public string Message { get; private set; }

        public Cutscene(string message, Func<Character, Character> effects = null) 
        { 
            Message = message; 
            _effects = effects;
        }

        public Character Play(Character character)
        {
            return Frame.Do(() => {
                Game.WriteLine($"{Message}\n", ConsoleColor.Green);
                return _effects?.Invoke(character) ?? character;
            });
        }
    }
}