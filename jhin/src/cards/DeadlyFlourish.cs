#nullable enable

using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.CardPools;
using jhin.Actions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class DeadlyFlourish() : AbstractShootCard(
    cost: 2,
    rarity: CardRarity.Rare,
    target: TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
        HoverTipFactory.FromKeyword(JhinKeywords.Mark),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!TryShoot(choiceContext))
        {
            return;
        }

        if (cardPlay.Target is null)
        {
            EndFlourishContext();
            return;
        }

        bool hadMark = ShootAction.GetMarkAmount(cardPlay.Target) > 0;

        await PerformShootAttack(choiceContext, cardPlay.Target);

        if (hadMark)
        {
            int vulnerableAmount = IsFlourishShot ? 2 : 1;
            ApplyOrStackVulnerable(cardPlay.Target, vulnerableAmount);
        }

        EndFlourishContext();
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }

    private static void ApplyOrStackVulnerable(MegaCrit.Sts2.Core.Entities.Creatures.Creature target, int amount)
    {
        VulnerablePower? existingPower = target.GetPower<VulnerablePower>();
        if (existingPower is not null)
        {
            existingPower.SetAmount(existingPower.Amount + amount, silent: false);
            return;
        }

        VulnerablePower vulnerablePower = (VulnerablePower)ModelDb.Power<VulnerablePower>().ToMutable();
        vulnerablePower.ApplyInternal(target, amount, silent: false);
    }
}
