using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int defense;
    public int currentHealth;
    public int dodgeChance;
    public Image healthBar;
    public TextMeshProUGUI enemyHealthText;
    public TutorialManager tutorialManager;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (enemyHealthText != null)
        {
            enemyHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Đảm bảo máu không âm
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public bool CanDodge()
    {
        int randomChance = Random.Range(0, 100);
        return randomChance < dodgeChance;
    }

    private void UpdateHealthUI()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        Debug.Log("Enemy is dead!");
        tutorialManager.SetCurrentStep(6);
    }
}
