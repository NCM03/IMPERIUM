using UnityEngine;

[System.Serializable]
public class BaseBoss
{
    public int attack = 50;
    public int defense = 10;
    public int hp = 200;
    public int stamina = 200;
    public int dodge = 10;
    public int currentStamina;
    public int currentHp;
    public void StartBoss1()
    {
        currentStamina = stamina;
        currentHp = hp;

        Debug.Log("Enemy Strength: Attack: " + attack + ", Defense: " + defense + ", Dodge: " + dodge + ", HP: " + hp + ", Stamina: " + stamina);
    }

    public bool CanDodge(int playerAttack)
    {
        // Tính xác suất dodge dựa trên attack của người chơi và dodge của kẻ địch
        float dodgeProbability = 1f / (1f + Mathf.Exp(playerAttack - dodge));

        // Random ngẫu nhiên từ 0 đến 1
        float randomValue = Random.Range(0f, 1f);

        bool dodged = randomValue < dodgeProbability;
        if (dodged)
        {
            Debug.Log("Enemy successfully dodged the attack with probability: " + dodgeProbability);
        }
        else
        {
            Debug.Log("Enemy failed to dodge with probability: " + dodgeProbability);
        }
        return dodged;
    }

}
