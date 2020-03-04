using System.Collections.Generic;

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
            Game.WriteLine($"You can't do that in {Name}.");
            return character;
        }
    }
}