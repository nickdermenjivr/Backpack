using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Core
{
    public class BootstrapScene : MonoBehaviour
    {
        public NetworkManager networkManager;
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
            if (!networkManager)
            {
                Debug.LogError("Managers aren't assigned!");
                return false;
            }
            
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