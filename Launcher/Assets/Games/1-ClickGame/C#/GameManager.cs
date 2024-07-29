using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Launcher.ClickGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int score;
        [Header("UI elements")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button clickBtn;

        private void Awake()
        {
            LoadOrCreateScoreData();
            menuBtn.onClick.AddListener(() => Unload());
            clickBtn.onClick.AddListener(() => OnClick());
            UpdateScoreText();
        }

        private void OnClick()
        {
            score++;
            PlayerPrefs.SetInt("Score", score);
            UpdateScoreText();
        }

        private void Unload() => SceneManager.UnloadSceneAsync("clickgame");

        private void UpdateScoreText() => scoreText.text = $"{score}";

        private void LoadOrCreateScoreData()
        {
            if (!PlayerPrefs.HasKey("Score"))
            {
                PlayerPrefs.SetInt("Score", 0);
            }

            score = PlayerPrefs.GetInt("Score");
        }
    }
}
