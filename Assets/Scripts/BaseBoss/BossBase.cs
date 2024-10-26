using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Base class for all enemies/bosses
public class BossBase : MonoBehaviour
{
    // Common stats for all enemies
    public int maxHealth = 100;
    public int currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public int defense = 10;  // Chỉ số phòng thủ
    public int dodgeChance = 10;  // Chỉ số né đòn
    public int attackDamage;  // Chỉ số tấn công
    public float attackRange;  // Tầm tấn công
    public PlayerHealth playerHealth;  // Tham chiếu đến sức khỏe của người chơi
    private Transform playerTransform;
    private Animator animator;  // Animator để chơi animation tấn công
    private string attackTrigger = "Attack";  // Trigger cho hoạt ảnh tấn công

    public Image healthBar;
    public Image staminaBar;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI enemyStaminaText;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        UpdateHealthUI();
        UpdateStaminaUI();

        // Gán đối tượng player và animator
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            UpdateHealthUI();  // Cập nhật lại giao diện nếu cần
        }
        //if (currentHealth <= 0)
        //{
        //    Die();
        //}
    }

    // Update health UI
    protected void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
        if (enemyHealthText != null)
        {
            enemyHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        }
    }

    // Update stamina UI
    protected void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / maxStamina;
        }
        if (enemyStaminaText != null)
        {
            enemyStaminaText.text = currentStamina.ToString("F0") + "/" + maxStamina.ToString("F0");
        }
    }

    // Take damage, considering defense
    public virtual void TakeDamage(int amount)
    {
        int effectiveDamage = Mathf.Max(0, amount - defense);  // Sử dụng chỉ số phòng thủ để giảm sát thương
        currentHealth -= effectiveDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        Debug.Log(gameObject.name + " took " + effectiveDamage + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Defend mechanic: Tăng chỉ số phòng thủ tạm thời
    public virtual void Defend()
    {
        int originalDefense = defense;
        defense += 20;  // Tăng phòng thủ lên tạm thời
        Debug.Log(gameObject.name + " is defending! Defense increased to: " + defense);

        // Sau một khoảng thời gian, reset về phòng thủ gốc (giả định trong game thực tế là sử dụng Coroutine hoặc Timer)
        Invoke("ResetDefense", 5f);  // Giảm phòng thủ về mức bình thường sau 5 giây
    }

    private void ResetDefense()
    {
        defense -= 20;
        Debug.Log(gameObject.name + " has stopped defending. Defense reset to: " + defense);
    }

    // Dodge mechanic
    public bool CanDodge()
    {
        int randomChance = Random.Range(0, 100);
        return randomChance < dodgeChance;
    }

    // Reduce stamina
    public virtual void ReduceStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaUI();
    }

    // Regain stamina
    public virtual void RegainStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaUI();
    }

    // Attack method
    public virtual void Attack()
    {
        if (playerHealth != null && currentStamina > 0)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                if (animator != null)
                {
                    animator.SetTrigger(attackTrigger);  // Chơi hoạt ảnh tấn công nếu có
                }
                playerHealth.TakeDamage(attackDamage);  // Gây sát thương cho người chơi
                Debug.Log(gameObject.name + " attacked the player for " + attackDamage + " damage.");
                ReduceStamina(10);  // Tốn thể lực khi tấn công
            }
            else
            {
                Debug.Log(gameObject.name + " is too far to attack.");
            }
        }
        else
        {
            Debug.Log(gameObject.name + " does not have enough stamina to attack.");
        }
    }

    // Method for enemy death
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }
}
