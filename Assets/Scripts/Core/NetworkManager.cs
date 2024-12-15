using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Events;

namespace Core
{
    public class NetworkManager : MonoBehaviour
    {
        private const string BaseUrl = "https://wadahub.manerai.com/api/inventory/status";
        private const string Token = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        private void Start()
        {
            EventManager.Subscribe<ItemAddedOrRemovedToBackpackEvent>(OnItemEvent);
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<ItemAddedOrRemovedToBackpackEvent>(OnItemEvent);
        }


        private void OnItemEvent(ItemAddedOrRemovedToBackpackEvent eventData)
        {
            _ = PostData(eventData);
        }

        private Task PostData(ItemAddedOrRemovedToBackpackEvent eventData)
        {
            if (eventData == null || string.IsNullOrEmpty(eventData.ItemConfig.id) || string.IsNullOrEmpty(eventData.Action))
            {
                Debug.LogWarning("Event data is invalid, nothing to send.");
                return Task.CompletedTask;
            }
            
            var postData = new PostData
            {
                itemId = eventData.ItemConfig.id,
                itemAction = eventData.Action
            };

            var jsonToSend = JsonUtility.ToJson(postData);

            var request = new UnityWebRequest(BaseUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonToSend)),
                downloadHandler = new DownloadHandlerBuffer()
            };

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {Token}");

            StartCoroutine(SendRequest(request));
            return Task.CompletedTask;
        }

        private static IEnumerator SendRequest(UnityWebRequest request)
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Failed to post data. Error: {request.error}");
            }
        }
    }
    
    [System.Serializable]
    public class PostData
    {
        public string itemId;
        public string itemAction;
    }
}