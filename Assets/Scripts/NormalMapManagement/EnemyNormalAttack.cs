using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNormalAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Khoảng cách để tấn công
    private EnemyStats enemyStats = new EnemyStats();
    public PlayerHealth playerHealth;

    public EnemyNormalManagement enemyNormalManagement;
    private Transform playerTransform;
    private Animator animator;
    private string triggerAttackWeak = "BossLowCut";
    private string triggerAttackNormal = "BossNormalCut";
    private string triggerAttackStrong = "BossStrongCut";

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Kiểm tra xem Player đã được gán chưa
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
    }

    // Hàm tấn công yếu
    public void AttackWeak()
    {
        if (playerHealth != null && enemyNormalManagement.stats.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackWeak);
            if (!enemyStats.CanDodge(enemyStats.attack))
            {
                int damageDealt = Mathf.Max(enemyStats.attack - enemyStats.defense, 0);
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Enemy performed a Weak Attack and dealt " + (damageDealt) + " damage!");
            }
            else
            {
                Debug.Log("Player dodged the attack!");
            }
            enemyNormalManagement.Rest();
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            enemyNormalManagement.Rest();
        }
    }

    // Hàm tấn công thường
    public void AttackNormal()
    {
        if (playerHealth != null && enemyNormalManagement.stats.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackNormal);
            if (!enemyStats.CanDodge(enemyStats.attack))
            {
                int damageDealt = Mathf.Max(enemyStats.attack - enemyStats.defense, 0);
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Enemy performed a Normal Attack and dealt " + (damageDealt) + " damage!");
            }
            else
            {
                Debug.Log("Player dodged the attack!");
            }
            enemyNormalManagement.Rest();
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            enemyNormalManagement.Rest();
        }
    }

    // Hàm tấn công mạnh
    public void AttackStrong()
    {
        if (playerHealth != null && enemyNormalManagement.stats.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackStrong);
            if (!enemyStats.CanDodge(enemyStats.attack))
            {
                int damageDealt = Mathf.Max(enemyStats.attack - enemyStats.defense, 0);
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Enemy performed a Strong Attack and dealt " + (damageDealt) + " damage!");
            }
            else
            {
                Debug.Log("Player dodged the attack!");
            }
            enemyNormalManagement.Rest();
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            enemyNormalManagement.Rest();
        }
    }

}
