using UnityEngine;
using DG.Tweening;  // Thêm thư viện DOTween

public class PlayerMovementV2 : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveDuration = 0.5f; // Thời gian di chuyển
    public float leftBoundary;
    public float rightBoundary;
    private Animator animator;
    private string triggerWalkLeft = "walk_left";
    private string triggerWalkRight = "walk_right";
    private PlayerStamina playerStamina;
    private float lastTriggerTime;
    public GameObject player;  // Đối tượng nhân vật
    public TurnManager turnManager;
    public Transform enemyTransform;

    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);

        if (player.transform.position.x < leftBoundary)
            player.transform.position = new Vector3(leftBoundary + 0.5f, player.transform.position.y, player.transform.position.z);
        if (player.transform.position.x > rightBoundary)
            player.transform.position = new Vector3(rightBoundary - 0.5f, player.transform.position.y, player.transform.position.z);

        animator = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
        lastTriggerTime = Time.time;
       
    }
    
    

    public void MoveForward()
    {
        if (Time.time < lastTriggerTime + moveDuration) return;

        if (player.transform.position.x + moveDistance <= rightBoundary && playerStamina.currentStamina > 0)
        {
            // Kích hoạt animation đi phải
            animator.SetTrigger(triggerWalkRight);

            // Di chuyển từ từ bằng DOTween
            transform.DOMoveX(transform.position.x + moveDistance, moveDuration)
                     .SetEase(Ease.Linear)
                     .OnComplete(() =>
                     {
                         // Hoàn thành di chuyển, cập nhật thông tin
                         playerStamina.ReduceStamina(10f);
                         lastTriggerTime = Time.time;
                         turnManager.EndPlayerTurn();
                     });

            // Tính toán và in ra khoảng cách đến kẻ thù
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            Debug.Log("Distance to enemy: " + distance);
        }
    }

    public void MoveBackward()
    {
        if (Time.time < lastTriggerTime + moveDuration) return;

        if (player.transform.position.x - moveDistance >= leftBoundary && playerStamina.currentStamina > 0)
        {
            // Kích hoạt animation đi trái
            animator.SetTrigger(triggerWalkLeft);

            // Di chuyển từ từ bằng DOTween
            transform.DOMoveX(transform.position.x - moveDistance, moveDuration)
                     .SetEase(Ease.Linear)
                     .OnComplete(() =>
                     {
                         // Hoàn thành di chuyển, cập nhật thông tin
                         playerStamina.ReduceStamina(10f);
                         lastTriggerTime = Time.time;
                         turnManager.EndPlayerTurn();
                     });
        }
    }
}
