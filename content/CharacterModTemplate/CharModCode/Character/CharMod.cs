using CharMod.CharModCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using STS2RitsuLib.Data.Models;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Characters;
using STS2RitsuLib.Scaffolding.Godot;

namespace CharMod.CharModCode.Character;

[RegisterCharacter]
public class CharMod : ModCharacterTemplate<CharModCardPool, CharModRelicPool, CharModPotionPool>
{
    public const string CharacterId = "CharMod";
    
    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override Color EnergyLabelOutlineColor => Color;
    public override Color MapDrawingColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;
    public override int StartingGold => 99;

    public override CharacterAssetProfile AssetProfile => CharacterAssetProfiles.Merge(
        CharacterAssetProfiles.Ironclad(),
        new(
            Ui: new(
                IconTexturePath: "character_icon_char_name.png".CharacterUiPath(),
                CharacterSelectIconPath: "char_select_char_name.png".CharacterUiPath(),
                CharacterSelectLockedIconPath: "char_select_char_name_locked.png".CharacterUiPath(),
                MapMarkerPath: "map_marker_char_name.png".CharacterUiPath()
            )
        )
    );

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