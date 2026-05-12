#nullable enable

using BaseLib.Patches.Localization;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using jhin.Actions;
using jhin.Magazine;

namespace jhin.Powers;

public class WhisperEchoPower : AbstractJhinPower, IAddDumbVariablesToPowerDescription
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(Cards.JhinKeywords.Flourish),
    ];

    public void AddDumbVariablesToPowerDescription(LocString description)
    {
        description.Add("energyAmount", Amount > 1 ? 2 : 1);
        description.Add("drawAmount", Amount > 1 ? 2 : 1);
    }

    protected override void SubscribeEventHandlers()
    {
        FlourishEventBus.OnFlourishTriggered += OnFlourishTriggered;
    }

    protected override void UnsubscribeEventHandlers()
    {
        FlourishEventBus.OnFlourishTriggered -= OnFlourishTriggered;
    }

    private void OnFlourishTriggered(PlayerChoiceContext choiceContext, Player player, JhinMagazineState state)
    {
        if (player != Owner?.Player)
        {
            return;
        }

        Flash();
        int energyAmount = Amount > 1 ? 2 : 1;
        int drawAmount = Amount > 1 ? 2 : 1;
        _ = PlayerCmd.GainEnergy(energyAmount, player);
        _ = JhinCombatActionUtil.Draw(choiceContext, player, drawAmount);
    }
}
