using UnityEngine;

public class EnemyTutorialAttack : MonoBehaviour
{
    public int attackDamage = 10;  // Sát thương của Enemy
    public float attackRange = 1.7f;
    public PlayerHealth playerHealth;  // Tham chiếu đến sức khỏe của Player
    public EnemyStamina enemyStamina;

    private Transform playerTransform;

    private void Start()
    {
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
    }

    public void Attack()
    {
        // Kiểm tra stamina trước khi thực hiện tấn công
        if (enemyStamina.currentStamina <= 0)
        {
            Debug.Log("Enemy does not have enough stamina to attack.");
            RestOrRechargeStamina();
            return;
        }

        // Kiểm tra khoảng cách trước khi tấn công
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= attackRange)
        {
            PerformAttack();
        }
        else
        {
            Debug.Log("Enemy attempted to attack, but is too far away.");
        }
    }

    // Hàm thực hiện tấn công
    private void PerformAttack()
    {
        if (playerHealth != null)
        {
            // Tính toán sát thương và tấn công người chơi
            playerHealth.TakeDamage(CalculateDamage());
            Debug.Log("Enemy attacked the player for " + attackDamage + " damage!");

            // Trừ stamina sau khi tấn công
            enemyStamina.ReduceStamina(15);
        }
    }

    // Hàm tính sát thương
    private int CalculateDamage()
    {
        return attackDamage; // Bạn có thể thêm logic tính toán sát thương dựa trên chỉ số của Enemy và Player.
    }

    // Hàm để nghỉ ngơi hoặc hồi stamina nếu không đủ stamina để tấn công
    private void RestOrRechargeStamina()
    {
        Debug.Log("Enemy is resting to regain stamina.");
        enemyStamina.RegainStamina(20);  // Hồi phục stamina (hoặc bạn có thể tạo cơ chế khác như 'rest').
    }
}
