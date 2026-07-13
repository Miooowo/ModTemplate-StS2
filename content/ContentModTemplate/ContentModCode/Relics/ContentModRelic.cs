using ContentMod.ContentModCode.Extensions;
using STS2RitsuLib.Scaffolding.Content;

namespace ContentMod.ContentModCode.Relics;

/// <summary>
/// This is the base class for your mod's relics, which is set up to load the relic's images from your mod's resources.
/// When creating a relic, right click the Relics folder and create a new file with the Custom Relic template.
/// This will generate a class that extends this one.
/// You can also just create the class manually; just make sure to inherit from this class.
/// </summary>
public abstract class ContentModRelic : ModRelicTemplate
{
    //ContentMod/images/relics
    public override RelicAssetProfile AssetProfile => new(
        IconPath: $"{GetType().Name}.png".RelicImagePath(),
        IconOutlinePath: $"{GetType().Name}_outline.png".RelicImagePath(),
        BigIconPath: $"{GetType().Name}.png".BigRelicImagePath()
    );
}