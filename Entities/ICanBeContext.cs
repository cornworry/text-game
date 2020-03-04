namespace TextGame.Entities
{
    public interface ICanBeContext 
    {
        
        string Name { get; set; }
        Character HandleInput(Character character, string input);
    }
}