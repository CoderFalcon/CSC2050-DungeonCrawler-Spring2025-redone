using UnityEngine;

public class Monster : Inhabitant
{
    public Monster(string name) : base(name)
    {
    }

    public override void attack(Inhabitant target)
    {
        int roll = Random.Range(1, 20);
        int damage = Random.Range(7, 18);

        if (roll + ac >= target.ac)
        {
            Debug.Log(name + " viciously strikes " + target.getName() + " for " + damage + " damage");
            target.takeDamage(damage);
        }
        else
        {
            Debug.Log(name + " misses the attack");
        }
    }
}
