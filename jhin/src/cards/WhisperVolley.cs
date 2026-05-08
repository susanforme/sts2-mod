using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Actions;
using jhin.CardPools;
using jhin.Magazine;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class WhisperVolley() : AbstractShootCard(
    cost: 2,
    rarity: CardRarity.Uncommon,
    target: TargetType.AllEnemies)
{
    private const int ShotCount = 2;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
    ];

    protected override bool IsPlayable
    {
        get
        {
            if (!base.IsPlayable)
            {
                return false;
            }

            JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(Owner);
            return state is not null && state.Bullets >= ShotCount;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (Owner.Creature?.CombatState is null)
        {
            return;
        }

        for (int shotIndex = 0; shotIndex < ShotCount; shotIndex++)
        {
            if (!TryShoot(choiceContext))
            {
                return;
            }

            List<Creature> enemies = Owner.Creature.CombatState.HittableEnemies.Where(enemy => enemy.IsAlive).ToList();
            foreach (Creature enemy in enemies)
            {
                await PerformShootAttack(choiceContext, enemy);
            }

            EndFlourishContext();
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}
