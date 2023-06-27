using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSimpleGame
{

    public partial class Game
    {
        public GameTime Time { get; }
        public GameInput Input { get; }
        public GameObjectCollection Objects { get; }

        public Game()
        {
            Time = new GameTime(this);
            Input = new GameInput(this);
            Objects = new GameObjectCollection(this);
        }

        public GameObject? GetObject(string name)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            return Objects.FirstOrDefault(obj => string.Equals(obj.Name, name));
        }

        public void AddObject(GameObject gameObject)
        {
            Objects.Add(gameObject);

            foreach (var component in gameObject.Components)
                component.Awake();
        }

        public void DestroyObject(GameObject gameObject)
        {
            foreach (var component in gameObject.Components)
                component.OnDestroy();

            Objects.Remove(gameObject);
        }

        public void Start()
        {
            Time.Start();
        }

        public void Stop()
        {
            Time.Stop();
        }
    }
}
