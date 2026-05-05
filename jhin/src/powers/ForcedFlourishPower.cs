#nullable enable

using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using jhin.Cards;

namespace jhin.Powers;

public class ForcedFlourishPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || cardPlay.Card is not AbstractShootCard)
        {
            return Task.CompletedTask;
        }

        Owner.RemovePowerInternal(this);
        return Task.CompletedTask;
    }
}
