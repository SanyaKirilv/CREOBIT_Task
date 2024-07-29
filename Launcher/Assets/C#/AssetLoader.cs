using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Launcher
{
    public class AssetLoader : MonoBehaviour 
    {
        [Header("Game data")]
        [SerializeField] private GameData gameData;
        [SerializeField] private GameView gameView;
        [Header("Asset data")]
        [SerializeField] private AssetBundle assetBundle;
        [Header("UI element")]
        [SerializeField] private Text errorText;

        private bool haveInternet;

        private void Awake()
        {
            gameView.playButton.onClick.AddListener(() => Play());
            gameView.loadButton.onClick.AddListener(() => Load());
            gameView.unloadButton.onClick.AddListener(() => UnloadBundle());
        }

        private void Start()
        {
            StartCoroutine(CheckInternetConnection());
            print(gameData.FilePath);

            UpdateView();
        }

        private IEnumerator CheckInternetConnection()
        {
            var request = new UnityWebRequest("http://google.com");
            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError
                or UnityWebRequest.Result.DataProcessingError
                or UnityWebRequest.Result.ProtocolError)
            {
                errorText.text = request.error;
                haveInternet = false;
            }
            else
            {
                errorText.text = " ";
                haveInternet = true;
            }
            UpdateView();
        }

        private void Load()
        {
            AssetBundle.UnloadAllAssetBundles(true);
            if(!gameData.IsDowloaded)
                StartCoroutine(Download(gameData.Id));
            
            LoadBundle();
            UpdateView();
        }

        private void LoadBundle()
        {
            var bundle = AssetBundle.LoadFromFile(gameData.FilePath);
            assetBundle = bundle;
        }

        private IEnumerator Download(string id)
        {
            var request = UnityWebRequest.Get("https://drive.google.com/uc?export=download&id=" + id);
            yield return request.SendWebRequest();

            if (request.result is UnityWebRequest.Result.ConnectionError
                or UnityWebRequest.Result.DataProcessingError
                or UnityWebRequest.Result.ProtocolError)
            {
                errorText.text = request.error;
                yield break;
            }

            byte[] bytes = request.downloadHandler.data;
            File.WriteAllBytes(gameData.FilePath, bytes);
        }

        private void Play()
        {
            if (assetBundle == null)
                errorText.text = "Failed to load game";
            
            SceneManager.LoadScene(gameData.Name, LoadSceneMode.Additive);
        }

        private void UnloadBundle()
        {
            assetBundle.Unload(false);
            File.Delete(gameData.FilePath);
            Caching.ClearAllCachedVersions(gameData.Name);
            UpdateView();
        }

        private void UpdateView() => gameView.Update(assetBundle != null, haveInternet);
    }
}