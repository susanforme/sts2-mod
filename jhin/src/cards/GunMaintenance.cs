using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.CardPools;
using jhin.Powers;

namespace jhin.Cards;

/// <summary>
/// 枪械保养 / Gun Maintenance — 1 cost, common power.
/// On play reload card: draw 1. Upgrade: draw 1 + 2 Block.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class GunMaintenance() : AbstractJhinCard(
    cost: 1,
    type: CardType.Power,
    rarity: CardRarity.Common,
    target: TargetType.Self)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
        HoverTipFactory.FromKeyword(JhinKeywords.Reload),
    ];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        GunMaintenancePower power = (GunMaintenancePower)ModelDb.Power<GunMaintenancePower>().ToMutable();
        power.ApplyInternal(Owner.Creature, IsUpgraded ? 2 : 1, silent: false);
        power.SubscribeEvents();
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
