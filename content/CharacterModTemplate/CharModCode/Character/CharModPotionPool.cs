using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Character;

/// <summary>
/// 角色专属药水池。用于绑定该角色药水的能量色与图标。
/// </summary>
public class CharModPotionPool : TypeListPotionPoolModel
{
    public override string EnergyColorName => CharMod.CharacterId.ToLowerInvariant();
    // 药水描述中显示的能量图标（模板使用占位图）。
    public override string? BigEnergyIconPath => "res://icon.svg";
    public override string? TextEnergyIconPath => "res://icon.svg";
}