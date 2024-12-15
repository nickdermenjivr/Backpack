using Configs;

namespace Core
{
    [System.Serializable]
    public class ItemConfig
    {
        public string id;
        public string name;
        public float weight;
        public ItemType type;
    }
}