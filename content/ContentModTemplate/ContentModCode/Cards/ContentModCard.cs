using ContentMod.ContentModCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Scaffolding.Content;

namespace ContentMod.ContentModCode.Cards;

/// <summary>
/// This is the base class for your mod's cards, which is set up to load the card's images from your mod's resources.
/// When creating a card, right click the Cards folder and create a new file with the Custom Card template.
/// This will generate a class that extends this one.
/// You can also just create the class manually; just make sure to inherit from this class.
/// </summary>
public abstract class ContentModCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    ModCardTemplate(cost, type, rarity, target)
{
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{GetType().Name}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{GetType().Name}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{GetType().Name}.png".CardImagePath();
}