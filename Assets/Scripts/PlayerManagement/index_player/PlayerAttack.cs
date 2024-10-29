using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage;  // Sát thương của Player (sẽ được load từ dữ liệu)
    public float attackRange = 2.0f; // Khoảng cách để tấn công
    public PlayerStamina playerStamina;
    public EnemyTutorialHealth enemyHealth;
    private Transform enemyTransform;
    private Animator animator;
    private string triggerAttackWeak = "Cut_Right";
    private string triggerAttackNormal = "CutNormal";
    private string triggerAttackStrong = "StrongCut";
    private TutorialManager tutorialManager;
    private EnemyStats enemyStats = new EnemyStats();
    private EnemyNormalManagement enemyNormalManagement;
    private BaseBoss BaseBoss = new BaseBoss();
    private Boss1NormalManagement boss1NormalManagement;

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
        enemyHealth = FindObjectOfType<EnemyTutorialHealth>();
        playerStamina = FindObjectOfType<PlayerStamina>();
        enemyNormalManagement = FindObjectOfType<EnemyNormalManagement>();
        boss1NormalManagement = FindObjectOfType<Boss1NormalManagement>();
        enemyTransform = GameObject.FindWithTag("enemy").transform;
        // Kiểm tra xem Enemy đã được gán chưa
        if (enemyHealth != null)
        {
            enemyTransform = enemyHealth.transform;
        }
    }

    private void Update()
    {
        if (enemyTransform != null && this.gameObject.scene.name == "TurtorialMap")
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
    }


    // Hàm tấn công yếu
    public void AttackWeak()
    {
        if(this.gameObject.scene.name == "TurtorialMap")
        {
            if (enemyHealth != null && playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackWeak);
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

                // Trừ stamina sau khi tấn công
                playerStamina.ReduceStamina(10);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if(this.gameObject.scene.name == "NormalMap")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackWeak);
                // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                if (!enemyStats.CanDodge(attackDamage))
                {
                    // Kẻ địch không né được, tiến hành tấn công
                    Debug.Log(attackDamage);
                    Debug.Log(enemyNormalManagement.stats.defense);
                    int damageDealt = Mathf.Max(attackDamage - enemyNormalManagement.stats.defense, 0); // Trừ phòng thủ của địch
                    enemyNormalManagement.TakeDamage(10 + damageDealt);
                    Debug.Log("Player attacked the enemy for " + (10 + damageDealt) + " damage!");
                }
                else
                {
                    Debug.Log("Enemy dodged the attack!");
                }

                // Trừ stamina sau khi tấn công
                playerStamina.ReduceStamina(10);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss1")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackWeak);
                // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                if (!enemyStats.CanDodge(attackDamage))
                {
                    // Kẻ địch không né được, tiến hành tấn công
                    Debug.Log(attackDamage);
                    Debug.Log(boss1NormalManagement.baseBoss.defense);
                    int damageDealt = Mathf.Max(attackDamage - boss1NormalManagement.baseBoss.defense, 0); // Trừ phòng thủ của địch
                    boss1NormalManagement.TakeDamage(10 + damageDealt);
                    Debug.Log("Player attacked the enemy for " + (10 + damageDealt) + " damage!");
                }
                else
                {
                    Debug.Log("Enemy dodged the attack!");
                }

                // Trừ stamina sau khi tấn công
                playerStamina.ReduceStamina(10);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
    }

    //Hàm tấn công thường
    public void AttackNormal()
    {
        if (enemyHealth != null && playerStamina.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackNormal);
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

            // Trừ stamina sau khi tấn công
            playerStamina.ReduceStamina(15);
        }
        else
        {
            Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
            playerStamina.RegainStamina(20);
        }
    }

    //Hàm tấn công Strong
    public void AttackStrong()
    {
        if (enemyHealth != null && playerStamina.currentStamina > 0)
        {

            animator.SetTrigger(triggerAttackStrong);
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

            // Trừ stamina sau khi tấn công
            playerStamina.ReduceStamina(20);
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