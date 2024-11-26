using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Start()
    {
        // Thêm listeners cho buttons
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() => {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RestartGame();
                }
            });
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

        // Ẩn panel khi bắt đầu
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Cập nhật điểm số khi panel được hiện
        if (finalScoreText != null && GameManager.Instance != null)
        {
            finalScoreText.text = "Score: " + GameManager.Instance.GetScore().ToString();
        }
    }
}