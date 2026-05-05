using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.Actions;
using jhin.CardPools;
using jhin.Powers;

namespace jhin.Cards;

/// <summary>
/// 观众入席 / Audience Seated — 1 cost, common power.
/// Combat start: all enemies get 1 Mark. Upgrade: Innate.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class AudienceSeated() : AbstractJhinCard(
    cost: 1,
    type: CardType.Power,
    rarity: CardRarity.Common,
    target: TargetType.Self)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Mark),
    ];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        AudienceSeatedPower power = (AudienceSeatedPower)ModelDb.Power<AudienceSeatedPower>().ToMutable();
        power.ApplyInternal(Owner.Creature, 1, silent: false);

        // Apply 1 mark to all enemies immediately
        if (Owner.Creature?.CombatState is not null)
        {
            foreach (Creature enemy in Owner.Creature.CombatState.HittableEnemies.Where(e => e.IsAlive))
            {
                ApplyMarkAction.Execute(enemy, 1);
            }
        }

        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
