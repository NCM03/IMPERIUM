using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAIController : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyStamina enemyStamina;
    private Transform playerTransform;
    public float attackRange = 1f;
    public float safeDistance = 1f;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>(); // Lấy EnemyAttack
        enemyStamina = GetComponent<EnemyStamina>(); // Lấy EnemyStamina
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    public void MakeDecision()
    {
        EnemyStamina enemyStamina = GetComponent<EnemyStamina>();

        if (enemyStamina.currentStamina < 10)
        {
            enemyMovement.Rest();
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // Nếu enemy đang ở xa player, tiến về phía player
            if (distanceToPlayer > attackRange)
            {
                if (distanceToPlayer > safeDistance)
                {
                    enemyMovement.MoveLeft(); // Di chuyển về phía player
                }
            }
            else
            {
                enemyAttack.Attack();
            }
        }
    }
}
