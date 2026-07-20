using CharMod.CharModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using CharacterType = CharMod.CharModCode.Character.CharMod;

namespace CharMod.CharModCode.Relics;

/// <summary>
/// 模板初始遗物：
/// - 默认作为角色起始遗物
/// - 每回合开始时抽 1 张牌
/// </summary>
[RegisterRelic(typeof(CharModRelicPool))]
[RegisterCharacterStarterRelic(typeof(CharacterType))]
public sealed class CharModStarterRelic : ModRelicTemplate
{
    // 必须是 Starter，才能被视为“起始遗物”参与相关事件逻辑（如欧洛巴斯之触）。
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override RelicAssetProfile AssetProfile => new(
        IconPath: "res://icon.svg",
        IconOutlinePath: "res://icon.svg",
        BigIconPath: "res://icon.svg"
    );

    // 回合开始触发：抽牌。
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, player);
    }
}
