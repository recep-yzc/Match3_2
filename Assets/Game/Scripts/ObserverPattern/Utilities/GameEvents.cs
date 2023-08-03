namespace Game.Scripts.ObserverPattern.Utilities
{
    internal static class GameEvents
    {
        public static class Cube
        {
            public const string OnFallCompleted = nameof(OnFallCompleted);
            public const string OnCubeCreate = nameof(OnCubeCreate);
            public const string OnCubeCreateAfter = nameof(OnCubeCreateAfter);
            public const string OnCubeControllerInit = nameof(OnCubeControllerInit);
        }

        public static class Cell
        {
            public const string OnCellCreate = nameof(OnCellCreate);
            public const string OnCellControllerInit = nameof(OnCellControllerInit);
            public const string OnClicked = nameof(OnClicked);
        }

        public static class Pool
        {
            public const string OnPoolCreate = nameof(OnPoolCreate);
        }
    }
}