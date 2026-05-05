#nullable enable

using MegaCrit.Sts2.Core.Entities.Creatures;

namespace jhin.Actions;

public static class CurtainCallVfxAction
{
    private const int OpeningDelayMs = 120;
    private const int ShotDelayMs = 90;
    private const int FinalShotDelayMs = 170;

    public static async Task PlayOpening()
    {
        MainFile.Logger.Info("Curtain Call: screen darkened and sniper lines prepared.");
        await Task.Delay(OpeningDelayMs);
    }

    public static async Task PlayShot(int shotNumber, Creature target, bool isFinalShot)
    {
        MainFile.Logger.Info(isFinalShot
            ? $"Curtain Call: heavy fourth shot line locked on {target.LogName}."
            : $"Curtain Call: sniper line {shotNumber} locked on {target.LogName}.");

        await Task.Delay(isFinalShot ? FinalShotDelayMs : ShotDelayMs);
    }
}
