using CharMod.CharModCode.Character;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Potions;

[RegisterPotion(typeof(CharModPotionPool), Inherit = true)]
public abstract class CharModPotion : ModPotionTemplate;