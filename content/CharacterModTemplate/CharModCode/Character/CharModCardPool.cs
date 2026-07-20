using Godot;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Character;

/// <summary>
/// 角色专属卡池。决定卡池名称、能量图标和卡框主题色。
/// </summary>
public class CharModCardPool : TypeListCardPoolModel
{
    // 卡池内部 ID（不是玩家可见名称）。
    public override string Title => CharMod.CharacterId;
    // 本地化与事件中使用的能量颜色名，通常为小写。
    public override string EnergyColorName => CharMod.CharacterId.ToLowerInvariant();
    
    // 卡面左上角/提示中显示的能量图标（模板使用 icon 占位）。
    public override string? BigEnergyIconPath => "res://icon.svg";
    public override string? TextEnergyIconPath => "res://icon.svg";
    // 能量数字描边颜色。
    public override Color EnergyOutlineColor => CharMod.Color;

    // 小卡图标（例如卡组列表）使用的主题色。
    public override Color DeckEntryCardColor => new("ffffff");
    
    // 是否无色卡池（事件/状态等一般为 true）。
    public override bool IsColorless => false;
}