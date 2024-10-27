using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;  // Sát thương của Enemy
    public float attackRange = 2.0f;
    public PlayerHealth playerHealth;  // Tham chiếu đến sức khỏe của Player
    public EnemyStamina enemyStamina;
    private Animator animator;
    private Transform playerTransform;
    private string triggerAttackWeak = "BossNormalCut";
    private void Start()
    {
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (playerHealth != null && enemyStamina.currentStamina > 0)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                animator.SetTrigger(triggerAttackWeak);
                // Nếu đứng gần đủ, tấn công và trừ máu
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy attacked the player!");
            }
            else
            {
                Debug.Log("Enemy attempted to attack, but is too far away.");
            }
            enemyStamina.ReduceStamina(15);
        }
        else
        {
            Debug.Log("Enemy does not have enough stamina to attack.");
            enemyStamina.ReduceStamina(20);
        }
    }
}
