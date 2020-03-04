using System;
using System.Collections.Generic;
using System.Linq;

namespace TextGame.Entities.Common
{
    public class Area : Entity, IHaveEntities, ICanBeContext
    {
        private Area() { }

        public new string Name { get; set; }

        // IHaveEntities
        public List<Entity> Entities { get; } = new List<Entity>();
        public static Area Named(string name) => new Area { Name = name };

        public Character HandleInput(Character character, string input)
        {
            var commandInput = input.Split(" ")[0];
            var command = CommandParser.From(commandInput);

            if(command == null) {
                Game.WriteLine($"Unknown command: \"{commandInput}\"");
                return character;
            }

            var targetName = input.Substring(commandInput.Length).Trim().ToLowerInvariant();
            Console.WriteLine($"command:\"{commandInput}\"\ntarget:\"{targetName}\"");

            var target = Entities.FirstOrDefault(e => e.Name == targetName);
            if(target == null) 
            {
                Game.WriteLine($"Unknown target: \"{targetName}\"");
                return character;
            }

            if(target.ActionMap.TryGetValue((Command) command, out var action)) {
                return action.Invoke(character);
            }

            Game.WriteLine($"You can't do that in {Name}.");
            return character;
        }
    }


}