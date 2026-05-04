# Jhin Cannot-Play FTUE Notes

## Problem

Jhin wanted custom localized disabled text for card-play failure states:

- no energy -> `节拍，停了。`
- has energy but no bullets on shoot cards -> `子弹已尽……但艺术，从不依赖火药。`
- no energy and no bullets -> prioritize the no-energy line

Early fixes that targeted `GetPlayerDialogueLine`, `SelectionScreenPrompt`, and `EnergyHoverTip` were not sufficient for the energy case.

## What Actually Happens

For the basegame "cannot play this card" FTUE popup:

- `NCardPlay.CannotPlayThisCardFtueCheck(CardModel card)` only checks whether FTUE should appear
- it only triggers when `CardModel.CanPlay(...)` returns `UnplayableReason.EnergyCostTooHigh`
- it creates `NCannotPlayCardFtue`
- the actual popup text is assigned later inside `NCannotPlayCardFtue._Ready()`

`NCannotPlayCardFtue._Ready()` directly fills the popup using basegame localization keys:

- table: `ftues`
- key: `CANNOT_PLAY_CARD_FTUE_TITLE`
- key: `CANNOT_PLAY_CARD_FTUE_DESCRIPTION`

That is why changing other prompt sources did not affect the popup text the player saw.

## BaseLib Finding

Local BaseLib source review showed no dedicated high-level API for overriding cannot-play FTUE text.

Relevant checked files:

- `E:\code\sts2-mod-demo\BaseLib-StS2-master\Abstracts\CustomCardModel.cs`
- `E:\code\sts2-mod-demo\BaseLib-StS2-master\Abstracts\ConstructedCardModel.cs`

BaseLib helps with card models, localization providers, portraits, tips, and similar content hooks, but this FTUE text still had to be handled through basegame nodes/patches.

## Final Working Fix

Working approach:

1. Patch `NCardPlay.CannotPlayThisCardFtueCheck(CardModel card)`
2. If the card belongs to Jhin and the failure reason is `EnergyCostTooHigh`, store the current card in a temporary static field
3. Patch `NCannotPlayCardFtue._Ready()`
4. In `_Ready()`, replace the popup description text with the Jhin-localized string
5. Clear the temporary tracked card only after `_Ready()` finishes

Important detail:

- Clearing tracked state too early breaks the fix, because `_Ready()` runs after the FTUE check method returns

## Key Debugging Lesson

If a UI text override appears to do nothing:

- do not assume the prompt comes from `CardModel` getters or reason-dialogue helpers
- inspect the real UI creation path
- for Godot/StS2 UI issues, disassembling the target methods can quickly show where text is truly assigned

## Related Localization Keys

Defined in `JhinMod/localization/*/cards.json`:

- `JHIN-SHOOT_DISABLED_NO_ENERGY`
- `JHIN-SHOOT_DISABLED_NO_BULLETS`

## Files Touched During Fix

- `src/patches/MagazineHooks.cs`
- `JhinMod/localization/zhs/cards.json`
- `JhinMod/localization/eng/cards.json`
