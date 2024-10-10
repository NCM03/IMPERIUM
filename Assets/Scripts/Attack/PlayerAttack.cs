using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20;  // Sát thương của Player
    public float attackRange = 2.0f; // Khoảng cách để tấn công
    public EnemyHealth enemyHealth;  // Tham chiếu đến sức khỏe của Enemy
    public PlayerStamina playerStamina;
    private Transform enemyTransform;

    private void Start()
    {
        if (enemyHealth != null)
        {
            enemyTransform = enemyHealth.transform;
        }
    }

    public void Attack()
    {
        if (enemyHealth != null && playerStamina.currentStamina > 0)
        {
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            if (distance <= attackRange)
            {
                // Nếu đứng gần đủ, tấn công và trừ máu
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log("Player attacked the enemy!");
            }
            else
            {
                Debug.Log("Player attempted to attack, but is too far away.");
            }
            playerStamina.ReduceStamina(15f);
        }
        else
        {
            Debug.Log("Không đủ mana, tự động nghỉ ngơi");
            playerStamina.RegainStamina(20f);
        }
    }
}
