using CharMod.CharModCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Powers;

/// <summary>
/// 角色能力（Power）基类：
/// - 自动注册能力类型
/// - 统一配置图标路径
/// </summary>
[RegisterPower(Inherit = true)]
public abstract class CharModPower : ModPowerTemplate
{
    // 默认从 CharMod/images/powers/{类名}.png 加载图标。
    public override PowerAssetProfile AssetProfile => new(
        IconPath: $"{GetType().Name}.png".PowerImagePath(),
        BigIconPath: $"{GetType().Name}.png".BigPowerImagePath()
    );

    /// <summary>
    /// 能力类型：增益（Buff）或减益（Debuff）。
    /// </summary>
    public abstract override PowerType Type { get; }
    
    /// <summary>
    /// 叠加方式：
    /// - Counter：重复施加会累加
    /// - Single：不可叠加（常用于唯一效果）
    /// - None：行为近似 Single，一般更推荐显式使用 Single
    /// </summary>
    public abstract override PowerStackType StackType { get; }
}