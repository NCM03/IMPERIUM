using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PowerBoss1 : MonoBehaviour
{
    public float attackRange = 2.0f; // Khoảng cách để tấn công
    public PlayerHealth playerHealth;

    public Boss1NormalManagement boss1NormalManagement;
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

        if (boss1NormalManagement != null)
        {
            boss1NormalManagement.baseBoss.StartBoss1();
            boss1NormalManagement.UpdateUI(); // Gọi UpdateUI qua boss1NormalManagement
        }
    }

    // Hàm tấn công yếu
    public void AttackWeak()
    {
        if (playerHealth != null && boss1NormalManagement.baseBoss.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackWeak);
            if (!boss1NormalManagement.baseBoss.CanDodge(boss1NormalManagement.baseBoss.attack))
            {
                int damageDealt = Mathf.Max(boss1NormalManagement.baseBoss.attack - boss1NormalManagement.baseBoss.defense, 0);
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Enemy performed a Weak Attack and dealt " + (damageDealt) + " damage!");
            }
            else
            {
                Debug.Log("Player dodged the attack!");
            }
            boss1NormalManagement.Rest();
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            boss1NormalManagement.Rest();
        }
    }

    // Hàm tấn công thường
    public void AttackNormal()
    {
        if (playerHealth != null && boss1NormalManagement.baseBoss.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackNormal);
            if (!boss1NormalManagement.baseBoss.CanDodge(boss1NormalManagement.baseBoss.attack))
            {
                int damageDealt = Mathf.Max(boss1NormalManagement.baseBoss.attack - boss1NormalManagement.baseBoss.defense, 0);
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Enemy performed a Normal Attack and dealt " + (damageDealt) + " damage!");
            }
            else
            {
                Debug.Log("Player dodged the attack!");
            }
            boss1NormalManagement.Rest();
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            boss1NormalManagement.Rest();
        }
    }

    // Hàm tấn công mạnh
    public void AttackStrong()
    {
        if (playerHealth != null && boss1NormalManagement.baseBoss.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackStrong);
            if (!boss1NormalManagement.baseBoss.CanDodge(boss1NormalManagement.baseBoss.attack))
            {
                int damageDealt = Mathf.Max(boss1NormalManagement.baseBoss.attack - boss1NormalManagement.baseBoss.defense, 0);
                playerHealth.TakeDamage(damageDealt);
                Debug.Log("Enemy performed a Strong Attack and dealt " + (damageDealt) + " damage!");
            }
            else
            {
                Debug.Log("Player dodged the attack!");
            }
            boss1NormalManagement.Rest();
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            boss1NormalManagement.Rest();
        }
    }
}
