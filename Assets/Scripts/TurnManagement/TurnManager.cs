using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayerMovementV2 playerMovement;
    public EnemyNormalAIController enemyAIController; // Thêm tham chiếu đến EnemyAIController
    private bool isPlayerTurn = true;

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        Invoke("StartEnemyTurn", 1f);
    }

    void StartEnemyTurn()
    {
        enemyAIController.MakeDecision(); // Gọi hàm để quyết định di chuyển của AI
        Invoke("StartPlayerTurn", 1f);
    }

    void StartPlayerTurn()
    {
        isPlayerTurn = true;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
