using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace Core
{
    public class NetworkManager : MonoBehaviour
    {
        private const string BaseUrl = "https://wadahub.manerai.com/api/inventory/status";
        private const string Token = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        public async Task PostData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                Debug.LogWarning("Data is null or empty, nothing to send.");
                return;
            }

            var request = new UnityWebRequest(BaseUrl, "POST");
            var jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {Token}");

            await SendRequest(request);
        }

        private async Task SendRequest(UnityWebRequest request)
        {
            await Task.Run(request.SendWebRequest);

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data successfully posted to the server.");
            }
            else
            {
                Debug.LogError($"Failed to post data. Error: {request.error}");
            }
        }
    }
}