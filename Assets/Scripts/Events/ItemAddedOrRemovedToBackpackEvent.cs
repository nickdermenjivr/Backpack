using Core;

namespace Events
{
    public class ItemAddedOrRemovedToBackpackEvent
    {
        public ItemConfig Item { get; set; }
        public string Action { get; set; }
    }
}
