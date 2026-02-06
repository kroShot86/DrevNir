using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class GameOverUI : MonoBehaviour
{
    [Header("Элементы UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;

    [Header("Настройки стиля")]
    [SerializeField] private string gameOverMessage = "GAME OVER";
    [SerializeField] private string retryButtonText = "Повторить";

    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        HideGameOver(); 
    }

    private void Start()
    {
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(ReloadScene);

            TextMeshProUGUI buttonText = retryButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
                buttonText.text = retryButtonText;
        }

        if (gameOverText != null)
            gameOverText.text = gameOverMessage;
    }

    public void ShowGameOver()
    {
        // Исправление 3: Убедимся, что панель активна
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("Ошибка: gameOverPanel не привязан в инспекторе!");
        }
    }

    public void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}