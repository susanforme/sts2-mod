using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using jhin.Extensions;
using jhin.Utils;

namespace jhin.Powers;

public class MarkPower : AbstractJhinPower, IAddDumbVariablesToPowerDescription
{
    public override string CustomPackedIconPath => "JHIN-MARK_POWER.png".PowerImagePath();
    public override string CustomBigIconPath => "JHIN-MARK_POWER.png".PowerImagePath();
    public override string CustomBigBetaIconPath => "JHIN-MARK_POWER.png".PowerImagePath();

    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override int DisplayAmount => Amount;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [];

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("markDamagePerStack", ConstantUtil.MarkDamagePerStack);
    }

    public static int DamagePerStack => ConstantUtil.MarkDamagePerStack;

    public void AddStacks(int amount)
    {
        SetAmount(Amount + amount);
    }

    public void ClearStacks()
    {
        SetAmount(0);
    }
}
