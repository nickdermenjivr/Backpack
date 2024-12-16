using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core
{
    public class BootstrapScene : MonoBehaviour
    {
        public NetworkManager networkManager;
        public InventoryManager inventoryManager;
        public string sceneToLoad;

        private void Start()
        {
            if (InitializeManagers())
            {
                LoadScene();
            }
        }

        private bool InitializeManagers()
        {
            if (!inventoryManager || !networkManager)
            {
                Debug.LogError("Managers aren't assigned!");
                return false;
            }
            
            DontDestroyOnLoad(inventoryManager.gameObject);
            DontDestroyOnLoad(networkManager.gameObject);
            return true;
        }

        private void LoadScene()
        {
            try
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load scene: {e}");
                throw;
            }
        }
    }
}