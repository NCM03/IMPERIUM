using UnityEngine;
using UnityEngine.UI;

public class EnemyStamina : MonoBehaviour
{
    public Image staminaBar;  // Tham chiếu đến thanh thể lực UI
    public float maxStamina = 100f; // Thể lực tối đa
    public float currentStamina;   // Thể lực hiện tại

    void Start()
    {
        currentStamina = maxStamina; // Đặt thể lực ban đầu là tối đa
        UpdateStaminaBar(); // Cập nhật giao diện thể lực
    }

    // Giảm thể lực
    public void ReduceStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0f)
        {
            currentStamina = 0f;
        }
        UpdateStaminaBar();
    }

    // Hồi thể lực
    public void RegainStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        UpdateStaminaBar();
    }

    // Cập nhật thanh thể lực
    private void UpdateStaminaBar()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }
}
