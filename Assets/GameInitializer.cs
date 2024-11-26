using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitializeGame();
        }
        else
        {
            Debug.LogError("GameManager không tồn tại! Tạo mới GameManager...");
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
            GameManager.Instance.InitializeGame();
        }
    }
}