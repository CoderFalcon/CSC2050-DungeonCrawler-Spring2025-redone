using UnityEngine;

public abstract class Inhabitant
{
    protected int currHp;
    protected int maxHp;
    protected int ac;
    protected string name;

    public Inhabitant(string name)
    {
        this.name = name;
        this.maxHp = Random.Range(30, 50);
        this.currHp = this.maxHp;
        this.ac = Random.Range(10, 20);
    }

    public string getName()
    {
        return name;
    }

    public int getCurrHP()
    {
        return currHp;
    }

    public int getAC()
    {
        return this.ac;
    }

    public void takeDamage(int dmg)
    {
        currHp -= dmg;
        if (currHp < 0) currHp = 0;
        Debug.Log(name + " takes " + dmg + " damage. Current HP: " + currHp);
    }

    public virtual void attack(Inhabitant target)
    {
        int roll = Random.Range(1, 20);
        int damage = Random.Range(5, 15);

        if (roll + ac >= target.ac)
        {
            Debug.Log(name + " hits " + target.getName() + " for " + damage + " damage");
            target.takeDamage(damage);
        }
        else
        {
            Debug.Log(name + " misses the attack");
        }
    }
}
