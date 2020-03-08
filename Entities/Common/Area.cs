using System.Collections.Generic;

namespace TextGame.Entities.Common
{
    public class Area : Entity, IHaveEntities, ICanBeContext
    {
        public Area() { }
        public new string Name { get; set; }

        // IHaveEntities
        public List<IHandleActions> Entities { get; } = new List<IHandleActions>();
        public static Area Named(string name) => new Area { Name = name };
    }


}