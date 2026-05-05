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

namespace jhin.Potions;

[Pool(typeof(PotionPools.JhinPotionPool))]
public class LotusPotion : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override MegaCrit.Sts2.Core.Entities.Cards.TargetType TargetType => MegaCrit.Sts2.Core.Entities.Cards.TargetType.Self;

    public override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.LotusTrap),
    ];

    public override string? CustomPackedImagePath => Placeholders.Role;
    public override string? CustomPackedOutlinePath => Placeholders.Role;

    protected override Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        if (Owner.Creature?.CombatState is null) return Task.CompletedTask;

        foreach (Creature enemy in Owner.Creature.CombatState.HittableEnemies)
        {
            if (enemy.IsAlive)
            {
                ApplyLotusTrapAction.Execute(enemy, 2);
            }
        }
        return Task.CompletedTask;
    }
}
