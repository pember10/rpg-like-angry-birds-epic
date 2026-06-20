# Loot Lab

A mobile-first, turn-based RPG prototype built in **Unity 6 (C#)** for **iOS and Android**.

The central design question:

> Does changing equipment create interesting gameplay and make players want to try one more build?

Everything in this project exists to answer that question. Story, world maps, quests, crafting, and long-term progression are deliberately out of scope until the loot loop is validated.

---

## Concept

Players fight short turn-based battles with a 3-character party. Every victory drops a random piece of loot. Equipment has distinct effects — burning, poisoning, chaining lightning, reflecting damage — and items interact with each other in meaningful ways. The loop is: equip → fight → get loot → tweak build → fight again.

---

## Tech Stack

| | |
|---|---|
| Engine | Unity 6 |
| Language | C# |
| Target Platform | Mobile only (iOS and Android) |
| Data | ScriptableObject-driven (items, characters, enemies) |
| Architecture | Combat logic decoupled from UI |

---

## Project Structure

```
Assets/
  Scripts/
    Characters/     # CharacterData, EnemyData ScriptableObjects
    Combat/         # CombatUnit, TurnManager, BattleManager
    Equipment/      # ItemData, EffectData (Sprint 2+)
    Loot/           # LootTable, RarityRoller (Sprint 5+)
    UI/             # BattleUI, EquipmentUI, InventoryUI, RewardUI
    Debug/          # DebugBattleStarter (Inspector-only test tool)
  ScriptableObjects/
    Characters/     # Party member data assets
    Enemies/        # Enemy archetype data assets
    Items/          # Weapon, Armor, Charm data assets (Sprint 2+)
  Scenes/
    LootLabScene    # Single prototype scene
```

---

## Build Archetypes

Five distinct playstyles supported by item combinations:

| Build | Core Items | Playstyle |
|---|---|---|
| Burn | Fire Wand + Fire Gem | Damage over time, burn spreading |
| Poison | Poison Dagger + Toxic Idol | Scaling DoT, strong vs high HP |
| Shock | Thunder Spear + Storm Mail | Chain damage + shield generation |
| Sustain | Blood Ruby + Druid Robe + Spiked Armor | Healing, reflect, outlast |
| Multi-Hit | Boomerang Axe + Blood Ruby + Lucky Coin | Hit many targets, trigger on-kill effects |

---

## Development Roadmap

| Sprint | Phase | Focus |
|---|---|---|
| 1 | Phase 0–1 | Unity project setup, ScriptableObjects, core combat loop |
| 2 | Phase 2 | Equipment system, inventory, item effects on combat |
| 3 | Phase 3 | Status effects (Burn, Poison, Shock, Shield) |
| 4 | Phase 3 | Item synergy interactions |
| 5 | Phase 4 | Loot tables, rarity tiers, reward screen |
| 6 | Phase 5 | UX polish — equipment/inventory/reward screens |
| 7 | Phase 6–7 | Balance tuning + external playtest |

Full plan: [PLAN.md](PLAN.md)

---

## Getting Started

### Prerequisites

- Unity 6 (download via [Unity Hub](https://unity.com/download))
- iOS and/or Android build modules installed

### Setup

1. Clone the repo:
   ```
   git clone https://github.com/pember10/rpg-like-angry-birds-epic.git
   ```
2. Open Unity Hub → **Add project from disk** → select the repo root.
3. Open `Assets/Scenes/LootLabScene`.

### Running a Debug Battle

1. Create `CharacterData` assets: right-click in `Assets/ScriptableObjects/Characters` → **Create > LootLab > Character**
2. Create `EnemyData` assets: right-click in `Assets/ScriptableObjects/Enemies` → **Create > LootLab > Enemy**
3. In the scene, select the `DebugBattleStarter` GameObject and assign your data assets in the Inspector.
4. Hit **Play**. The battle starts automatically.

---

## Playtest Success Criteria

The prototype is considered validated if testers:

- Replay battles voluntarily
- Experiment with gear without being told to
- Remember items by their effect (not their stat number)
- Discover synergies on their own
- Say something like *"I want to try one more build"*

