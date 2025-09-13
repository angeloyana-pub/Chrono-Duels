using UnityEngine;
using Unity.Cinemachine;
using System;

public class BattleManager : MonoBehaviour
{
    public PlayerController player;
    public CinemachineCamera battleCam;
    private Vector3 battleLocation;
    private BattleChrono enemy;
    
    public void StartBattle(Vector3 battleLocation, BattleChrono enemy)
    {
        // NOTE: this script must be attached on battle ui for this to work.
        gameObject.SetActive(true);

        this.enemy = enemy;
        this.battleLocation = battleLocation;
        battleCam.Priority = 5;
        player.DisableController();
        player.SetDestination(
            new Vector3(
                battleLocation.x - 2f,
                battleLocation.y,
                battleLocation.z
            )
        );
        enemy.SetDestination(
            new Vector3(
                battleLocation.x + 2f,
                battleLocation.y,
                battleLocation.z
            )
        );
    }

    public void EndBattle()
    {
        battleCam.Priority = 0;
        player.EnableController();
        enemy.SetDestination(battleLocation);
        gameObject.SetActive(false);
    }
}
