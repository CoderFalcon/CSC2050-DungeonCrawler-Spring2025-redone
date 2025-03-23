using UnityEngine;

public class fightSceneManager : MonoBehaviour
{
    private Fight fight;

    void Start()
    {
        fight = new Fight();
    }

    void Update()
    {
        if (fight != null && !fight.IsFightOver())
        {
            fight.Update();
        }
    }
}