using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Character;

/// <summary>
/// 角色专属遗物池。用于绑定该角色遗物的能量色与图标。
/// </summary>
public class CharModRelicPool : TypeListRelicPoolModel
{
    public override string EnergyColorName => CharMod.CharacterId.ToLowerInvariant();

    // 遗物描述中显示的能量图标（模板使用占位图）。
    public override string? BigEnergyIconPath => "res://icon.svg";
    public override string? TextEnergyIconPath => "res://icon.svg";
}