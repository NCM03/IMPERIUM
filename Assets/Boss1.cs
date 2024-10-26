using UnityEngine;

public class Boss1 : BossBase
{
    protected override void Start()
    {
        base.Start();
        maxHealth = 200;  // Boss1 có nhiều máu hơn
        maxStamina = 200;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        defense = 15;     // Boss1 có phòng thủ cao hơn
        dodgeChance = 5;  // Boss1 có tỷ lệ né thấp hơn
        attackDamage = 20;  // Boss1 có sát thương cao hơn
        attackRange = 3.0f; // Boss1 có tầm đánh xa hơn
        UpdateHealthUI();  // Gọi hàm để cập nhật UI sau khi khởi tạo
        UpdateStaminaUI();
        Debug.Log("Boss1 Initialized");
    }

    // Boss1-specific special move
    public void SpecialMove()
    {
        Debug.Log("Boss1 used a special move!");
        Defend();  // Gọi phương thức phòng thủ trong SpecialMove
    }
}
