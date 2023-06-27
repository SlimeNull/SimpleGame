namespace LibSimpleGame.Console
{
    public record ConsoleGameAdapters(ConsoleGameInputAdapter InputAdapter, ConsoleGameRenderAdapter RenderAdapter);

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

        public static void AddConsoleRenderAdapter(this GameRenderer gameRenderer, int width, int height)
        {
            var adapter = new ConsoleGameRenderAdapter(width, height);
            gameRenderer.Adapters.Add(adapter);
        }

        public static void AddConsoleRenderAdapter(this GameRenderer gameRenderer, Action<ConsoleGameRenderAdapter> adapterSetup)
        {
            var adapter = new ConsoleGameRenderAdapter();
            adapterSetup(adapter);
            gameRenderer.Adapters.Add(adapter);
        }

        public static void AddConsoleAdapters(this Game game)
        {
            var adapters = new ConsoleGameAdapters(new ConsoleGameInputAdapter(), new ConsoleGameRenderAdapter());
            game.Input.Adapters.Add(adapters.InputAdapter);
            game.Renderer.Adapters.Add(adapters.RenderAdapter);
        }

        public static void AddConsoleAdapters(this Game game, Action<ConsoleGameAdapters> adapterSetup)
        {
            var adapters = new ConsoleGameAdapters(new ConsoleGameInputAdapter(), new ConsoleGameRenderAdapter());
            adapterSetup(adapters);
            game.Input.Adapters.Add(adapters.InputAdapter);
            game.Renderer.Adapters.Add(adapters.RenderAdapter);
        }
    }
}
