using System;
using System.Collections.Generic;
using TextGame.Entities;
using TextGame.Entities.Common;
using TextGame.Flow;
using TextGame.Flow.Common;

namespace TextGame.ExampleCampaign 
{
    public class ExampleCampaign : ICampaign
    {
        public List<Func<Character, Character>> Acts { get; } = new List<Func<Character, Character>>() { Act };

        public static Character Act(Character character)
        {
            var introMessage = $@"Your head aches. Your head feels wet and sticky.
You're sitting on a hard stone floor in a small room. A door with bars at face height dominates one wall. One other person is lies in a heap in the corner.
Your name is {character.Name}... you don't remember how you got here.";

            var startingLocation = Area.Named("a small cell")
                .ContainingEntities(
                    Item.Named("body").AddHandler(Command.Examine, CheckTheBody),
                    Portal.To(Area.Named("guard room"))
                ); 

            Game.FrameStack.Push(new Cutscene(introMessage, character => {
                character.AddDebuff("head wound");
                character.SetContext(startingLocation);
                return character;
            }).Play);
            
            return character;
        }

        private static Character CheckTheBody(Character character)
        {
            var message = "The man is curled in a ball facing the corner. His shirt is torn and bloody. Shackles are affixed to his ankles. He chest rises and falls laboriously.";
            Game.WriteLine(message);
            return character;
        }
    }
}