using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Launcher.RunGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float currentTime;
        [SerializeField] private GameObject winScreen;
        [Header("Text Elements")]
        [SerializeField] private Text[] currentTimeText;
        [SerializeField] private Text[] bestTimeText;
        [Header("Buttons")]
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button resumeBtn;

        private float BestTime
        {
            get => PlayerPrefs.HasKey("Time") ? PlayerPrefs.GetFloat("Time") : 10000;
            set => PlayerPrefs.SetFloat("Time", value);
        }

        private bool isCompleted;
        
        private void Awake()
        {
            menuBtn.onClick.AddListener(() => Unload());
            resumeBtn.onClick.AddListener(() => Unload());

            UpdateTimeText(currentTimeText, currentTime, "Current");
            UpdateTimeText(bestTimeText, BestTime, "Best");

            _ = StartCoroutine(TimeCounter());
        }

        public void OnFinishReached()
        {
            isCompleted = true;
            winScreen.SetActive(true);
            BestTime = currentTime < BestTime ? currentTime : BestTime;

            UpdateTimeText(currentTimeText, currentTime, "Current");
            UpdateTimeText(bestTimeText, BestTime, "Best");
            print("Win");
        }

        private void Unload() => SceneManager.UnloadSceneAsync("rungame");

        private IEnumerator TimeCounter()
        {
            while (!isCompleted)
            {
                currentTime += .01f;
                UpdateTimeText(currentTimeText, currentTime, "Current");
                yield return new WaitForSeconds(.01f);
            }
        }

        private void UpdateTimeText(Text[] timeTexts, float time, string prefix)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            var fullTime = string.Format("{0:D2}m:{1:D2}s:{2:D3}ms",
                timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

            foreach (var timeText in timeTexts)
            {
                timeText.text = $"{prefix} {fullTime}";
            }
        }
    }
}
