using System.Collections.ObjectModel;

namespace LibSimpleGame
{
    public class LinkCollection<TOwner, TItem> : Collection<TItem> where TItem : ILinkCollectionItem<TOwner>
    {
        public TOwner Owner { get; }

        public LinkCollection(TOwner owner)
        {
            Owner = owner;
        }

        protected override void SetItem(int index, TItem item)
        {
            TItem originComponent = this[index];

            if (object.ReferenceEquals(item, originComponent))
                return;

            UnlinkComponent(originComponent);

            EnsureComponentNotLinkedToOther(Owner, item);
            LinkComponent(Owner, item);

            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, TItem item)
        {
            EnsureComponentNotLinkedToOther(Owner, item);
            LinkComponent(Owner, item);

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            TItem originComponent = this[index];

            UnlinkComponent(originComponent);

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var component in this)
                UnlinkComponent(component);

            base.ClearItems();
        }



        void EnsureComponentNotLinkedToOther(TOwner gameObject, TItem item)
        {
            if (item.Owner != null && object.ReferenceEquals(item.Owner, gameObject))
                throw new InvalidOperationException("Item is already added to another collection");
        }

        void LinkComponent(TOwner gameObject, TItem item)
        {
            item.Owner = gameObject;
        }

        void UnlinkComponent(TItem item)
        {
            item.Owner = default;
        }
    }
}