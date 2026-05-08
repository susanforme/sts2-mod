#nullable enable

using MegaCrit.Sts2.Core.Entities.Creatures;
using jhin.Utils;

namespace jhin.CurtainCall;

public static class CurtainCallDamageUtil
{
    public static int CalculateDamage(int baseDamage, Creature? target)
    {
        decimal multiplier = GetMissingHpMultiplier(target);
        return decimal.ToInt32(decimal.Floor(baseDamage * multiplier));
    }

    public static int CalculateDiscreteMissingHpDamage(int baseDamage, Creature? target)
    {
        decimal multiplier = GetDiscreteMissingHpMultiplier(target);
        return decimal.ToInt32(decimal.Floor(baseDamage * multiplier));
    }

    public static decimal GetMissingHpMultiplier(Creature? target)
    {
        if (target is null || target.MaxHp <= 0)
        {
            return 1m;
        }

        decimal currentHpRatio = Math.Clamp((decimal)target.CurrentHp / target.MaxHp, 0m, 1m);
        return 1m + ((1m - currentHpRatio) * ConstantUtil.CurtainCallMissingHpMultiplierScale);
    }

    public static decimal GetDiscreteMissingHpMultiplier(Creature? target)
    {
        if (target is null || target.MaxHp <= 0)
        {
            return 1m;
        }

        decimal missingHpRatio = 1m - Math.Clamp((decimal)target.CurrentHp / target.MaxHp, 0m, 1m);
        int missingHpSteps = decimal.ToInt32(decimal.Floor(missingHpRatio * 10m));
        return 1m + (missingHpSteps * 0.1m);
    }
}
