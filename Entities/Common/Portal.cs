using System.Text;

namespace TextGame.Entities.Common
{
    //TODO: Need a way to reference doors. The sentence form name isn't helping.
    public class Portal : Entity
    {
        private Portal() { }

        public Entity ToArea { get; private set; }
        public bool IsLocked { get; private set; }
        
        //TODO: Come up with a better word for "can you see where it leads".
        public bool IsTransparent { get; private set; }

        public static Portal To(Area toArea)
        {
            var portal = new Portal 
            { 
                Name        = $"portal to {toArea.Name}",
                ToArea      = toArea,
                IsKnown     = true
            };

            portal.ActionMap.Add(Command.Examine, (c) => HandleExamine(portal, c));

            return portal;
        }

        public Portal Locked()
        {
            IsLocked = true;
            return this;
        }

        public bool HasBeenSearched { get; private set; }

        private static Character HandleExamine(Portal portal, Character character)
        {
            var sb = new StringBuilder();

            if(portal.IsTransparent) sb.Append($"A portal to {portal.ToArea.Name}. ");
            else sb.Append("A mysterious door. ");

            if(portal.HasBeenSearched)
            {
                sb.Append($"It appears to be {(!portal.IsLocked? "": "un")}locked.");
            }

            return character;
        }
    }
}