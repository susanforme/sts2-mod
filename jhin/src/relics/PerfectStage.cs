#nullable enable

using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Extensions;
using System.Threading.Tasks;

namespace jhin.Relics;

[Pool(typeof(RelicPools.JhinRelicPool))]
public class PerfectStage : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    public override string PackedIconPath => "last_whisper.png".ImagePath();
    protected override string PackedIconOutlinePath => "last_whisper.png".ImagePath();
    protected override string BigIconPath => "last_whisper.png".ImagePath();

    public void CheckTurnEnd()
    {
        if (Owner?.Creature is null) return;

        Magazine.JhinMagazineState? state = Magazine.JhinMagazineStateRegistry.TryGet(Owner);
        if (state is not null && state.CardsPlayedThisTurn == 4)
        {
            Flash();
            _ = CreatureCmd.GainBlock(Owner.Creature, 12m, ValueProp.Move, null, false);
        }
    }
}
