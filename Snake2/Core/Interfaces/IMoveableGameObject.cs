namespace Snake2.Core.Interfaces
{
    public interface IMoveableGameObject : IGameObject
    {
        int MovementSpeed { get; set; }
    }
}
