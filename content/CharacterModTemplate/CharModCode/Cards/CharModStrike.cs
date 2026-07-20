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
/// 模板初始打击：
/// - 注册到角色卡池
/// - 默认加入初始卡组 4 张
/// </summary>
[RegisterCard(typeof(CharModCardPool))]
[RegisterCharacterStarterCard(typeof(CharacterType), 4)]
public sealed class CharModStrike : ModCardTemplate
{
    // {Damage:diff()} 对应的基础值与升级增量。
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6, ValueProp.Move)
    ];

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    public override CardAssetProfile AssetProfile => new(
        PortraitPath: "res://icon.svg"
    );

    public CharModStrike() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
    {
    }

    // 打出时对目标造成伤害。
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, cardPlay)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    // 升级：伤害 +3。
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}
