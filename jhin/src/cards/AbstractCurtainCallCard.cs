#nullable enable

using MegaCrit.Sts2.Core.Entities.Cards;
using jhin.CurtainCall;

namespace jhin.Cards;

public abstract class AbstractCurtainCallCard(int cost, CardRarity rarity, TargetType target)
    : AbstractJhinCard(cost, CardType.Attack, rarity, target)
{
    protected abstract CurtainCallCondition UseCondition { get; }

    protected override bool IsPlayable => base.IsPlayable && CanUseCurtainCall();

    public bool CanUseCurtainCall()
    {
        return CurtainCallConditionChecker.CanUse(Owner, UseCondition);
    }
}
