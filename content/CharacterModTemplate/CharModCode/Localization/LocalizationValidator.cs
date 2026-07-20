using System.Text.Json;

namespace CharMod.CharModCode.Localization;

/// <summary>
/// 本地化启动校验器：
/// - 检查目录/文件是否存在
/// - 检查 JSON 是否合法
/// - 可选严格检查 title/description 成对
/// </summary>
public static class LocalizationValidator
{
    /// <summary>
    /// 执行本地化校验。失败会抛异常并阻止模组继续初始化。
    /// </summary>
    public static void ValidateOrThrow(string modId, bool strictTitleDescriptionPairs = false)
    {
        string modRootPath = AppContext.BaseDirectory;
        string localizationRootPath = Path.Combine(modRootPath, modId, "localization");

        if (!Directory.Exists(localizationRootPath))
        {
            throw new InvalidOperationException(
                $"Localization folder not found: '{localizationRootPath}'. " +
                $"Create '{modId}/localization/<lang>/*.json' before running the mod."
            );
        }

        string[] localizationFiles = Directory.GetFiles(localizationRootPath, "*.json", SearchOption.AllDirectories);
        if (localizationFiles.Length == 0)
        {
            throw new InvalidOperationException(
                $"No localization json files found in '{localizationRootPath}'. " +
                "Add at least one localization file (for example: eng/cards.json)."
            );
        }

        int totalKeyCount = 0;
        foreach (string filePath in localizationFiles)
        {
            string jsonContent = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                throw new InvalidOperationException(
                    $"Localization file is empty: '{filePath}'."
                );
            }

            JsonDocument jsonDocument;
            try
            {
                jsonDocument = JsonDocument.Parse(jsonContent);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    $"Invalid localization JSON in '{filePath}': {ex.Message}",
                    ex
                );
            }

            using (jsonDocument)
            {
                if (jsonDocument.RootElement.ValueKind != JsonValueKind.Object)
                {
                    throw new InvalidOperationException(
                        $"Localization file must contain a JSON object at the root: '{filePath}'."
                    );
                }

                int keyCountInFile = jsonDocument.RootElement.EnumerateObject().Count();
                totalKeyCount += keyCountInFile;
            }
        }

        if (totalKeyCount == 0)
        {
            throw new InvalidOperationException(
                $"No localization keys found under '{localizationRootPath}'."
            );
        }

        if (strictTitleDescriptionPairs)
        {
            // 严格模式：保证内容键的标题/描述成对出现。
            ValidateStrictTitleDescriptionPairs(localizationFiles);
        }
    }

    /// <summary>
    /// 严格键级校验：每个 .title 必须有对应 .description，反之亦然。
    /// </summary>
    private static void ValidateStrictTitleDescriptionPairs(IEnumerable<string> localizationFiles)
    {
        var allKeys = new HashSet<string>(StringComparer.Ordinal);

        foreach (string filePath in localizationFiles)
        {
            using JsonDocument jsonDocument = JsonDocument.Parse(File.ReadAllText(filePath));
            if (jsonDocument.RootElement.ValueKind != JsonValueKind.Object)
            {
                continue;
            }

            foreach (JsonProperty prop in jsonDocument.RootElement.EnumerateObject())
            {
                allKeys.Add(prop.Name);
            }
        }

        foreach (string key in allKeys)
        {
            if (key.EndsWith(".title", StringComparison.Ordinal))
            {
                string pairedDescriptionKey = key[..^".title".Length] + ".description";
                if (!allKeys.Contains(pairedDescriptionKey))
                {
                    throw new InvalidOperationException(
                        $"Strict localization check failed: missing paired key '{pairedDescriptionKey}' for '{key}'."
                    );
                }
            }
            else if (key.EndsWith(".description", StringComparison.Ordinal))
            {
                string pairedTitleKey = key[..^".description".Length] + ".title";
                if (!allKeys.Contains(pairedTitleKey))
                {
                    throw new InvalidOperationException(
                        $"Strict localization check failed: missing paired key '{pairedTitleKey}' for '{key}'."
                    );
                }
            }
        }
    }
}
