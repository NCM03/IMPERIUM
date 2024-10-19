using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerMovementV2 playerMovement;
    public EnemyTutorialAIController enemyAIController;
    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;
    private bool isPlayerTurn = true;
    private bool isProcessingTurn = false; // Đảm bảo mỗi lượt chỉ được xử lý một lần

    void Start()
    {
        StartPlayerTurn(); // Bắt đầu với lượt của người chơi
    }

    void StartPlayerTurn()
    {
        if (CheckGameOver() || isProcessingTurn) return; // Kiểm tra game over trước khi bắt đầu lượt
        isPlayerTurn = true;
        isProcessingTurn = false;
        Debug.Log("Player's turn started");
    }

    public void EndPlayerTurn()
    {
        if (CheckGameOver() || isProcessingTurn) return; // Kiểm tra game over sau khi kết thúc lượt

        isPlayerTurn = false;
        isProcessingTurn = true;
        Debug.Log("Player's turn ended, AI's turn starting...");
        Invoke(nameof(StartEnemyTurn), 1f); // Chuyển lượt cho AI
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }


    void StartEnemyTurn()
    {
        if (CheckGameOver() || isProcessingTurn == false) return; // Kiểm tra game over trước khi bắt đầu lượt của AI

        Debug.Log("AI's turn started");
        enemyAIController.MakeDecision(); // Gọi hành động của AI
        Invoke(nameof(EndEnemyTurn), 1f); // Sau một khoảng thời gian, kết thúc lượt của AI
    }

    void EndEnemyTurn()
    {
        if (CheckGameOver()) return; // Kiểm tra game over sau khi kết thúc lượt của AI
        isProcessingTurn = false;
        Debug.Log("AI's turn ended, Player's turn starting...");
        StartPlayerTurn(); // Quay trở lại lượt của người chơi
    }

    // Kiểm tra điều kiện kết thúc game
    bool CheckGameOver()
    {
        if (playerHealth.currentHealth <= 0)
        {
            Debug.Log("Player is dead. Game Over!");
            // Dừng toàn bộ game hoặc chuyển sang màn hình kết thúc
            return true;
        }
        if (enemyHealth.currentHealth <= 0)
        {
            Debug.Log("Enemy is dead. Game Over!");
            // Dừng toàn bộ game hoặc chuyển sang màn hình kết thúc
            return true;
        }
        return false;
    }
}
