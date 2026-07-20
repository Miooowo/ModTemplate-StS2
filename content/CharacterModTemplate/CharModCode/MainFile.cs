using System.Reflection;
using CharMod.CharModCode.Cards;
using CharMod.CharModCode.Relics;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using STS2RitsuLib;
using STS2RitsuLib.Interop;
using CharMod.CharModCode.Localization;

namespace CharMod.CharModCode;

[ModInitializer(nameof(Initialize))]
/// <summary>
/// 模组入口。负责初始化自动注册、本地化校验以及 Harmony 补丁。
/// </summary>
public partial class MainFile : Node
{
    /// <summary>
    /// 模组唯一 ID，需与 CharMod.json 中的 id 保持一致。
    /// </summary>
    public const string ModId = "CharMod";

    /// <summary>
    /// Godot 资源根路径，例如 res://CharMod。
    /// </summary>
    public const string ResPath = $"res://{ModId}";

    /// <summary>
    /// 是否启用严格本地化键校验（title/description 成对检查）。
    /// 由模板参数 StrictLocalizationValidation 注入。
    /// </summary>
    public static readonly bool StrictLocalizationValidation = bool.Parse("{StrictLocalizationValidation}");

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    /// <summary>
    /// 模组初始化入口。
    /// </summary>
    public static void Initialize()
    {
        var assembly = Assembly.GetExecutingAssembly();

        // 两步都要保留：
        // 1) 注册 Godot 脚本类型
        // 2) 扫描并注册带有 Register* 特性的内容类型
        RitsuLibFramework.EnsureGodotScriptsRegistered(assembly, Logger);
        ModTypeDiscoveryHub.RegisterModAssembly(ModId, assembly);

        // 人物独有内容注册：
        // 古老牙齿：将额外初始卡升级为对应先古卡。
        RitsuLibFramework.RegisterArchaicToothTranscendenceMapping<CharModToothCard, CharModToothAncientCard>();
        // 欧洛巴斯之触：将初始遗物升级为模板中的强化遗物。
        RitsuLibFramework.RegisterTouchOfOrobasRefinementMapping<CharModStarterRelic, CharModStarterRelicRefined>();

        // 启动时快速检查本地化文件，避免运行中才发现缺键/坏 JSON。
        LocalizationValidator.ValidateOrThrow(ModId, StrictLocalizationValidation);
        // 若卡池实现了 IModColorfulPhilosophersCardPool，则执行专项校验。
        ColorfulPhilosophersValidator.ValidateOrThrow(ModId, assembly);

        Harmony harmony = new(ModId);

        harmony.PatchAll();
    }
}
