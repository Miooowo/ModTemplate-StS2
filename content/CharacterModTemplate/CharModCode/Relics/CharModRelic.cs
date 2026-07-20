using CharMod.CharModCode.Character;
using CharMod.CharModCode.Extensions;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Relics;

/// <summary>
/// 角色遗物基类：
/// - 自动绑定到角色遗物池
/// - 统一约定遗物图路径和缺图回退
/// 新建角色遗物时建议继承此类。
/// </summary>
[RegisterRelic(typeof(CharModRelicPool), Inherit = true)]
public abstract class CharModRelic : ModRelicTemplate
{
    public override RelicAssetProfile AssetProfile => new(
        IconPath: $"{GetType().Name}.png".RelicImagePath(),
        IconOutlinePath: $"{GetType().Name}_outline.png".RelicImagePath(),
        BigIconPath: $"{GetType().Name}.png".BigRelicImagePath()
    );
}