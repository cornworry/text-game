using System;
using System.Diagnostics;
using System.Linq;

namespace TextGame.Entities
{
    public interface ICanBeContext : IHandleActions
    {
    }

    public static class ICanBeContextExtensions 
    {
        public static Character HandleInput<T>(this T context, Character character, string input) where T: ICanBeContext
        {
            var commandInput = input.Split(" ")[0];
            var command = CommandParser.From(commandInput);

            if(command == null) {
                Game.WriteLine($"Unknown command: \"{commandInput}\"");
                return character;
            }

            var targetName = input.Substring(commandInput.Length).Trim().ToLowerInvariant();
            Debug.WriteLine($"HandleInput");
            Debug.WriteLine($"- command:\"{commandInput}\"");
            Debug.WriteLine($"- target:\"{targetName}\"");

            // The target is the area.
            if(!TryGetTarget(context, targetName, out var target))
            {
                Game.WriteLine($"There's nothing called \"{targetName}\" here.", ConsoleColor.Red);
                return character;
            }

            Debug.WriteLine($"Found target: {target.Name}.");
            if(target.ActionMap.TryGetValue((Command) command, out var action)) {
                return action.Invoke(character);
            }

            Game.WriteLine($"You can't do that right now.");
            return character;
        }

        private static bool TryGetTarget(ICanBeContext context, string targetName, out IHandleActions outputEntity)  
        {
            if(targetName == context.Name) 
            {
                outputEntity = context;
                return true;
            }
            
            var hasEntities = context as IHaveEntities;
            if(hasEntities != null) 
            {
                outputEntity = hasEntities.Entities
                    .Where(e => e.IsKnown)
                    .FirstOrDefault(e => e.Name == targetName);
                if(outputEntity != null) return true;
            }

            outputEntity = null;
            return false;
        }
    }
}