#nullable enable

using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace jhin.Powers;

/// <summary>
/// Implement this interface on any power that needs to run logic at the start of the player's turn.
/// The MagazineHooks patch will automatically discover and invoke all powers implementing this interface.
/// </summary>
public interface IJhinTurnStartPower
{
    void OnTurnStart(PlayerChoiceContext choiceContext, Player player);
}
