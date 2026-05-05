# Jhin Phase 8 Relic And Potion Notes

## Scope

This note records the implementation conventions added for Jhin's MVP phase-8 relics and potion.

## Pool Ownership

Jhin-specific relics and potions should use Jhin-owned pools, not shared pools.

Current setup:

- `src/relicpools/JhinRelicPool.cs`
- `src/potionpools/JhinPotionPool.cs`
- `src/characters/JhinCharacter.cs`

Reason:

- `SharedRelicPool` and `SharedPotionPool` would leak Jhin-only content into non-Jhin runs.
- Jhin's phase-8 content is character-specific combat support, not generic shared content.

## Bullet Empty Event

Phase 8 adds a reusable bullet-empty hook:

- `src/actions/BulletEmptyEventBus.cs`

Current producer:

- `src/actions/ShootAction.cs`

Rule:

- Fire the event only when a shot actually consumes a bullet and the resulting bullet count is exactly `0`.
- Do not fire it on start-of-turn reload.
- Do not fire it on repeated checks while already empty.

This is the correct hook for relics like `EmptyShell`.

## Forced Flourish Entry

Forced flourish is implemented in magazine state, not as a separate parallel flourish system.

Relevant files:

- `src/magazine/JhinMagazineState.cs`
- `src/actions/JhinCombatActionUtil.cs`
- `src/potions/FlourishPotion.cs`
- `src/powers/ForcedFlourishPower.cs`

Rules:

1. Forced flourish must affect both:
   - actual shot resolution
   - preview / playability checks for cards that require a flourish shot
2. The next-shot flourish check should go through `WouldFlourishOnNextShot()`.
3. Do not special-case only card execution, or cards like `FourthAct` will not become playable from the potion.
4. Clear the forced state after one successful shot or when flourish is disabled for the turn.

## Whisper Preview Rule

`Whisper` preview logic should use `WouldFlourishOnNextShot()` instead of checking only `Bullets == 1`.

Reason:

- Forced flourish from phase 8 must also preview the correct damage multiplier and low-HP bonus.

## Verification

After phase-8 relic or potion changes, run:

```powershell
dotnet build "jhin.csproj" -c Debug
powershell -ExecutionPolicy Bypass -File "pack.ps1" -Deploy
```

Also re-check `pack.ps1` if the repository path changes, because it currently contains a hardcoded project root.
