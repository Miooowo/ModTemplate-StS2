using System.Reflection;
using STS2RitsuLib.Scaffolding.Content;

namespace CharMod.CharModCode.Localization;

/// <summary>
/// 色彩哲学家专项校验：
/// 仅在卡池实现 IModColorfulPhilosophersCardPool 时触发。
/// </summary>
public static class ColorfulPhilosophersValidator
{
    public static void ValidateOrThrow(string modId, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        string modRootPath = AppContext.BaseDirectory;
        string localizationRootPath = Path.Combine(modRootPath, modId, "localization");
        if (!Directory.Exists(localizationRootPath))
        {
            return;
        }

        foreach (Type poolType in assembly.GetTypes().Where(IsColorfulPhilosophersCardPoolType))
        {
            ValidateEventsLocalizationForPool(localizationRootPath, poolType);
            ValidateCardRarityCountsForPool(assembly, poolType);
        }
    }

    private static bool IsColorfulPhilosophersCardPoolType(Type type)
    {
        if (type.IsAbstract || type.IsInterface)
        {
            return false;
        }

        if (!typeof(TypeListCardPoolModel).IsAssignableFrom(type))
        {
            return false;
        }

        // 用名称判断，避免不同版本/命名空间微调导致的强耦合问题。
        return type.GetInterfaces().Any(i => i.Name == "IModColorfulPhilosophersCardPool");
    }

    private static void ValidateEventsLocalizationForPool(string localizationRootPath, Type poolType)
    {
        if (Activator.CreateInstance(poolType) is not TypeListCardPoolModel poolInstance)
        {
            throw new InvalidOperationException(
                $"ColorfulPhilosophers validation failed: cannot create card pool instance '{poolType.FullName}'. " +
                "Make sure it has a public parameterless constructor."
            );
        }

        string optionId = poolInstance.EnergyColorName.ToUpperInvariant();
        string titleKey = $"COLORFUL_PHILOSOPHERS.pages.INITIAL.options.{optionId}.title";
        string descriptionKey = $"COLORFUL_PHILOSOPHERS.pages.INITIAL.options.{optionId}.description";

        string[] languageDirs = Directory.GetDirectories(localizationRootPath);
        foreach (string languageDir in languageDirs)
        {
            string language = Path.GetFileName(languageDir);
            string eventsPath = Path.Combine(languageDir, "events.json");
            if (!File.Exists(eventsPath))
            {
                throw new InvalidOperationException(
                    $"ColorfulPhilosophers validation failed for pool '{poolType.Name}': " +
                    $"missing localization file '{eventsPath}'."
                );
            }

            using var doc = System.Text.Json.JsonDocument.Parse(File.ReadAllText(eventsPath));
            if (doc.RootElement.ValueKind != System.Text.Json.JsonValueKind.Object)
            {
                throw new InvalidOperationException(
                    $"ColorfulPhilosophers validation failed for pool '{poolType.Name}' in language '{language}': " +
                    $"'{eventsPath}' must be a JSON object."
                );
            }

            bool hasTitle = doc.RootElement.TryGetProperty(titleKey, out _);
            bool hasDescription = doc.RootElement.TryGetProperty(descriptionKey, out _);
            if (!hasTitle || !hasDescription)
            {
                throw new InvalidOperationException(
                    $"ColorfulPhilosophers validation failed for pool '{poolType.Name}' in language '{language}': " +
                    $"missing keys '{titleKey}' and/or '{descriptionKey}'."
                );
            }
        }
    }

    private static void ValidateCardRarityCountsForPool(Assembly assembly, Type poolType)
    {
        var cardTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract)
            .Where(t => IsRegisteredToPool(t, poolType))
            .ToArray();

        int common = 0;
        int uncommon = 0;
        int rare = 0;

        foreach (Type cardType in cardTypes)
        {
            object? cardInstance = Activator.CreateInstance(cardType);
            if (cardInstance is null)
            {
                continue;
            }

            PropertyInfo? rarityProperty = cardType.GetProperty("Rarity");
            object? rarityValue = rarityProperty?.GetValue(cardInstance);
            string rarityName = rarityValue?.ToString() ?? string.Empty;

            switch (rarityName)
            {
                case "Common":
                    common++;
                    break;
                case "Uncommon":
                    uncommon++;
                    break;
                case "Rare":
                    rare++;
                    break;
            }
        }

        if (common < 3 || uncommon < 3 || rare < 3)
        {
            throw new InvalidOperationException(
                $"ColorfulPhilosophers validation failed for pool '{poolType.Name}': " +
                $"requires at least 3 Common / 3 Uncommon / 3 Rare cards, " +
                $"but found Common={common}, Uncommon={uncommon}, Rare={rare}."
            );
        }
    }

    private static bool IsRegisteredToPool(Type cardType, Type poolType)
    {
        foreach (CustomAttributeData attr in cardType.CustomAttributes)
        {
            if (attr.AttributeType.Name != "RegisterCardAttribute")
            {
                continue;
            }

            if (attr.ConstructorArguments.Count == 0)
            {
                continue;
            }

            object? arg = attr.ConstructorArguments[0].Value;
            if (arg is Type registeredPoolType && registeredPoolType == poolType)
            {
                return true;
            }
        }

        return false;
    }
}
