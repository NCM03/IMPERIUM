using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float leftBoundary;
    public float rightBoundary;
    private EnemyStamina enemyStamina;

    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);
        enemyStamina = GetComponent<EnemyStamina>();

        // Giới hạn vị trí enemy ban đầu trong phạm vi map
        if (transform.position.x < leftBoundary)
            transform.position = new Vector3(leftBoundary + 0.5f, transform.position.y, transform.position.z);
        if (transform.position.x > rightBoundary)
            transform.position = new Vector3(rightBoundary - 0.5f, transform.position.y, transform.position.z);
    }

    public void MoveLeft()
    {
        if (transform.position.x - moveDistance >= leftBoundary)
        {
            transform.position -= new Vector3(moveDistance, 0, 0);
            enemyStamina.ReduceStamina(10f);
        }
    }

    public void MoveRight()
    {
        if (transform.position.x + moveDistance <= rightBoundary)
        {
            transform.position += new Vector3(moveDistance, 0, 0);
            enemyStamina.ReduceStamina(10f);
        }
    }

    public void Rest()
    {
        if (enemyStamina.currentStamina < enemyStamina.maxStamina / 2)
        {
            enemyStamina.RegainStamina(20f);
        }
    }
}
