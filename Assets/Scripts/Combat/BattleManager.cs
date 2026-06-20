using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BattleState { Idle, PlayerTurn, EnemyTurn, Victory, Defeat }

/// <summary>
/// Main battle state machine. Attach to a GameObject in LootLabScene.
/// Wire up BattleUI in the Inspector. Call StartBattle() to begin.
/// Call ExecutePlayerAttack() from a UI button during PlayerTurn.
/// </summary>
public class BattleManager : MonoBehaviour
{
    [Header("References")]
    public BattleUI battleUI;

    private List<CombatUnit> _playerUnits = new List<CombatUnit>();
    private List<CombatUnit> _enemyUnits = new List<CombatUnit>();
    private readonly TurnManager _turnManager = new TurnManager();
    private BattleState _state = BattleState.Idle;

    // Expose for UI (e.g. showing whose turn it is)
    public BattleState State => _state;
    public IReadOnlyList<CombatUnit> PlayerUnits => _playerUnits;
    public IReadOnlyList<CombatUnit> EnemyUnits => _enemyUnits;

    public void StartBattle(List<CharacterData> partyData, List<EnemyData> enemyData)
    {
        _playerUnits = partyData.Select(d => new CombatUnit(d)).ToList();
        _enemyUnits = enemyData.Select(d => new CombatUnit(d)).ToList();

        var allUnits = new List<CombatUnit>(_playerUnits);
        allUnits.AddRange(_enemyUnits);
        _turnManager.BuildTurnOrder(allUnits);

        _state = BattleState.Idle;
        battleUI?.Refresh(_playerUnits, _enemyUnits);
        Log("Battle started!");

        StartCoroutine(BattleLoop());
    }

    private IEnumerator BattleLoop()
    {
        while (true)
        {
            _turnManager.RemoveDeadUnits();

            if (CheckVictory()) yield break;
            if (CheckDefeat()) yield break;

            CombatUnit current = _turnManager.GetCurrentUnit();
            if (current == null) yield break;

            if (current.IsPlayerUnit)
            {
                _state = BattleState.PlayerTurn;
                battleUI?.SetWaitingForInput(current);
                yield break; // Resumes when ExecutePlayerAttack is called
            }
            else
            {
                _state = BattleState.EnemyTurn;
                yield return new WaitForSeconds(0.8f);
                ExecuteEnemyAttack(current);
                _turnManager.AdvanceTurn();
            }
        }
    }

    /// <summary>
    /// Called by UI when the player selects an attack action.
    /// attacker = current player unit, target = chosen enemy.
    /// </summary>
    public void ExecutePlayerAttack(CombatUnit attacker, CombatUnit target)
    {
        if (_state != BattleState.PlayerTurn) return;
        if (!attacker.IsAlive || !target.IsAlive) return;

        int damage = target.TakeDamage(attacker.Attack);
        Log($"{attacker.UnitName} attacks {target.UnitName} for {damage} damage.");

        if (!target.IsAlive)
            Log($"{target.UnitName} is defeated.");

        battleUI?.Refresh(_playerUnits, _enemyUnits);
        _turnManager.AdvanceTurn();
        StartCoroutine(BattleLoop());
    }

    private void ExecuteEnemyAttack(CombatUnit enemy)
    {
        CombatUnit target = _playerUnits.FirstOrDefault(u => u.IsAlive);
        if (target == null) return;

        int damage = target.TakeDamage(enemy.Attack);
        Log($"{enemy.UnitName} attacks {target.UnitName} for {damage} damage.");

        if (!target.IsAlive)
            Log($"{target.UnitName} is defeated.");

        battleUI?.Refresh(_playerUnits, _enemyUnits);
    }

    private bool CheckVictory()
    {
        if (_enemyUnits.All(u => !u.IsAlive))
        {
            _state = BattleState.Victory;
            Log("Victory!");
            battleUI?.ShowVictory();
            return true;
        }
        return false;
    }

    private bool CheckDefeat()
    {
        if (_playerUnits.All(u => !u.IsAlive))
        {
            _state = BattleState.Defeat;
            Log("Defeat. The party has fallen.");
            battleUI?.ShowDefeat();
            return true;
        }
        return false;
    }

    private void Log(string message)
    {
        Debug.Log($"[Battle] {message}");
        battleUI?.AddLogMessage(message);
    }
}
