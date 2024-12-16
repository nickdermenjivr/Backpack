using _Project.Scripts.Core;

namespace _Project.Scripts.Events
{
    public class ItemAddedOrRemovedToBackpackEvent
    {
        public ItemConfig ItemConfig { get; set; }
        public string Action { get; set; }
    }
}
