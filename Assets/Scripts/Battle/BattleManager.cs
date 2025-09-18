using UnityEngine;
using Unity.Cinemachine;

public class BattleManager : MonoBehaviour
{
    public UIManager uiManager;
    public Transform battleGround;
    public Transform playerBattlePosition;
    public Transform allyBattlePosition;
    public Transform enemyBattlePosition;
    public CinemachineCamera battleCamera;
    
    public bool isPlayerTurn = true;
    
    private BattlePlayer player;
    private BattleChrono enemy;
    
    public void StartBattle(Vector3 battlePosition, BattlePlayer player, BattleChrono enemy)
    {
        // TODO: show black screen for transition and switch to battle ui.
        this.player = player;
        this.enemy = enemy;
        
        battleGround.position = battlePosition;
        battleCamera.Priority = 5;
        uiManager.ShowBattleUI();
        
        player.BattleSetup(playerBattlePosition, allyBattlePosition);
        enemy.BattleSetup(battlePosition, enemyBattlePosition);
    }
    
    public void PlayerAttack()
    {
        /**
         * TODO:
         * - show attack dialogue
         * - play attack animation
         * - disable player ui
         * - check for game over
         * - show victory ui, destroy enemy gameobject, and put the enemy into inventory if enemy hp is down to 0.
         */
        if (!isPlayerTurn) return;
        player.Attack(enemy);
        isPlayerTurn = false;
        EnemyAttack();
    }
    
    private void EnemyAttack()
    {
        if (isPlayerTurn) return;
        
        /**
         * TODO:
         * - show attack dialogue
         * - play attack animation
         * - enable player ui
         * - check for game over
         * - do something if player ally chrono hp is down to 0 (e.g. stop the battle and teleport to nearest health center).
         */
        enemy.Attack(player);
        isPlayerTurn = true;
    }
    
    public void PlayerEscape() {
        // TODO: show black screen for transition and switch back to main ui.
        battleCamera.Priority = 0;
        uiManager.ShowMainUI();
        
        player.PlayerEscape();
        enemy.PlayerEscape();
    }
}
