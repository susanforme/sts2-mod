#nullable enable

using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.CardPools;
using jhin.Extensions;

namespace jhin.Cards;

/// <summary>
/// 艺术切割 / Art Slice — 1 cost, non-shoot attack.
/// 6 dmg. If enemy has Weak, +6 dmg. Upgrade: 8 dmg, +8 dmg.
/// </summary>
[Pool(typeof(JhinCardPool))]
public class ArtSlice() : AbstractJhinCard(
    cost: 1,
    type: CardType.Attack,
    rarity: CardRarity.Common,
    target: TargetType.AnyEnemy)
{
    protected override string PortraitResourcePath => "Card/JHIN-ART_SLICE.png".ImagePath();

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Target is null)
        {
            return;
        }

        bool hasWeak = cardPlay.Target.GetPower<WeakPower>() is not null;

        await CommonActions.CardAttack(this, cardPlay.Target, DynamicVars.Damage.IntValue, 1, null, null, null).Execute(choiceContext);

        if (hasWeak)
        {
            int bonusDamage = IsUpgraded ? 8 : 6;
            await CommonActions.CardAttack(this, cardPlay.Target, bonusDamage, 1, null, null, null).Execute(choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}
