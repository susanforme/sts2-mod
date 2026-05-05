#nullable enable

using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using jhin.Actions;
using jhin.Cards;
using jhin.Extensions;
using jhin.Magazine;

namespace jhin.Potions;

[Pool(typeof(PotionPools.JhinPotionPool))]
public class CurtainCallPotion : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override MegaCrit.Sts2.Core.Entities.Cards.TargetType TargetType => MegaCrit.Sts2.Core.Entities.Cards.TargetType.Self;

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
    ];

    public override string? CustomPackedImagePath => Placeholders.Role;
    public override string? CustomPackedOutlinePath => Placeholders.Role;

    protected override Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(Owner);
        state?.SetBulletsToZero();
        _ = JhinCombatActionUtil.Draw(choiceContext, Owner, 2);
        return Task.CompletedTask;
    }
}
