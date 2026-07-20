A template for a Slay the Spire 2 character mod with RitsuLib as a dependency.

Localization keys for model content should follow the RitsuLib analyzer format:
`{MODID}_{CATEGORY}_{ORIGINAL_ID}.title` and `{MODID}_{CATEGORY}_{ORIGINAL_ID}.description`.
Example: `TEST_CARD_TEST_CARD.title`.

This template now validates localization at build/init time:
- Build fails if `CharMod/localization/**/*.json` has no files.
- Mod initialization fails fast if a localization folder/file is missing, a file is empty, or JSON is invalid.
- `eng` and `zhs` localization folders are pre-seeded with minimal character/card/relic entries.
- Optional strict key-level validation can be enabled:
  - Set template parameter `StrictLocalizationValidation=true` when creating the project, or
  - Change `MainFile.StrictLocalizationValidation` to `true`.
  - In strict mode, each `.title` key must have a matching `.description` key, and vice versa.
- Colorful Philosophers validation (conditional):
  - If a card pool implements `IModColorfulPhilosophersCardPool`, startup will validate:
    - `events.json` has `COLORFUL_PHILOSOPHERS.pages.INITIAL.options.{ENERGY_COLOR_NAME_UPPER}.title`
    - `events.json` has `COLORFUL_PHILOSOPHERS.pages.INITIAL.options.{ENERGY_COLOR_NAME_UPPER}.description`
    - The pool has at least 3 `Common`, 3 `Uncommon`, and 3 `Rare` cards.

Out of the box test character content is included:
- `CharMod` character with fallback Ironclad visuals (no custom art required).
- Starter deck: 4 `CharModStrike` + 4 `CharModDefend`.
- Starter relic: `CharModStarterRelic` (draws 1 card at turn start).
- Minimal localization keys for character/cards/relic so the template can be launched and tested immediately.

See the [wiki](https://github.com/Miooowo/ModTemplate-StS2/wiki) to get started.
