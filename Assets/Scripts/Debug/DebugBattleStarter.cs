using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Debug tool to start a battle from the Inspector without any menu flow.
/// Attach to a GameObject in LootLabScene.
/// Assign CharacterData and EnemyData assets, then enter Play Mode.
/// </summary>
public class DebugBattleStarter : MonoBehaviour
{
    [Header("Party (assign CharacterData assets)")]
    public List<CharacterData> partyMembers = new List<CharacterData>();

    [Header("Enemies (assign EnemyData assets)")]
    public List<EnemyData> enemies = new List<EnemyData>();

    [Header("Battle Manager")]
    public BattleManager battleManager;

    private void Start()
    {
        if (battleManager == null)
        {
            Debug.LogError("[DebugBattleStarter] BattleManager reference is missing.");
            return;
        }

        if (partyMembers.Count == 0 || enemies.Count == 0)
        {
            Debug.LogWarning("[DebugBattleStarter] Assign at least one CharacterData and one EnemyData in the Inspector.");
            return;
        }

        battleManager.StartBattle(partyMembers, enemies);
    }
}
