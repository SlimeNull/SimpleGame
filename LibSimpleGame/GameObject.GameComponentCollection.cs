using System.Collections.ObjectModel;

namespace LibSimpleGame
{

    public partial class GameObject
    {
        public sealed class GameComponentCollection : LinkCollection<GameObject, GameComponent>
        {
            public GameComponentCollection(GameObject owner) : base(owner)
            {
            }
        }
    }
}