namespace LibSimpleGame
{
    public partial class Game
    {
        public sealed class GameObjectCollection : LinkCollection<Game, GameObject>
        {
            public GameObjectCollection(Game owner) : base(owner)
            {
            }
        }
    }
}