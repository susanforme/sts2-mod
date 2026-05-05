#nullable enable

using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace jhin.Powers;

/// <summary>
/// 等待掌声 / Await Applause — Next turn start: gain 1 energy, then remove self.
/// The turn-start trigger is handled in MagazineHooks.PlayerCombatStateResetEnergyPatch.
/// </summary>
public class AwaitApplausePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [];
}
