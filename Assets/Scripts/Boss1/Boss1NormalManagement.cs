using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss1NormalManagement : MonoBehaviour
{
    public BaseBoss baseBoss = new BaseBoss();
    private EnemyMovement movement;
    public Image staminaBar;
    public TextMeshProUGUI enemyStaminaText;
    public Image healthBar;
    public TextMeshProUGUI enemyHealthText;

    private void Start()
    {
        baseBoss.StartBoss1();

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
            enemyStaminaText.text = $"{baseBoss.currentStamina:F0}/{baseBoss.stamina:F0}";
        }

        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"{baseBoss.currentHp:F0}/{baseBoss.hp:F0}";
        }
    }

    public void Rest()
    {
        RegainStamina(20);
    }

    private void RegainStamina(int amount)
    {
        baseBoss.currentStamina = Mathf.Min(baseBoss.currentStamina + amount, baseBoss.stamina);
        UpdateStaminaBar();
    }
    public virtual void TakeDamage(int amount)
    {
        baseBoss.currentHp -= amount;
        baseBoss.currentHp = Mathf.Clamp(baseBoss.currentHp, 0, baseBoss.hp);  // Đảm bảo máu không âm
        UpdateUI();  // Cập nhật giao diện người dùng (nếu có)

        Debug.Log(gameObject.name + " took " + amount + " damage. Current Health: " + baseBoss.currentHp);

        if (baseBoss.currentHp <= 0)
        {
            //Die();
        }
    }
    public void ReduceStamina(int amount)
    {
        baseBoss.currentStamina = Mathf.Max(baseBoss.currentStamina - amount, 0);
        UpdateStaminaBar();
    }
    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = baseBoss.currentStamina / baseBoss.stamina;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = baseBoss.currentHp / baseBoss.hp;
        }
    }
}
