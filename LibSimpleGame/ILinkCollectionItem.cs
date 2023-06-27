namespace LibSimpleGame
{
    public interface ILinkCollectionItem<TOwner>
    {
        public TOwner? Owner { get; internal set; }
    }
}