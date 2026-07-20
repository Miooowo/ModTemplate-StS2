using CharMod.CharModCode.Character;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Potions;

/// <summary>
/// 角色药水基类（抽象）。
/// 继承此类创建药水会自动注册到角色药水池。
/// </summary>
[RegisterPotion(typeof(CharModPotionPool), Inherit = true)]
public abstract class CharModPotion : ModPotionTemplate;