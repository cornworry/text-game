using System;
using TextGame;
using TextGame.Entities.Common;

namespace ExampleCampaign.Areas
{
    public class SmallCell : Area
    {
        public SmallCell(string name)
        {
            Name = name;
        }

        public Character ExamineTheBody(Character character)
        {
            TextGame.TextGame.WriteLine("The man is curled in a ball facing the corner. His shirt is torn and bloody. Shackles are affixed to his ankles. He chest rises and falls laboriously.");
            return character;
        }

        public Character SearchTheBody(Character character)
        {
            TextGame.TextGame.Write("As you begin searching the man he rolls onto his back. His eyelids part slightly to reveal his eyes rolled in the back of his head. He lets out a shudder. ");
            TextGame.TextGame.Write("\"PLEASE NO! I haven't...\" ", ConsoleColor.Yellow);
            TextGame.TextGame.WriteLine("His chest lurches upward. After a moment his body slumps, settling back on the ground.");
            TextGame.TextGame.WriteLine("He's not waking up anytime soon.");

            return character;
        }
    }
}