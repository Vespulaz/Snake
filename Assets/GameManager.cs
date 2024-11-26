using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Snake snakeController; // Reference tới Snake controller
    [SerializeField] private GameObject gameOverPanel;  // Reference tới Game Over Panel
    
    public bool isGameOver { get; private set; }
    private int score = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public static bool HasInstance()
    {
        return Instance != null;
    }

    public void InitializeGame()
    {
        score = 0;
        isGameOver = false;
        Time.timeScale = 1f;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        
        if (snakeController != null)
        {
            snakeController.EnableSnake();
        }
    }

    public void GameOver()
    {
        if (Instance == null) return;
        
        isGameOver = true;
        Time.timeScale = 0f;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (snakeController != null)
        {
            snakeController.DisableSnake();
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        score = 0;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (snakeController != null)
        {
            snakeController.ResetState(); // Gọi trực tiếp ResetState thay vì EnableSnake
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        ResetScore();
        SceneManager.LoadScene(0);
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }
}