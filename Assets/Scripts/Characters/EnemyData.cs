using UnityEngine;

/// <summary>
/// Data asset for an enemy. Create instances via:
/// Assets > Create > LootLab > Enemy
///
/// Sample values (from PLAN.md):
///   Slime    — HP:30,  ATK:8,  DEF:2,  SPD:5
///   Mushroom — HP:40,  ATK:10, DEF:4,  SPD:4
///   Skeleton — HP:50,  ATK:14, DEF:6,  SPD:7
///   Bandit   — HP:45,  ATK:18, DEF:4,  SPD:10
///   Turtle   — HP:80,  ATK:8,  DEF:16, SPD:3
/// </summary>
[CreateAssetMenu(fileName = "NewEnemy", menuName = "LootLab/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int attack;
    public int defense;
    public int speed;
}
