using CharMod.CharModCode.Character;
using CharMod.CharModCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Cards;

/// <summary>
/// 角色卡牌基类：
/// - 自动绑定到角色卡池
/// - 统一约定卡图路径和缺图回退逻辑
/// 新建卡牌时建议继承此类。
/// </summary>
[RegisterCard(typeof(CharModCardPool), Inherit = true)]
public abstract class CharModCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    ModCardTemplate(cost, type, rarity, target)
{
    // 大图（展示/详情）建议尺寸：606x852。
    public override string CustomPortraitPath => $"{GetType().Name}.png".BigCardImagePath();

    // 小图（手牌/战斗）建议尺寸：250x190（或更高分辨率由引擎缩放）。
    public override string PortraitPath => $"{GetType().Name}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{GetType().Name}.png".CardImagePath();
}