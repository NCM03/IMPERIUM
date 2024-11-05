using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyNormalManagement : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    private EnemyMovement movement;
    public Image staminaBar;
    public TextMeshProUGUI enemyStaminaText;
    public Image healthBar;
    public TextMeshProUGUI enemyHealthText;
    public Image lose;

    private void Start()
    {
        stats.AssignRandomStrength();

        // Thêm thành phần EnemyMovement vào GameObject
        movement = gameObject.AddComponent<EnemyMovement>();
        UpdateHealthBar();
        UpdateStaminaBar();

        if (staminaBar == null || enemyStaminaText == null || healthBar == null || enemyHealthText == null)
        {
            Debug.LogWarning("Một trong các thành phần UI không được thiết lập trong Inspector.");
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    public void ReloadScene()
    {
        // Reset lại scene hiện tại bằng cách load lại chính nó
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateUI()
    {
        if (enemyStaminaText != null)
        {
            enemyStaminaText.text = $"{stats.currentStamina:F0}/{stats.stamina:F0}";
        }

        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"{stats.currentHp:F0}/{stats.hp:F0}";
        }
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    public void Rest()
    {
        RegainStamina(20);
    }

    private void RegainStamina(int amount)
    {
        stats.currentStamina = Mathf.Min(stats.currentStamina + amount, stats.stamina);
        Debug.Log("After resting, Current Stamina: " + stats.currentStamina);
        UpdateUI();  // Cập nhật giao diện người dùng
    }
    public void ReduceStamina(int amount)
    {
        stats.currentStamina = Mathf.Max(stats.currentStamina - amount, 0);
        UpdateUI();  // Cập nhật giao diện người dùng
    }

    public virtual void TakeDamage(int amount)
    {
        stats.currentHp -= amount;
        stats.currentHp = Mathf.Clamp(stats.currentHp, 0, stats.hp);  // Đảm bảo máu không âm
        Debug.Log("After taking damage, Current HP: " + stats.currentHp);
        UpdateUI();  // Cập nhật giao diện người dùng
        if (stats.currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        lose.gameObject.SetActive(true);
    }

    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = (float)stats.currentStamina / stats.stamina;
            Debug.Log("Stamina Bar fillAmount: " + staminaBar.fillAmount);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)stats.currentHp / stats.hp;
            Debug.Log("Health Bar fillAmount: " + healthBar.fillAmount);
        }
    }
}
