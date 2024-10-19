using UnityEngine;
using System.IO;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage;
    public float attackRange = 1.7f;
    public PlayerStamina playerStamina;
    public EnemyHealth enemyHealth;
    private Transform enemyTransform;
    private Animator animator;
    private TutorialManager tutorialManager;

    private string saveFilePath;

    private void Start()
    {
        // Tìm đường dẫn file JSON để load chỉ số
        string directoryPath = Application.persistentDataPath + "/DB";
        saveFilePath = directoryPath + "/playerData.json";

        // Load dữ liệu của nhân vật
        LoadPlayerData();
        animator = GetComponent<Animator>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        if (enemyHealth != null)
        {
            enemyTransform = enemyHealth.transform;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, enemyTransform.position);
        if (distance <= attackRange && tutorialManager.GetCurrentStep() == 3)
        {
            tutorialManager.SetCurrentStep(4);
        }

        if (enemyHealth != null && enemyHealth.currentHealth <= enemyHealth.maxHealth * 0.2f && tutorialManager.GetCurrentStep() == 4)
        {
            tutorialManager.SetCurrentStep(5);
        }
    }

    // Hàm chung cho các loại tấn công
    private void PerformAttack(string animationTrigger, int baseDamage, int staminaCost)
    {
        if (enemyHealth != null && playerStamina.currentStamina > 0)
        {
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            if (distance <= attackRange)
            {
                // Kích hoạt animation
                animator.SetTrigger(animationTrigger);

                // Kiểm tra né tránh của kẻ địch
                if (!enemyHealth.CanDodge())
                {
                    // Tính toán sát thương
                    int damageDealt = CalculateDamage(baseDamage);
                    enemyHealth.TakeDamage(damageDealt);
                    Debug.Log($"Player attacked the enemy for {damageDealt} damage!");
                }
                else
                {
                    Debug.Log("Enemy dodged the attack!");
                }

                // Trừ stamina sau khi tấn công
                playerStamina.ReduceStamina(staminaCost);
            }
            else
            {
                Debug.Log("Player attempted to attack, but is too far away.");
            }
        }
        else
        {
            Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
            playerStamina.RegainStamina(20);
        }
    }

    // Hàm tính toán sát thương (bao gồm cả trừ phòng thủ của kẻ địch)
    private int CalculateDamage(int baseDamage)
    {
        return Mathf.Max(baseDamage + attackDamage - enemyHealth.defense, 0);
    }

    // Các loại tấn công khác nhau
    public void AttackWeak()
    {
        PerformAttack("Cut_Right", 10, 10);
    }

    public void AttackNormal()
    {
        PerformAttack("CutNormal", 15, 15);
    }

    public void AttackStrong()
    {
        PerformAttack("StrongCut", 20, 20);
    }

    // Hàm load dữ liệu của nhân vật từ file JSON
    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Tải tất cả chỉ số từ file
            attackDamage = 10 + data.attack;

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
