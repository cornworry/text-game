using System;
using System.Collections.Generic;
using TextGame.Entities;
using TextGame.Entities.Common;
using TextGame.Flow;
using TextGame.Flow.Common;

namespace TextGame {
    public class Character
    {
        public string Name { get; private set; }
        public List<string> Debuffs { get; } = new List<string>();
        public ICanBeContext Context { get; private set; }

        public static Character GatherCharacter()
        {
            var character = new Character();
            Func<string, bool> emptyTest = str => !string.IsNullOrEmpty(str);
            character.Name = Question.With(prompt: "What is your name?", validate: emptyTest).GetInput();
            return character;
        }

        internal void SetContext(ICanBeContext context) 
        {
            var location = context as Area;
            if(location != null) TextGame.WriteLine($"You are now in a new location: {context.Name}.", ConsoleColor.DarkGreen);
            Context = location;
        }

        //TODO: Add type for debuff. Type and params. 
        //TODO: For a head wound type of Injury, and Head as a location param.
        internal void AddDebuff(string v)
        {
            TextGame.WriteLine($"You have a new debuff: {v}.", ConsoleColor.Red);
            Debuffs.Add(v);
        }
    }
}
