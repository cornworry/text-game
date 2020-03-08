using ExampleCampaign.Areas;
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
                        .AppendLine("You're sitting on a hard stone floor in a small room. A door with bars at face height dominates one wall. Another body lies in a heap in one corner.")
                        .AppendLine($"Your name is {character.Name}... you don't remember how you got here.");

            var body = Item.Named("body");  

            var smallCell = new SmallCell("a small cell")
                .ContainingEntities(
                    body,
                    Portal.To(Area.Named("guard room"))
                ); 

            body.SetHandler(Command.Examine, smallCell.ExamineTheBody)
                .SetHandler(Command.Search,  smallCell.SearchTheBody);

            Game.FrameStack.Push(new Cutscene(introBuilder.ToString(), character => {
                character.AddDebuff("head wound");
                character.SetContext(smallCell);
                return character;
            }).Play);
            
            return character;
        }

       
    }
}
