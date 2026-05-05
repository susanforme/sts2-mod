#nullable enable

using MegaCrit.Sts2.Core.Entities.Players;
using jhin.Magazine;

namespace jhin.Actions;

public static class BulletEmptyEventBus
{
    public delegate void BulletEmptiedHandler(Player player, JhinMagazineState state);

    public static event BulletEmptiedHandler? OnBulletEmptied;

    internal static void Notify(Player player, JhinMagazineState state)
    {
        OnBulletEmptied?.Invoke(player, state);
    }

    public static void ClearListeners()
    {
        OnBulletEmptied = null;
    }
}
