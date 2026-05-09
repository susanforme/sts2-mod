#nullable enable

using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using jhin.Cards;
using jhin.Extensions;

namespace jhin.Powers;

public class ForcedFlourishPower : CustomPowerModel
{
    public override string CustomPackedIconPath => "Card/JHIN-FORCED_FLOURISH_POWER.png".ImagePath();
    public override string CustomBigIconPath => "Card/JHIN-FORCED_FLOURISH_POWER.png".ImagePath();
    public override string CustomBigBetaIconPath => "Card/JHIN-FORCED_FLOURISH_POWER.png".ImagePath();

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner.Player || cardPlay.Card is not AbstractShootCard)
        {
            return;
        }

        await PowerCmd.Remove(this);
    }
}
