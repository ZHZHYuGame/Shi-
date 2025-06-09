using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;

    private int highScore;
    private const string HIGH_SCORE_KEY = "HighScore2048";

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        highScoreText.text = "High Score: " + highScore;

        GridManager.Instance.OnScoreUpdated += UpdateScore;
        GridManager.Instance.OnGameOver += ShowGameOver;
        GridManager.Instance.OnGameWon += ShowGameWon;

        restartButton.onClick.AddListener(GridManager.Instance.RestartGame);
        continueButton.onClick.AddListener(HideGameWon);

        UpdateScore(0);
        HideGameOver();
        HideGameWon();
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;

        if (newScore > highScore)
        {
            highScore = newScore;
            highScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        }
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void HideGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    private void ShowGameWon()
    {
        gameWonPanel.SetActive(true);
    }

    private void HideGameWon()
    {
        gameWonPanel.SetActive(false);
    }
}