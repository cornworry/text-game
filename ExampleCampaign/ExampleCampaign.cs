using System;
using System.Collections.Generic;
using System.Text;
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
            var introBuilder = new StringBuilder();
            introBuilder.AppendLine("Your head aches. Your head feels wet and sticky.")
                        .AppendLine("You're sitting on a hard stone floor in a small room. A door with bars at face height dominates one wall. One other person is lies in a heap in the corner.")
                        .AppendLine($"Your name is {character.Name}... you don't remember how you got here.");

            var body = Item.Named("body");  

            var smallCell = Area.Named("a small cell")
                .ContainingEntities(
                    body,
                    Portal.To(Area.Named("guard room"))
                ); 

            body.SetHandler(Command.Examine, c => ExamineTheBody(c, smallCell))
                .SetHandler(Command.Search,  SearchTheBody);

            Game.FrameStack.Push(new Cutscene(introBuilder.ToString(), character => {
                character.AddDebuff("head wound");
                character.SetContext(smallCell);
                return character;
            }).Play);
            
            return character;
        }

        private static Character ExamineTheBody(Character character, Area smallCell)
        {
            Game.WriteLine("The man is curled in a ball facing the corner. His shirt is torn and bloody. Shackles are affixed to his ankles. He chest rises and falls laboriously.");
            return character;
        }

        private static Character SearchTheBody(Character character)
        {
            Game.WriteLine("As you begin searching the man he rolls onto his back. His eyelids part slightly to reveal his eyes rolled in the back of his head. He lets out a shudder.");
            Game.WriteLine("\"PLEASE NO! I haven't...\"", ConsoleColor.Yellow);
            Game.WriteLine("His chest lurches upward as he lets out a sigh. His body slumps back settling into unconsciousness.");
            return character;
        }
    }
}
