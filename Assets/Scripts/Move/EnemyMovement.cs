using UnityEngine;
using DG.Tweening;  // Thêm thư viện DOTween

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveDuration = 0.5f; // Thời gian di chuyển
    public float leftBoundary;
    public float rightBoundary;
    private EnemyStamina enemyStamina;
    private Transform enemyTransform;
    private Animator animator;
    private string triggerWalkLeft = "walk_left";
    private string triggerWalkRight = "walk_right";
    private float lastTriggerTime;

    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);
        enemyStamina = GetComponent<EnemyStamina>();
        enemyTransform = GameObject.FindWithTag("enemy").transform;

        animator = GetComponent<Animator>();
        lastTriggerTime = Time.time;

        // Giới hạn vị trí enemy ban đầu trong phạm vi map
        if (enemyTransform.position.x < leftBoundary)
            enemyTransform.position = new Vector3(leftBoundary + 0.5f, enemyTransform.position.y, enemyTransform.position.z);
        if (enemyTransform.position.x > rightBoundary)
            enemyTransform.position = new Vector3(rightBoundary - 0.5f, enemyTransform.position.y, enemyTransform.position.z);
    }

    public void MoveLeft()
    {
        Debug.Log("MoveLeft called");
        if (Time.time < lastTriggerTime + moveDuration) return;

        if (enemyTransform.position.x - moveDistance >= leftBoundary)
        {
            Debug.Log("Enemy is moving left");
            // Kích hoạt animation đi trái
            animator.SetTrigger(triggerWalkLeft);

            // Di chuyển từ từ sang trái bằng DOTween
            enemyTransform.DOMoveX(enemyTransform.position.x - moveDistance, moveDuration)
                          .SetEase(Ease.Linear)
                          .OnComplete(() =>
                          {
                              // Giảm stamina sau khi di chuyển hoàn tất
                              enemyStamina.ReduceStamina(10f);
                              lastTriggerTime = Time.time;
                          });
        }
    }

    public void MoveRight()
    {
        if (Time.time < lastTriggerTime + moveDuration) return;

        if (enemyTransform.position.x + moveDistance <= rightBoundary)
        {
            // Kích hoạt animation đi phải
            animator.SetTrigger(triggerWalkRight);

            // Di chuyển từ từ sang phải bằng DOTween
            enemyTransform.DOMoveX(enemyTransform.position.x + moveDistance, moveDuration)
                          .SetEase(Ease.Linear)
                          .OnComplete(() =>
                          {
                              // Giảm stamina sau khi di chuyển hoàn tất
                              enemyStamina.ReduceStamina(10f);
                              lastTriggerTime = Time.time;
                          });
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
