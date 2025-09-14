using Unity.Cinemachine;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public UIManager uiManager;
    public CinemachineCamera battleCamera;
    public Transform playerBattlePosition;
    public Transform allyBattlePosition;
    public Transform enemyBattlePosition;

    private Vector3 currentBattlePosition;
    private Transform player;
    private PlayerController playerController;
    private BattlePlayer battlePlayer;
    private Transform enemy;

    public void StartBattle(Vector3 battlePosition, Transform player, Transform enemy)
    {
        currentBattlePosition = battlePosition;
        this.player = player;
        this.enemy = enemy;

        uiManager.FocusBattleUI();
        transform.position = battlePosition;
        battleCamera.Priority = 5;

        player.GetComponent<ActiveChronoManager>().HideActiveChrono();
        playerController = player.GetComponent<PlayerController>();
        player.position = playerBattlePosition.position;
        playerController.Disable();
        playerController.FaceRight();

        battlePlayer = player.GetComponent<BattlePlayer>();
        battlePlayer.SpawnFirstChrono(allyBattlePosition);

        EnemyChrono enemyChrono = enemy.GetComponent<EnemyChrono>();
        enemy.position = enemyBattlePosition.position;
        enemyChrono.FaceLeft();
    }

    public void EndBattle()
    {
        uiManager.FocusMainUI();
        enemy.position = currentBattlePosition;
        battleCamera.Priority = 0;
        battlePlayer.EndBattle();
        playerController.Enable();
        player.GetComponent<ActiveChronoManager>().ShowActiveChrono();
    }
}
