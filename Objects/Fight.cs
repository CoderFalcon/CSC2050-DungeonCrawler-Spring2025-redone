using UnityEngine;

public class Fight
{
    private Inhabitant attacker;
    private Inhabitant defender;

    public Fight(Inhabitant p, Inhabitant m)
    {
        int roll = Random.Range(0, 20) + 1;
        if (roll <= 10)
        {
            Debug.Log(m.getName() + " goes first");
            attacker = m;
            defender = p;
        }
        else
        {
            Debug.Log(p.getName() + " goes first");
            attacker = p;
            defender = m;
        }
    }

    public void startFight()
    {
        Debug.Log("Fight begins between " + attacker.getName() + " and " + defender.getName());

        while (attacker.getCurrHP() > 0 && defender.getCurrHP() > 0)
        {
            attacker.attack(defender);

           
            
            if (defender.getCurrHP() > 0)
            {
                Inhabitant temp = attacker;
                attacker = defender;
                defender = temp;
            }
        }

        Debug.Log(defender.getName() + " has been defeated");
    }
}
