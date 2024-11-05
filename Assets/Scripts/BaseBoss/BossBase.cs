using UnityEngine;

[System.Serializable]
public class BaseBoss
{
    public int attack;
    public int defense;
    public int hp;
    public int stamina;
    public int dodge;
    public int currentStamina;
    public int currentHp;
    public void StartBoss1()
    {
        attack = 30;
        defense = 10;
        hp = 160;
        stamina = 180;
        dodge = 20;
        currentStamina = stamina;
        currentHp = hp;
    }

    public void StartBoss2()
    {
        attack = 40;
        defense = 20;
        hp = 180;
        stamina = 210;
        dodge = 30;
        currentStamina = stamina;
        currentHp = hp;

        Debug.Log("Enemy Strength: Attack: " + attack + ", Defense: " + defense + ", Dodge: " + dodge + ", HP: " + hp + ", Stamina: " + stamina);
    }

    public void StartBoss3()
    {
        attack = 60;
        defense = 30;
        hp = 200;
        stamina = 230;
        dodge = 50;
        currentStamina = stamina;
        currentHp = hp;

        Debug.Log("Enemy Strength: Attack: " + attack + ", Defense: " + defense + ", Dodge: " + dodge + ", HP: " + hp + ", Stamina: " + stamina);
    }

    public bool CanDodge(int playerAttack, float K)
    {
        // Tính xác suất dodge dựa trên dodge  của  người chơi và attack của kẻ địch
        float dodgeProbability = 1f / (1f + Mathf.Exp((playerAttack - dodge) / K));

        // Random ngẫu nhiên từ 0 đến 1
        float randomValue = Random.Range(0f, 1f);

        bool dodged = randomValue < dodgeProbability;
        if (dodged)
        {
            Debug.Log("Player successfully dodged the attack with probability: " + dodgeProbability);
        }
        else
        {
            Debug.Log("Player failed to dodge with probability: " + dodgeProbability);
        }
        return dodged;
    }

}
