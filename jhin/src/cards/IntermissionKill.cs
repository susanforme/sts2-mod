#nullable enable

using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Actions;
using jhin.CardPools;
using jhin.Powers;

namespace jhin.Cards;

/// <summary>
/// 幕间点杀 / Intermission Kill — 1 cost, non-shoot attack.
/// 8 dmg. If target has Mark, gain 4 Block. Upgrade: 11 dmg, gain 6 Block.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class IntermissionKill() : AbstractJhinCard(
    cost: 1,
    type: CardType.Attack,
    rarity: CardRarity.Common,
    target: TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Mark),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target is null)
        {
            return;
        }

        bool hasMark = cardPlay.Target.GetPower<MarkPower>() is not null;

        await CommonActions.CardAttack(this, cardPlay.Target, DynamicVars.Damage.IntValue, 1, null, null, null).Execute(choiceContext);

        if (hasMark)
        {
            int blockAmount = IsUpgraded ? 6 : 4;
            await CreatureCmd.GainBlock(Owner.Creature, blockAmount, ValueProp.Move, cardPlay, false);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}
