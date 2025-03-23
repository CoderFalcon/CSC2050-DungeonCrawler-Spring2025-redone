using UnityEngine;

public class Fight
{
    private Inhabitant attacker;
    private Inhabitant defender;
    private float attackTimer = 0f;
    private float attackCooldown = 1f;
    private bool isFightOver = false;
    private bool hasFightStarted = false;

    public Fight()
    {
        Monster m = new Monster("Goblin");
        this.defender = Core.thePlayer;
        this.attacker = m;

        int roll = Random.Range(0, 20) + 1;
        if (roll <= 10)
        {
            Debug.Log("Monster goes first");
        }
        else
        {
            Debug.Log("Player goes first");
            this.attacker = Core.thePlayer;
            this.defender = m;
        }

        Debug.Log("Fight begins between " + attacker.getName() + " and " + defender.getName());
        hasFightStarted = true;
    }

    public void Update()
    {
        if (!hasFightStarted || isFightOver) return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            attacker.attack(defender);
            attackTimer = 0f;

            if (defender.getCurrHP() <= 0)
            {
                Debug.Log(defender.getName() + " died");
                isFightOver = true;
                return;
            }

            // Swap attacker and defender
            Inhabitant temp = attacker;
            attacker = defender;
            defender = temp;
        }
    }

    public bool IsFightOver()
    {
        return isFightOver;
    }
}

