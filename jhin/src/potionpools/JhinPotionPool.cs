#nullable enable

using BaseLib.Abstracts;
using jhin.Extensions;

namespace jhin.PotionPools;

public class JhinPotionPool : CustomPotionPoolModel
{
    public override string EnergyColorName => Characters.JhinCharacter.CharacterId.ToLowerInvariant();

    public override string? BigEnergyIconPath => Placeholders.Role;

    public override string? TextEnergyIconPath => Placeholders.Role;
}
