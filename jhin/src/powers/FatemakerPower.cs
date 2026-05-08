#nullable enable

using BaseLib.Abstracts;
using BaseLib.Patches.Localization;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using jhin.Magazine;

namespace jhin.Powers;

public class FatemakerPower : CustomPowerModel, IAddDumbVariablesToPowerDescription, IJhinTurnStartPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(Cards.JhinKeywords.Bullet),
    ];

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("extraBullets", Amount > 1 ? 2 : 1);
        description.Add("energyAmount", Amount > 1 ? 2 : 1);
    }

    public void OnTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        JhinMagazineState? state = JhinMagazineStateRegistry.TryGet(player);
        if (state is null)
        {
            return;
        }

        int extraBullets = Amount > 1 ? 2 : 1;
        state.IncreaseMaxBullets(extraBullets);

        Flash();
        int energyAmount = Amount > 1 ? 2 : 1;
        _ = PlayerCmd.GainEnergy(energyAmount, player);
    }
}
