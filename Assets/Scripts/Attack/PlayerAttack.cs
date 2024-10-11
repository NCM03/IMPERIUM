using UnityEngine;
using System.IO;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage;  // Sát thương của Player (sẽ được load từ dữ liệu)
    //public int defense;       // Phòng thủ của Player (sẽ được load từ dữ liệu)
    //public int maxHP;         // Máu tối đa của Player (sẽ được load từ dữ liệu)
    //public int currentHP;     // Máu hiện tại của Player
    //public int maxStamina;    // Stamina tối đa của Player (sẽ được load từ dữ liệu)
    //public int currentStamina; // Stamina hiện tại của Player
    //public int dodgeChance;   // Tỉ lệ né tránh (sẽ được load từ dữ liệu)

    public float attackRange = 2.0f; // Khoảng cách để tấn công
    public PlayerStamina playerStamina;
    public EnemyHealth enemyHealth;  // Tham chiếu đến sức khỏe của Enemy
    private Transform enemyTransform;

    private string saveFilePath;

    private void Start()
    {
        // Tìm đường dẫn file JSON để load chỉ số
        string directoryPath = Application.persistentDataPath + "/DB";
        saveFilePath = directoryPath + "/playerData.json";

        // Load dữ liệu của nhân vật
        LoadPlayerData();

        // Kiểm tra xem Enemy đã được gán chưa
        if (enemyHealth != null)
        {
            enemyTransform = enemyHealth.transform;
        }
    }

    // Hàm tấn công
    public void Attack()
    {
        if (enemyHealth != null && playerStamina.currentStamina > 0)
        {
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            if (distance <= attackRange)
            {
                // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                if (!enemyHealth.CanDodge())
                {
                    // Kẻ địch không né được, tiến hành tấn công
                    int damageDealt = Mathf.Max(attackDamage - enemyHealth.defense, 0); // Trừ phòng thủ của địch
                    enemyHealth.TakeDamage(10 + damageDealt);
                    Debug.Log("Player attacked the enemy for " + (10 + damageDealt) + " damage!");
                }
                else
                {
                    Debug.Log("Enemy dodged the attack!");
                }
            }
            else
            {
                Debug.Log("Player attempted to attack, but is too far away.");
            }

            // Trừ stamina sau khi tấn công
            playerStamina.ReduceStamina(15);
        }
        else
        {
            Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
            playerStamina.RegainStamina(20);
        }
    }

    // Hàm load dữ liệu của nhân vật từ file JSON
    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Tải tất cả chỉ số từ file
            attackDamage = data.attack;

            Debug.Log("Dữ liệu nhân vật đã được load thành công!");
        }
        else
        {
            Debug.LogError("Không tìm thấy file lưu trữ tại: " + saveFilePath);
        }
    }

    // Cấu trúc dữ liệu để lưu trữ và load chỉ số
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int attack;
        public int defense;
        public int hp;
        public int stamina;
        public int dodge;
        public int skillPoints;
    }
}