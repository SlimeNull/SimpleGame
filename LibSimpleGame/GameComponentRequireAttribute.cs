namespace LibSimpleGame
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameComponentRequireAttribute : Attribute
    {
        public Type[] RequiredComponentTypes { get; }

        public GameComponentRequireAttribute(Type reqiredComponentType)
        {
            RequiredComponentTypes = new[]
            {
                reqiredComponentType
            };
        }

        public GameComponentRequireAttribute(params Type[] requiredComponentTypes)
        {
            RequiredComponentTypes = requiredComponentTypes;
        }
    }
}