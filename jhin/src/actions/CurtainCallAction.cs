#nullable enable

using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using jhin.CurtainCall;

namespace jhin.Actions;

public static class CurtainCallAction
{
    public const int ShotCount = 4;

    public static async Task Execute(PlayerChoiceContext choiceContext, CardModel sourceCard, int baseDamage)
    {
        await CurtainCallVfxAction.PlayOpening();

        for (int i = 0; i < ShotCount; i++)
        {
            Creature? target = GetRandomLivingEnemy(sourceCard.Owner);
            if (target is null)
            {
                return;
            }

            bool isFinalShot = i == ShotCount - 1;
            await CurtainCallVfxAction.PlayShot(i + 1, target, isFinalShot);

            int damage = CurtainCallDamageUtil.CalculateDamage(baseDamage, target);
            await CommonActions
                .CardAttack(sourceCard, target, damage, 1, null, null, null)
                .Execute(choiceContext);
        }
    }

    private static Creature? GetRandomLivingEnemy(Player? player)
    {
        if (player?.Creature?.CombatState is null)
        {
            return null;
        }

        List<Creature> enemies = player.Creature.CombatState.HittableEnemies
            .Where(enemy => enemy.IsAlive)
            .ToList();

        return enemies.Count == 0
            ? null
            : player.PlayerRng.Transformations.NextItem(enemies);
    }
}
