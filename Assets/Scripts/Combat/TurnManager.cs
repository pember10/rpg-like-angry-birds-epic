using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages turn order for all units in a battle.
/// Plain C# class — no MonoBehaviour needed.
/// Units are sorted by Speed descending. Dead units are skipped.
/// </summary>
public class TurnManager
{
    private List<CombatUnit> _turnOrder = new List<CombatUnit>();
    private int _currentIndex = 0;

    public void BuildTurnOrder(List<CombatUnit> allUnits)
    {
        _turnOrder = allUnits.OrderByDescending(u => u.Speed).ToList();
        _currentIndex = 0;
    }

    /// <summary>Returns the unit whose turn it is, skipping any dead units.</summary>
    public CombatUnit GetCurrentUnit()
    {
        for (int i = 0; i < _turnOrder.Count; i++)
        {
            int index = (_currentIndex + i) % _turnOrder.Count;
            if (_turnOrder[index].IsAlive)
            {
                _currentIndex = index;
                return _turnOrder[index];
            }
        }
        return null;
    }

    public void AdvanceTurn()
    {
        _currentIndex = (_currentIndex + 1) % Mathf.Max(1, _turnOrder.Count);
    }

    public void RemoveDeadUnits()
    {
        int currentUnitId = _currentIndex < _turnOrder.Count
            ? _turnOrder.IndexOf(_turnOrder[_currentIndex])
            : -1;

        _turnOrder.RemoveAll(u => !u.IsAlive);

        if (_currentIndex >= _turnOrder.Count)
            _currentIndex = 0;
    }
}
