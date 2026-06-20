using UnityEngine;

/// <summary>
/// Data asset for a player party member. Create instances via:
/// Assets > Create > LootLab > Character
///
/// Sample values (from PLAN.md):
///   Warrior  — HP:120, ATK:18, DEF:10, SPD:8
///   Rogue    — HP:90,  ATK:22, DEF:6,  SPD:14
///   Mage     — HP:80,  ATK:26, DEF:4,  SPD:10
/// </summary>
[CreateAssetMenu(fileName = "NewCharacter", menuName = "LootLab/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int maxHealth;
    public int attack;
    public int defense;
    public int speed;
    public Sprite portrait;
}
