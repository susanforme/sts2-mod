# Jhin Lotus Trap Implementation Notes

## Context

This note records lessons learned while implementing Jhin's Lotus Trap system in the STS2 mod.

The final design goal:

- Apply Lotus Trap stacks to enemies.
- Trigger after the trapped enemy completes an attack.
- Deal 6 damage per stack immediately.
- Apply 1 Weak per stack after that enemy side's turn ends.
- If Lotus Trap damage kills the enemy, deal 4 damage per stack to all enemies.

## Damage And Death Flow

Do not use `Creature.LoseHpInternal(...)` for gameplay damage that can kill enemies.

`LoseHpInternal` can reduce HP and make the target appear dead, but it can bypass parts of the normal death/removal flow. This caused enemies to display as killed without being properly released from combat.

Use the public damage command instead:

```csharp
IEnumerable<DamageResult> results = await CreatureCmd.Damage(
    choiceContext,
    target,
    amount,
    ValueProp.Move,
    dealer);
```

Use `DamageResult.WasTargetKilled` to decide whether trap damage killed the enemy. Avoid manual fallback checks like `CurrentHp <= 0` unless there is a concrete engine bug and a clear reason to bypass the normal damage result.

Avoid calling `CreatureCmd.Kill(...)` as a generic fallback after `CreatureCmd.Damage(...)`. It is redundant and can interfere with sequencing, especially when follow-up effects such as death explosions need to happen before or during the normal damage/death pipeline.

## Death Explosion Ordering

For Lotus Trap, explosion is triggered only if the trap damage itself kills the target.

When implementing the explosion:

- Use the trap stack count captured before damage resolves.
- Damage is `stacks * ConstantUtil.LotusTrapDeathExplosionDamage`.
- Resolve explosion via `CreatureCmd.Damage(...)`.
- Prefer a living player creature as the dealer rather than the killed enemy. A dead enemy can be an unsafe damage source for follow-up damage.

Do not remove/kill/release the trapped enemy before queuing or resolving the explosion unless the engine's damage command already handles it. Changing combat state first can prevent the explosion from finding valid targets or running at all.

## Hook And Model Construction

Do not create custom `AbstractModel` instances with `new` for combat hook subscribers.

This caused:

```text
DuplicateModelException: Trying to create a duplicate canonical model ... Don't call constructors on models! Use ModelDb instead.
```

If a class inherits `AbstractModel`, it participates in the model database/canonical model system. It must be created through the correct model flow, not ad-hoc constructors.

For global/simple hook logic, prefer attaching behavior to an already valid combat model/power if appropriate. In this implementation, delayed Weak is processed from `BulletPower.AfterTurnEnd(...)` because `BulletPower` is already applied to Jhin through `ModelDb` during combat setup.

## Enemy Attack Trigger Timing

The current implementation patches `MonsterModel.PerformMove` and records `MonsterModel.IntendsToAttack` before the move starts.

Important details:

- Capture intent in Prefix before the enemy action mutates move state.
- In Postfix, wrap the original returned `Task` and await it before triggering the trap.
- This ensures Lotus Trap fires after the enemy's attack behavior completes, not before or midway through it.

Pattern:

```csharp
public static void Postfix(MonsterModel __instance, bool __state, ref Task __result)
{
    if (__state)
    {
        __result = TriggerAfterMove(__instance, __result);
    }
}
```

## Delayed Weak Timing

Applying Weak immediately after an enemy attacks can be wrong because the enemy-side end-turn duration tick can immediately decrement it.

Current approach:

- Trap damage resolves immediately after enemy attack.
- Weak amount is queued by target creature.
- `BulletPower.AfterTurnEnd(...)` applies queued Weak when the affected creature's side ends its turn.

This keeps Weak from being applied too early while avoiding a separate custom hook model.

## Resource Paths

Power icon paths can be provided by overriding BaseLib custom icon properties:

```csharp
public override string CustomPackedIconPath => "res://JhinMod/Images/captive _audience.png";
public override string CustomBigIconPath => "res://JhinMod/Images/captive _audience.png";
public override string CustomBigBetaIconPath => "res://JhinMod/Images/captive _audience.png";
```

The actual file name in this project includes a space before the underscore:

```text
JhinMod/Images/captive _audience.png
```

Use the exact file name.

## Tooltip And Localization

If a Power's own description already explains the mechanic, avoid adding the same keyword as an extra hover tip on the Power itself. That creates redundant tooltip content.

Card keyword tooltips are still useful on cards that mention the mechanic.

All user-facing text must be in `JhinMod/localization`, not hardcoded in code.

When renaming terminology, update at least:

- `JhinMod/localization/zhs/powers.json`
- `JhinMod/localization/zhs/cards.json`
- `JhinMod/localization/zhs/card_keywords.json`
- Any current documentation such as `docs/card/cards.md`

Historical design docs can remain unchanged unless explicitly requested.

## Verification

After any gameplay change, run both:

```powershell
dotnet build "jhin.csproj" -c Debug
powershell -ExecutionPolicy Bypass -File "pack.ps1" -Deploy
```

When runtime behavior fails, check:

```text
C:\Users\susan\AppData\Roaming\SlayTheSpire2\logs
```

The logs have been essential for diagnosing startup and combat-entry failures.
