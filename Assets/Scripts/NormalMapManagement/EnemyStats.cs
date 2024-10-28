using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public int attack;
    public int defense;
    public int hp;
    public int stamina;
    public int dodge;
    public int currentStamina;
    public int currentHp;
    public void AssignRandomStrength()
    {
        int[] possibleTotals = { 10, 15, 20, 25 };
        int randomTotal = possibleTotals[Random.Range(0, possibleTotals.Length)];

        attack = Random.Range(1, randomTotal - 3);
        defense = Random.Range(1, randomTotal - attack - 1);
        dodge = randomTotal - attack - defense;
        hp = Random.Range(100, 150);
        stamina = Random.Range(100, 150);

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
