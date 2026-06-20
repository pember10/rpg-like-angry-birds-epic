using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles all battle screen display. Attach to the BattleUI root GameObject.
/// Assign fields in Inspector. Prefab slots for unit rows will be expanded in Sprint 5.
/// </summary>
public class BattleUI : MonoBehaviour
{
    [Header("Combat Log")]
    public Text combatLogText;

    [Header("Unit Panels (expand in Sprint 5 with prefabs)")]
    public Text playerStatusText;
    public Text enemyStatusText;

    [Header("Action Buttons")]
    public Button attackButton;

    [Header("Result Panels")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private readonly List<string> _logLines = new List<string>();
    private const int MaxLogLines = 8;

    // Set by BattleManager; used by attack button's onClick
    private BattleManager _battleManager;
    private CombatUnit _activeUnit;
    private CombatUnit _firstEnemyTarget;

    private void Awake()
    {
        victoryPanel?.SetActive(false);
        defeatPanel?.SetActive(false);

        if (attackButton != null)
        {
            attackButton.interactable = false;
            attackButton.onClick.AddListener(OnAttackPressed);
        }
    }

    public void SetBattleManager(BattleManager bm)
    {
        _battleManager = bm;
    }

    public void AddLogMessage(string message)
    {
        _logLines.Add(message);
        if (_logLines.Count > MaxLogLines)
            _logLines.RemoveAt(0);

        if (combatLogText != null)
            combatLogText.text = string.Join("\n", _logLines);
    }

    public void Refresh(IReadOnlyList<CombatUnit> playerUnits, IReadOnlyList<CombatUnit> enemyUnits)
    {
        if (playerStatusText != null)
        {
            var lines = new System.Text.StringBuilder();
            foreach (var u in playerUnits)
                lines.AppendLine($"{u.UnitName}: {u.CurrentHealth}/{u.MaxHealth} HP");
            playerStatusText.text = lines.ToString().TrimEnd();
        }

        if (enemyStatusText != null)
        {
            var lines = new System.Text.StringBuilder();
            foreach (var u in enemyUnits)
            {
                string status = u.IsAlive ? $"{u.CurrentHealth}/{u.MaxHealth} HP" : "Defeated";
                lines.AppendLine($"{u.UnitName}: {status}");
            }
            enemyStatusText.text = lines.ToString().TrimEnd();
        }

        // Cache first living enemy as default attack target
        _firstEnemyTarget = null;
        foreach (var u in enemyUnits)
        {
            if (u.IsAlive) { _firstEnemyTarget = u; break; }
        }
    }

    public void SetWaitingForInput(CombatUnit activeUnit)
    {
        _activeUnit = activeUnit;
        if (attackButton != null)
            attackButton.interactable = true;

        AddLogMessage($"{activeUnit.UnitName}'s turn — tap Attack.");
    }

    public void ShowVictory()
    {
        victoryPanel?.SetActive(true);
        if (attackButton != null) attackButton.interactable = false;
        AddLogMessage("You found a loot reward! (Sprint 4)");
    }

    public void ShowDefeat()
    {
        defeatPanel?.SetActive(true);
        if (attackButton != null) attackButton.interactable = false;
    }

    private void OnAttackPressed()
    {
        if (_battleManager == null || _activeUnit == null || _firstEnemyTarget == null) return;
        attackButton.interactable = false;
        _battleManager.ExecutePlayerAttack(_activeUnit, _firstEnemyTarget);
    }
}
