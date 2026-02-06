//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerHealth : MonoBehaviour
//{
//    [Header("Настройки здоровья")]
//    [SerializeField] private int maxHealth = 100;
//    private int currentHealth;

//    [Header("UI")]
//    [SerializeField] private Slider healthSlider;
//    [SerializeField] private GameOverUI gameOverUI;

//    private void Start()
//    {
//        currentHealth = maxHealth;
//        UpdateHealthUI();
//    }

//    public void TakeDamage(int damage)
//    {
//        currentHealth -= damage;

//        if (currentHealth < 0) currentHealth = 0;

//        UpdateHealthUI();

//        if (currentHealth <= 0)
//        {
//            Die();
//        }
//    }

//    private void UpdateHealthUI()
//    {
//        if (healthSlider != null)
//        {
//            healthSlider.maxValue = maxHealth;
//            healthSlider.value = currentHealth;
//        }
//    }

//    private void Die()
//    {
//        Debug.Log("Game Over");
//        if (gameOverUI != null)
//        {
//            gameOverUI.ShowGameOver();
//        }
//        else
//        {
//            Debug.LogWarning("GameOverUI не найден.");
//        }
//    }
//}


using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Настройки")]
    public float maxHealth = 100;
    public float currentHealth;
    public Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0) currentHealth = 0;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (GameOverUI.Instance != null)
        {
            GameOverUI.Instance.ShowGameOver();
        }
        else
        {
            Debug.LogError("Ошибка: GameOverUI не найден! Проверьте привязку.");
        }

        // Теперь деактивируем игрока
        gameObject.SetActive(false);
    }
}