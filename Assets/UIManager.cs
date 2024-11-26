using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        UpdateScore(0);
        UpdateHighScore();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public void UpdateHighScore()
    {
        if (highScoreText != null)
            highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
    
    private void Update()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.GetScore().ToString();
        }
    }
}