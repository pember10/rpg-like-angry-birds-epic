using UnityEngine;

/// <summary>
/// A runtime instance of a character or enemy during a battle.
/// Plain C# class — no MonoBehaviour needed.
/// Holds current state (HP) and handles damage calculation.
/// </summary>
public class CombatUnit
{
    public string UnitName { get; private set; }
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Speed { get; private set; }
    public bool IsAlive => CurrentHealth > 0;
    public bool IsPlayerUnit { get; private set; }

    public CombatUnit(CharacterData data)
    {
        UnitName = data.characterName;
        MaxHealth = data.maxHealth;
        CurrentHealth = data.maxHealth;
        Attack = data.attack;
        Defense = data.defense;
        Speed = data.speed;
        IsPlayerUnit = true;
    }

    public CombatUnit(EnemyData data)
    {
        UnitName = data.enemyName;
        MaxHealth = data.maxHealth;
        CurrentHealth = data.maxHealth;
        Attack = data.attack;
        Defense = data.defense;
        Speed = data.speed;
        IsPlayerUnit = false;
    }

    /// <summary>
    /// Applies damage after defense reduction. Always deals at least 1.
    /// Returns the actual damage dealt.
    /// </summary>
    public int TakeDamage(int rawDamage)
    {
        int damage = Mathf.Max(1, rawDamage - Defense);
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        return damage;
    }
}
