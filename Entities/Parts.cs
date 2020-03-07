using System;
using System.Collections.Generic;

namespace TextGame.Entities
{
    public interface IHandleActions : ICanBeHidden, IHaveAName
    {
        Dictionary<Command, Func<Character, Character>> ActionMap { get; }
    }

    public interface ICanBeHidden 
    {
        bool IsKnown { get; }
    }

    public interface IHaveAName
    {
        string Name { get; }
    }
}