#nullable enable

using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Actions;
using jhin.CardPools;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class DancingGrenade() : AbstractJhinCard(
    cost: 2,
    type: CardType.Attack,
    rarity: CardRarity.Rare,
    target: TargetType.RandomEnemy)
{
    private const int MaxHitCount = 4;
    private const int KillBonusDamage = 6;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature?.CombatState is null || Owner.PlayerRng is null)
        {
            return;
        }

        int bounceBonusDamage = 0;
        Creature? currentTarget = JhinCombatActionUtil.GetRandomLivingEnemy(Owner);
        if (currentTarget is null)
        {
            return;
        }

        for (int hitIndex = 0; hitIndex < MaxHitCount && currentTarget is not null; hitIndex++)
        {
            int damage = DynamicVars.Damage.IntValue + (hitIndex * GetBounceStepDamage()) + bounceBonusDamage;
            IEnumerable<DamageResult> results = await CreatureCmd.Damage(choiceContext, currentTarget, damage, ValueProp.Move, Owner.Creature, this);
            DamageResult? result = results.FirstOrDefault(entry => entry.Receiver == currentTarget);

            bounceBonusDamage = result?.WasTargetKilled == true ? bounceBonusDamage + KillBonusDamage : 0;
            currentTarget = GetNextBounceTarget(currentTarget);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }

    private int GetBounceStepDamage() => IsUpgraded ? 5 : 3;

    private Creature? GetNextBounceTarget(Creature currentTarget)
    {
        if (Owner.Creature?.CombatState is null || Owner.PlayerRng is null)
        {
            return null;
        }

        List<Creature> livingOtherEnemies = Owner.Creature.CombatState.HittableEnemies
            .Where(enemy => enemy.IsAlive && enemy != currentTarget)
            .ToList();

        return livingOtherEnemies.Count == 0
            ? null
            : Owner.PlayerRng.Transformations.NextItem(livingOtherEnemies);
    }
}
