using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public Button attackButton;
    public Button restButton;
    private TurnManager turnManager;
    private PlayerStamina playerStamina;
    private PlayerMovementV2 playerMovement;
    private PlayerAttack playerAttack;
    private Transform playerTransform;
    private EnemyMovement enemyMovement;
    public float leftBoundary;
    public float rightBoundary;
    public float Vitrikedich = 1f;


    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);

        turnManager = FindObjectOfType<TurnManager>();
        playerMovement = FindObjectOfType<PlayerMovementV2>();
        playerStamina = playerMovement.GetComponent<PlayerStamina>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerTransform = playerMovement.transform;
        enemyMovement = FindObjectOfType<EnemyMovement>();


        moveForwardButton.onClick.AddListener(() => {
            playerMovement.MoveForward();
            turnManager.EndPlayerTurn();
        });

        moveBackwardButton.onClick.AddListener(() => {
            playerMovement.MoveBackward();
            turnManager.EndPlayerTurn();
        });

        attackButton.onClick.AddListener(() => {
            playerAttack.Attack();
            turnManager.EndPlayerTurn();
        });

        restButton.onClick.AddListener(() => {
            playerStamina.RegainStamina(20f);
            turnManager.EndPlayerTurn();
        });
    }

    void Update()
    {
        bool isPlayerTurn = turnManager.IsPlayerTurn();
        float distanceToEnemy = Vector3.Distance(playerTransform.position, enemyMovement.transform.position);
        bool hasEnoughStamina = playerStamina.currentStamina >= 10;
        bool canMoveForward = distanceToEnemy > Vitrikedich;
        bool canMoveBackward = playerTransform.position.x > leftBoundary;
        moveForwardButton.gameObject.SetActive(isPlayerTurn && canMoveForward && hasEnoughStamina);
        moveBackwardButton.gameObject.SetActive(isPlayerTurn && canMoveBackward && hasEnoughStamina);
        attackButton.gameObject.SetActive(isPlayerTurn && hasEnoughStamina);
        restButton.gameObject.SetActive(isPlayerTurn && (playerStamina.currentStamina < playerStamina.maxStamina / 2 || !hasEnoughStamina));
    }
}
