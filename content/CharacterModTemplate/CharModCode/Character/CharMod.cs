using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using STS2RitsuLib.Data.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Characters;
using STS2RitsuLib.Scaffolding.Godot;

namespace CharMod.CharModCode.Character;

[RegisterCharacter]
/// <summary>
/// 模板角色实现。默认使用铁甲战士资源回退，保证模板开箱可运行。
/// </summary>
public class CharMod : ModCharacterTemplate<CharModCardPool, CharModRelicPool, CharModPotionPool>
{
    /// <summary>
    /// 角色原始 ID（会参与自动注册生成最终内容键）。
    /// </summary>
    public const string CharacterId = "CharMod";
    
    /// <summary>
    /// 角色主题色（名字、地图标记等）。
    /// </summary>
    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    // 如需自定义能量数字描边，可取消注释并调整颜色。
    // public override Color EnergyLabelOutlineColor => new("1f1f1f");
    public override Color MapDrawingColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;
    public override int StartingGold => 99;
    public override float AttackAnimDelay => 0f;
    public override float CastAnimDelay => 0f;

    // 默认回退到原版铁甲资源，避免新项目缺素材时报错。
    public override CharacterAssetProfile AssetProfile => CharacterAssetProfiles.Ironclad();
    public override string? PlaceholderCharacterId => "ironclad";

    // 模板角色不启用时间线/纪元剧情。
    public override bool RequiresEpochAndTimeline => false;

    protected override NCreatureVisuals? TryCreateCreatureVisuals()
    {
        var visualsPath = AssetProfile.Scenes?.VisualsPath;
        if (string.IsNullOrWhiteSpace(visualsPath))
        {
            return null;
        }

        return RitsuGodotNodeFactories.CreateFromScenePath<NCreatureVisuals>(visualsPath);
    }

    public override List<string> GetArchitectAttackVfx() => [
        "vfx/vfx_attack_blunt",
        "vfx/vfx_heavy_blunt",
        "vfx/vfx_attack_slash",
        "vfx/vfx_bloody_impact",
        "vfx/vfx_rock_shatter"
    ];
}