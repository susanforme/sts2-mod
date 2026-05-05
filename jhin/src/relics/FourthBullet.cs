#nullable enable

using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using jhin.Cards;
using jhin.Extensions;
using System.Threading.Tasks;

namespace jhin.Relics;

[Pool(typeof(RelicPools.JhinRelicPool))]
public class FourthBullet : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;

    public override string PackedIconPath => "last_whisper.png".ImagePath();
    protected override string PackedIconOutlinePath => "last_whisper.png".ImagePath();
    protected override string BigIconPath => "last_whisper.png".ImagePath();

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Flourish),
    ];

    private bool _triggeredThisCombat;

    public override Task BeforeCombatStart()
    {
        _triggeredThisCombat = false;
        Actions.FlourishEventBus.OnFlourishTriggered += OnFlourishTriggered;
        return Task.CompletedTask;
    }

    public override Task AfterCombatEnd(MegaCrit.Sts2.Core.Rooms.CombatRoom room)
    {
        Actions.FlourishEventBus.OnFlourishTriggered -= OnFlourishTriggered;
        return Task.CompletedTask;
    }

    private void OnFlourishTriggered(MegaCrit.Sts2.Core.Entities.Players.Player player, Magazine.JhinMagazineState state)
    {
        if (player != Owner || _triggeredThisCombat) return;
        _triggeredThisCombat = true;
        Flash();
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (!_triggeredThisCombat && dealer == Owner?.Creature && Actions.FlourishContext.IsActive)
        {
            return 2m;
        }
        return 1m;
    }
}
