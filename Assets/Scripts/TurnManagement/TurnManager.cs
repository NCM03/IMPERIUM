using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerMovementV2 playerMovement;
    public EnemyTutorialAIController enemyAIController;
    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;
    public bool isPlayerTurn = true;
    private bool isEnemyTurn = false;  // Đảm bảo mỗi lượt chỉ được xử lý một lần


    void Start()
    {
        StartPlayerTurn(); // Bắt đầu với lượt của người chơi
    }

    public bool IsPlayerTurn
    {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }

    void StartPlayerTurn()
    {
        if (CheckGameOver() || isEnemyTurn == true) return; // Kiểm tra nếu đang xử lý lượt hoặc đã GameOver
        isPlayerTurn = true;
        Debug.Log("Player's turn started");
    }
    public void EndPlayerTurn()
    {
        if (CheckGameOver() || isEnemyTurn == true || isPlayerTurn == true) return;
        isPlayerTurn = false;
        isEnemyTurn = true;  // Đặt cờ đang xử lý lượt
        Debug.Log("Player's turn ended, AI's turn starting...");
  
        Invoke(nameof(StartEnemyTurn), 1f); // Chuyển lượt cho AI
        
    }

    void StartEnemyTurn()
    {
        if (CheckGameOver() || isEnemyTurn == false) return; // Kiểm tra nếu đang xử lý lượt hoặc đã GameOver
        Debug.Log("AI's turn started");
        enemyAIController.MakeDecision(); // Gọi hành động của AI
        Invoke(nameof(EndEnemyTurn), 1f); // Sau một khoảng thời gian, kết thúc lượt của AI
    }

    void EndEnemyTurn()
    {
        if (CheckGameOver()) return; // Kiểm tra GameOver sau lượt của AI
        isEnemyTurn = false;  // Đặt lại cờ khi lượt kết thúc
        Debug.Log("AI's turn ended, Player's turn starting...");
        StartPlayerTurn(); // Quay lại lượt của người chơi
    }

    // Kiểm tra điều kiện kết thúc game
    bool CheckGameOver()
    {
        if (playerHealth.currentHealth <= 0)
        {
            Debug.Log("Player is dead. Game Over!");
            return true;
        }
        if (enemyHealth.currentHealth <= 0)
        {
            Debug.Log("Enemy is dead. Game Over!");
            return true;
        }
        return false;
    }
}
