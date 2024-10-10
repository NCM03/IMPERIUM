using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float leftBoundary;
    public float rightBoundary;
    private Animator animator;
    private string triggerWalkLeft = "walk_left";
    private string triggerWalkRight = "walk_right";
    private PlayerStamina playerStamina;
    private float animDuration = 0.5f;
    private float lastTriggerTime;
    public GameObject player;  // Đối tượng nhân vật
    public TurnManager turnManager;

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
        if (Time.time < lastTriggerTime + animDuration) return;

        if (player.transform.position.x + moveDistance <= rightBoundary && playerStamina.currentStamina > 0)
        {
            transform.position += new Vector3(moveDistance, 0, 0);
            animator.SetTrigger(triggerWalkRight);
            playerStamina.ReduceStamina(10f);
            lastTriggerTime = Time.time;
            turnManager.EndPlayerTurn();
        }
    }

    public void MoveBackward()
    {
        if (Time.time < lastTriggerTime + animDuration) return;

        if (player.transform.position.x - moveDistance >= leftBoundary && playerStamina.currentStamina > 0)
        {
            transform.position -= new Vector3(moveDistance, 0, 0);
            animator.SetTrigger(triggerWalkLeft);
            playerStamina.ReduceStamina(10f);
            lastTriggerTime = Time.time;
            turnManager.EndPlayerTurn();
        }
    }
}
