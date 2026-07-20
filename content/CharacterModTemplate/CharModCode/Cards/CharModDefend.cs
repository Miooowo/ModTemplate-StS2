using CharMod.CharModCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using CharacterType = CharMod.CharModCode.Character.CharMod;

namespace CharMod.CharModCode.Cards;

/// <summary>
/// 模板初始防御：
/// - 注册到角色卡池
/// - 默认加入初始卡组 4 张
/// </summary>
[RegisterCard(typeof(CharModCardPool))]
[RegisterCharacterStarterCard(typeof(CharacterType), 4)]
public sealed class CharModDefend : ModCardTemplate
{
    // 标记为“提供格挡”的卡，便于界面与逻辑识别。
    public override bool GainsBlock => true;

    // {Block:diff()} 对应的基础值与升级增量。
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(5m, ValueProp.Move)
    ];

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

    public override CardAssetProfile AssetProfile => new(
        PortraitPath: "res://icon.svg"
    );

    public CharModDefend() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
    {
    }

    // 打出时获得格挡。
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
    }

    // 升级：格挡 +3。
    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}
