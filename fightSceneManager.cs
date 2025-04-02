using System;
using UnityEngine;

public class fightSceneManager : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;

    private float timeSinceLastTimeDeltaTime = 0.0f;
    private Fight theFight;

    private Vector3 playerStartPos;
    private Vector3 monsterStartPos;

    private bool isPlayerTurn = true;
    private bool waitingForInput = true;

    void Start()
    {
        this.playerStartPos = this.player.transform.position;
        this.monsterStartPos = this.monster.transform.position;

        Debug.Log("Player Max HP: " + Core.thePlayer.getMaxHp());
        Debug.Log("Monster Max HP: " + Core.theMonster.getMaxHp());

        this.theFight = new Fight(Core.theMonster);

        Debug.Log("Player AC: " + Core.thePlayer.getAC());
        Debug.Log("Monster AC: " + Core.theMonster.getAC());
    }

    void Update()
    {
        this.timeSinceLastTimeDeltaTime += Time.deltaTime;

        if (this.timeSinceLastTimeDeltaTime >= 0.5f && !this.theFight.isFightOver())
        {
            if (isPlayerTurn)
            {
                if (waitingForInput)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        this.theFight.takeASwing(player, monster, "normal");
                        waitingForInput = false;
                        isPlayerTurn = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        this.theFight.takeASwing(player, monster, "power");
                        waitingForInput = false;
                        isPlayerTurn = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        this.theFight.drinkPotion(Core.thePlayer);
                        waitingForInput = false;
                        isPlayerTurn = false;
                    }
                }
            }
            else
            {
                this.theFight.takeASwing(monster, player, "normal");
                isPlayerTurn = true;
                waitingForInput = true;
            }

            this.timeSinceLastTimeDeltaTime = 0.0f;
        }
    }
}