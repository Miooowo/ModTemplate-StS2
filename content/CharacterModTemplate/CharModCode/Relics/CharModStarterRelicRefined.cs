using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using CharMod.CharModCode.Character;

namespace CharMod.CharModCode.Relics;

/// <summary>
/// 初始遗物的强化版（用于欧洛巴斯之触升级结果）。
/// </summary>
[RegisterRelic(typeof(CharModRelicPool))]
public sealed class CharModStarterRelicRefined : ModRelicTemplate
{
    public override RelicRarity Rarity => RelicRarity.Rare;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(2)
    ];

    public override RelicAssetProfile AssetProfile => new(
        IconPath: "res://icon.svg",
        IconOutlinePath: "res://icon.svg",
        BigIconPath: "res://icon.svg"
    );

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, player);
    }
}
