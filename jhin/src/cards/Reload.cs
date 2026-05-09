using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.Combat;
using jhin.CardPools;
using jhin.Actions;
using jhin.Extensions;

namespace jhin.Cards;

[Pool(typeof(JhinCardPool))]
public class Reload() : AbstractJhinCard(
    cost: 0,
    type: CardType.Skill,
    rarity: CardRarity.Basic,
    target: TargetType.Self)
{
    protected override string PortraitResourcePath => "Card/JHIN-RELOAD.png".ImagePath();

    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(JhinKeywords.Bullet),
        HoverTipFactory.FromKeyword(JhinKeywords.Reload),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ReloadAction.Execute(choiceContext, Owner);
        await Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
    }
}
