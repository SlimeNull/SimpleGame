namespace LibSimpleGame.Console
{
    public static class ConsoleGameExtensions
    {
        static void DefaultConsoleInputAdapterSetup(ConsoleGameInputAdapter adapter)
        {
            adapter.RegisterAxis("Horizontal", ConsoleKey.A, ConsoleKey.D);
            adapter.RegisterAxis("Horizontal", ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
            adapter.RegisterAxis("Vertical", ConsoleKey.S, ConsoleKey.W);
            adapter.RegisterAxis("Vertical", ConsoleKey.DownArrow, ConsoleKey.UpArrow);
        }

        public static void AddConsoleInputAdapter(this GameInput gameInput)
        {
            AddConsoleInputAdapter(gameInput, DefaultConsoleInputAdapterSetup);
        }

        public static void AddConsoleInputAdapter(this GameInput gameInput, Action<ConsoleGameInputAdapter> adapterSetup)
        {
            var adapter = new ConsoleGameInputAdapter();
            adapterSetup(adapter);
            gameInput.Adapters.Add(adapter);
        }
    }
}
