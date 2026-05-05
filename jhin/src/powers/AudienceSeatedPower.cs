#nullable enable

using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using jhin.Actions;

namespace jhin.Powers;

/// <summary>
/// 观众入席 / Audience Seated — Combat start: all enemies get 1 Mark.
/// The mark is applied from the card's OnPlay directly.
/// This power just serves as a visual indicator.
/// </summary>
public class AudienceSeatedPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(Cards.JhinKeywords.Mark),
    ];
}
