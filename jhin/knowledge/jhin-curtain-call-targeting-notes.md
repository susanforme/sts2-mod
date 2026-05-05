# Jhin Curtain Call Targeting Notes

## Context

This note records the targeting bug found while implementing `谢幕 / Curtain Call`.

Curtain Call should:

- be an Attack card
- not ask the player to select a target
- randomly choose a living enemy for each of its 4 shots during resolution

## Bug

The first implementation used `TargetType.AnyEnemy`, which made the player select a target even though the card's own Action ignored that selected target and rolled random targets internally.

Changing the card to `TargetType.Self` fixed the target-selection prompt but introduced a worse runtime bug: after dragging the card out, the card stayed floating and no effect resolved.

## Cause

`TargetType.Self` is not the right workaround for a no-manual-target Attack card.

In STS2, the card drag/play state machine still depends on card target semantics. An Attack card marked as `Self` can enter a mismatched play path and fail to complete card play resolution.

## Correct Fix

Use the engine's dedicated random enemy target type:

```csharp
target: TargetType.RandomEnemy
```

For Curtain Call, keep target selection inside the custom action:

```csharp
Creature? target = GetRandomLivingEnemy(sourceCard.Owner);
```

This means:

- the player does not manually choose an enemy
- the card remains semantically an enemy-targeting Attack
- the engine's drag/play flow can finish normally
- each Curtain Call shot can still independently roll a fresh random living enemy

## TargetType Values Confirmed

Reflection confirmed these relevant `TargetType` enum values exist:

```text
None = 0
Self = 1
AnyEnemy = 2
AllEnemies = 3
RandomEnemy = 4
```

## Lesson

Do not use `TargetType.Self` merely to avoid target selection on an Attack card.

For random enemy attacks, use `TargetType.RandomEnemy`. For all-enemy attacks, use `TargetType.AllEnemies`. Only use `Self` for cards whose effect is actually self-targeted, such as Block or Reload-style skills.

## Verification

After fixing target type, run:

```powershell
dotnet build "jhin.sln"
powershell -ExecutionPolicy Bypass -File "pack.ps1" -Deploy
```

Manual check:

- with 0 bullets, drag `谢幕` out
- it should not require clicking an enemy
- it should resolve immediately and fire 4 random shots
- the card should leave play normally instead of floating
