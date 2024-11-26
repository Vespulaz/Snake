using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private bool isPaused = false;

    private void Start()
    {
        // Kiểm tra GameManager
        if (!GameManager.HasInstance())
        {
            Debug.LogError("GameManager không tồn tại trong scene!");
            // Tạo GameManager nếu cần
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }

        // Setup pause panel
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        // Setup buttons
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(() => {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ReturnToMainMenu();
                }
            });
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    private void Update()
    {
        // Chỉ cho phép pause khi game chưa kết thúc
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
        
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnDestroy()
    {
        // Cleanup button listeners
        if (resumeButton != null)
            resumeButton.onClick.RemoveAllListeners();
        
        if (mainMenuButton != null)
            mainMenuButton.onClick.RemoveAllListeners();
        
        if (quitButton != null)
            quitButton.onClick.RemoveAllListeners();
    }
}
