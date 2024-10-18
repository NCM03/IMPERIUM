using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float leftBoundary;
    public float rightBoundary;
    private EnemyStamina enemyStamina;
    private Transform EnemyTrans;
    private Animator animator;
    private string triggerWalkLeft = "walk_left";
    private string triggerWalkRight = "walk_right";
    private float animDuration = 0.5f;
    private float lastTriggerTime;

    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);
        enemyStamina = GetComponent<EnemyStamina>();
        EnemyTrans = GameObject.FindWithTag("enemy").transform;

        animator = GetComponent<Animator>();
        lastTriggerTime = Time.time;

        // Giới hạn vị trí enemy ban đầu trong phạm vi map
        if (EnemyTrans.transform.position.x < leftBoundary)
            EnemyTrans.transform.position = new Vector3(leftBoundary + 0.5f, EnemyTrans.transform.position.y,  EnemyTrans.transform.position.z);
        if (EnemyTrans.transform.position.x > rightBoundary)
            EnemyTrans.transform.position = new Vector3(rightBoundary - 0.5f, EnemyTrans.transform.position.y, transform.position.z);
    }

    public void MoveLeft()
    {
        Debug.Log("MoveLeft called");
        if (EnemyTrans.transform.position.x - moveDistance >= leftBoundary)
        {
            Debug.Log("Enemy is moving left");
            EnemyTrans.transform.position -= new Vector3(moveDistance, 0, 0);
            animator.SetTrigger(triggerWalkLeft);
            enemyStamina.ReduceStamina(10f);
        }
    }


    public void MoveRight()
    {
        if (EnemyTrans.transform.position.x + moveDistance <= rightBoundary)
        {
            EnemyTrans.transform.position += new Vector3(moveDistance, 0, 0);
            animator.SetTrigger(triggerWalkRight);
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
