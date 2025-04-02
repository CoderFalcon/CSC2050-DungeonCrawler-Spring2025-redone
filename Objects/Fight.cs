using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Fight
{
    private Inhabitant attacker;
    private Inhabitant defender;

    private Monster theMonster;

    private bool fightOver = false;

    public Fight(Monster m)
    {
        this.theMonster = m;

        //initially determine who goes first
        int roll = Random.Range(0, 20) + 1;
        if (roll <= 10)
        {
            Debug.Log("Monster goes first");
            this.attacker = m;
            this.defender = Core.thePlayer;
        }
        else
        {
            Debug.Log("Player goes first");
            this.attacker = Core.thePlayer;
            this.defender = m;
        }

    }

    public bool isFightOver()
    {
        return this.fightOver;
    }

    public void takeASwing(GameObject attackerObject, GameObject defenderObject, string attackType)
{
    int attackRoll = Random.Range(1, 21);
    float damageMultiplier = 1.0f;
    int attackBonus = 0;

    if (attackType == "power")
    {
        damageMultiplier = 1.5f;
        attackRoll = (int)(attackRoll * 0.75f);
        Debug.Log("Player used Power Attack.");
    }

    Debug.Log("Attack Roll: " + attackRoll);
    Debug.Log("Defender AC: " + defender.getAC());

    if (attackRoll >= defender.getAC())
    {
        int baseDamage = Random.Range(1, 6);
        int totalDamage = (int)(baseDamage * damageMultiplier);
        defender.takeDamage(totalDamage);
        Debug.Log(attacker.getName() + " hit " + defender.getName() + " for " + totalDamage + " damage");

        if (defender.isDead())
        {
            this.fightOver = true;
            Debug.Log(attacker.getName() + " killed " + defender.getName());

            if (defender is Player)
            {
                Debug.Log("Player died");
                attackerObject.SetActive(false);
            }
            else
            {
                Debug.Log("Monster died");
                GameObject.Destroy(defenderObject);
            }
        }
    }
    else
    {
        Debug.Log(attacker.getName() + " missed " + defender.getName());
    }

    Inhabitant temp = this.attacker;
    this.attacker = this.defender;
    this.defender = temp;
}

public void drinkPotion(Inhabitant who)
{
    int healAmount = (int)(who.getMaxHp() * 0.25f);
    int newHp = who.getCurrHp() + healAmount;
    if (newHp > who.getMaxHp())
    {
        newHp = who.getMaxHp();
    }

    Debug.Log(who.getName() + " drinks a potion and restores " + (newHp - who.getCurrHp()) + " HP.");
    typeof(Inhabitant)
        .GetField("currHp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        .SetValue(who, newHp);
}

    public void startFight(GameObject playerGameObject, GameObject monsterGameObject)
    {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round and
        //the one who goes first was determined in the constructor.
        while(true)
        {
            int attackRoll = Random.Range(0, 20) + 1;
            if(attackRoll >= this.defender.getAC())
            {
                //attacker hits the defender
                int damage = Random.Range(1, 6); //1 to 5 damage
                this.defender.takeDamage(damage);

                if(this.defender.isDead())
                {
                    Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                    if(this.defender is Player)
                    {
                        //player died
                        Debug.Log("Player died");
                        //end the game
                        playerGameObject.SetActive(false); //hide the player
                    }
                    else
                    {
                        //monster died
                        Debug.Log("Monster died");
                        //remove the monster from the scene
                        GameObject.Destroy(monsterGameObject); //remove the monster from the scene
                    }
                    break; //fight is over
                }
            }
            else
            {
                Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
            }
            Inhabitant temp = this.attacker;
            this.attacker = this.defender;
            this.defender = temp;
        }
    }
}

