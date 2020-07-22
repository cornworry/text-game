using ExampleCampaign.Areas;
using System.Text;
using TextGame.Entities;
using TextGame.Entities.Common;
using TextGame.Flow;
using TextGame.Flow.Common;

namespace TextGame.ExampleCampaign 
{
    public class ExampleCampaign : ICampaign
    {
        static void Main(string[] args)
        {
            var campaign = new ExampleCampaign();
            campaign.Play();
        }

        public void Play()
        {
            TextGame.Start(this);
        }
        
        public Character FirstFrame(Character character)
        {
            TitleScreen.Play();
            TextGame.Push(WakeUp);

            // We probably don't have a character.
            if(character == null) TextGame.Push(c => GetCharacterInfo());
            return character;
        }

        public Character DefaultFrame(Character character)
        {
            var input = TextGame.GetInput("What now?");
            character.Context.HandleInput(character, input);
            return character;
        }

        public Character GetCharacterInfo() 
        {
            return Character.GatherCharacter();
        }

        public Character WakeUp(Character character)
        {
            var introBuilder = new StringBuilder();
            introBuilder.AppendLine("Your head aches. You touch your forehead and it comes away wet and sticky.")
                        .AppendLine("You're sitting on a hard stone floor. Stone walls on all sides. A barred door dominates one wall. Someone else lies in a heap in one corner.")
                        .AppendLine($"Your name is {character.Name}... you don't remember how you got here.");

            var body = Item.Named("body");  
            var guardRoom = Area.Named("guard room");
            var portal = Portal.To(guardRoom).Locked();

            var smallCell = new SmallCell("a small cell")
                .ContainingEntities(body, portal); 

            body.SetHandler(Command.Examine, smallCell.ExamineTheBody)
                .SetHandler(Command.Search,  smallCell.SearchTheBody);

            return new Cutscene(introBuilder.ToString(), character => {
                character.AddDebuff("head wound");
                character.SetContext(smallCell);
                return character;
            }).Play(character);
        }

       
    }
}
