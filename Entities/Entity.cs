using System;
using System.Collections.Generic;
using TextGame.Flow;

namespace TextGame.Entities
{
    public abstract class Entity : IHandleActions
    {
        public string Name { get; set; }
        public bool IsKnown { get; set; }

        public Dictionary<Command, Func<Character, Character>> ActionMap { get; } 
            = new Dictionary<Command, Func<Character, Character>>();
    }

    public static class IEntityExtensions 
    {
        public static T AsHidden<T>(this T entity) where T : Entity
        {
            entity.IsKnown = true;
            return entity;
        }

        public static T SetHandler<T>(this T entity, Command command, Func<Character, Character> handler) where T : Entity
        {
            entity.ActionMap[command] = handler;
            return entity;
        }
    }

    public enum Command 
    {
        Examine,
        Search,
        Grab,
        Go
    }

        public class CommandParser
    {
        public static Command? From(string input) {

            if(!Enum.TryParse(typeof(Command), input, true, out var command)) return null;
            return (Command?) command;
        }
    }
}