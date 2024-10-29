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

    private void UpdateUI()
    {
        if (enemyStaminaText != null)
        {
            enemyStaminaText.text = $"{stats.currentStamina:F0}/{stats.stamina:F0}";
        }

        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"{stats.currentHp:F0}/{stats.hp:F0}";
        }
    }

    public void Rest()
    {
        RegainStamina(20);
    }

    private void RegainStamina(int amount)
    {
        stats.currentStamina = Mathf.Min(stats.currentStamina + amount, stats.stamina);
        UpdateStaminaBar();
    }

    public void ReduceStamina(int amount)
    {
        stats.currentStamina = Mathf.Max(stats.currentStamina - amount, 0);
        UpdateStaminaBar();
    }

    public virtual void TakeDamage(int amount)
    {
        stats.currentHp -= amount;
        stats.currentHp = Mathf.Clamp(stats.currentHp, 0, stats.hp);  // Đảm bảo máu không âm
        UpdateUI();  // Cập nhật giao diện người dùng (nếu có)

        Debug.Log(gameObject.name + " took " + amount + " damage. Current Health: " + stats.currentHp);

        if (stats.currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Người chơi đã chiến thắng");
        SceneManager.LoadScene("Lobby");
    }

    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = stats.currentStamina / stats.stamina;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = stats.currentHp / stats.hp;
        }
    }
}
