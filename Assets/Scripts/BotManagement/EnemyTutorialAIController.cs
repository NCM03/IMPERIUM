using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorialAIController : MonoBehaviour
{
    private EnemyMovementV2 enemyMovement;
    private EnemyTutorialAttack enemyAttack;
    private EnemyStamina enemyStamina;
    private Transform playerTransform;
    public float attackRange = 1.7f;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovementV2>();
        enemyAttack = GetComponent<EnemyTutorialAttack>(); // Lấy EnemyAttack
        enemyStamina = GetComponent<EnemyStamina>(); // Lấy EnemyStamina
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    public void MakeDecision()
    {
        EnemyStamina enemyStamina = GetComponent<EnemyStamina>();

        if (enemyStamina.currentStamina < 10)
        {
            enemyMovement.Rest();
            return;
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            Debug.Log(distanceToPlayer);
            // Nếu enemy đang ở xa player, tiến về phía player
            if (distanceToPlayer > attackRange)
            {
                enemyMovement.MoveLeft();
            }
            else
            {
                enemyAttack.Attack();
            }
        }
    }
}
