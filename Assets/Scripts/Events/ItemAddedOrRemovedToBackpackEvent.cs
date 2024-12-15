using Core;

namespace Events
{
    public class ItemAddedOrRemovedToBackpackEvent
    {
        public ItemConfig ItemConfig { get; set; }
        public string Action { get; set; }
    }
}
