using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlayerManagement : MonoBehaviour
{
    //Khởi tạo các nút hiển thị UI
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public Button weakAttackButton;
    public Button normalAttackButton;
    public Button strongAttackButton;
    public Button restButton;
    private TurnManager turnManager;
    private PlayerStamina playerStamina;
    private PlayerMovementV2 playerMovement;
    private PlayerAttack playerAttack;

    void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        playerMovement = FindObjectOfType<PlayerMovementV2>();
        playerStamina = playerMovement.GetComponent<PlayerStamina>();
        playerAttack = FindObjectOfType<PlayerAttack>();

        moveForwardButton.onClick.AddListener(() => {
            playerMovement.MoveForward();
            turnManager.EndPlayerTurn();
        });

        moveBackwardButton.onClick.AddListener(() => {
            playerMovement.MoveBackward();
            turnManager.EndPlayerTurn();
        });

        weakAttackButton.onClick.AddListener(() => {
            playerAttack.AttackWeak();
            turnManager.EndPlayerTurn();
        });

        normalAttackButton.onClick.AddListener(() => {
            playerAttack.AttackNormal();
            turnManager.EndPlayerTurn();
        });

        strongAttackButton.onClick.AddListener(() => {
            playerAttack.AttackStrong();
            turnManager.EndPlayerTurn();
        });

        restButton.onClick.AddListener(() => {
            playerStamina.RegainStamina(20f);
            turnManager.EndPlayerTurn();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
