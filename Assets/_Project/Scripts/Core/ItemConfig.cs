using _Project.Scripts.Configs;

namespace _Project.Scripts.Core
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