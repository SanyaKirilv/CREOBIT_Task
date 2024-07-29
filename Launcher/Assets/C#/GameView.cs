using UnityEngine;
using UnityEngine.UI;

namespace Launcher
{
    [System.Serializable]
    public class GameView
    {
        [Header("Buttons")]
        public Button playButton;
        public Button loadButton;
        public Button unloadButton;

        public void Update(bool isLoaded, bool haveInternet)
        {
            playButton.interactable = isLoaded;
            loadButton.interactable =! isLoaded && haveInternet;
            unloadButton.interactable = isLoaded;
        }
    }
}