using CharMod.CharModCode.Character;
using CharMod.CharModCode.Extensions;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Relics;

/// <summary>
/// This is the base class for your mod's relics, which is set up to load the relic's images from your mod's resources.
/// When creating a relic, right click the Relics folder and create a new file with the Custom Relic template.
/// This will generate a class that extends this one.
/// You can also just create the class manually; just make sure to inherit from this class.
///
/// The [RegisterRelic] annotation marks this relic as being tied to your specific character. Inheriting from this class means
/// that your relics don't need to invidually say which pool they should be in.
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