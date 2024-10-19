using UnityEngine;

public abstract class BossBase : MonoBehaviour
{
    public int attackDamage;  // Sát thương cơ bản của Boss
    public float attackRange;  // Phạm vi tấn công
    public int health;  // Máu của Boss
    public int stamina;  // Stamina của Boss
    public float dodgeChance;  // Khả năng né tránh (%)

    public PlayerHealth playerHealth;  // Tham chiếu đến PlayerHealth
    protected Transform playerTransform;  // Vị trí của người chơi

    protected virtual void Start()
    {
        // Gán vị trí của player
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
    }

    // Phương thức tấn công chung cho các boss
    public virtual void Attack()
    {
        if (playerHealth != null && stamina > 0)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log(gameObject.name + " attacked the player!");
            }
            else
            {
                Debug.Log(gameObject.name + " is too far away to attack.");
            }
            stamina -= 15;  // Trừ stamina sau khi tấn công
        }
        else
        {
            Debug.Log(gameObject.name + " does not have enough stamina to attack.");
        }
    }


    // Phương thức nhận sát thương
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health left: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Phương thức kiểm tra khả năng né tránh
    public virtual bool CanDodge()
    {
        return Random.value < dodgeChance;
    }

    // Phương thức chết, để các class con có thể tùy chỉnh
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // Logic để xử lý cái chết, ví dụ như phá hủy đối tượng
        Destroy(gameObject);
    }
}
