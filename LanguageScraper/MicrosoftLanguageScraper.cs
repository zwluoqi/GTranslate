using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
//using System.Text.Json;
using System.Threading.Tasks;
using GTranslate;
using Newtonsoft.Json.Linq;

namespace LanguageScraper;

public class MicrosoftLanguageScraper : ILanguageScraper
{
    private readonly HttpClient _httpClient = new();

    public MicrosoftLanguageScraper()
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en");
    }

    public TranslationServices TranslationService => TranslationServices.Microsoft;

    public IReadOnlyCollection<ILanguage> ExistingTtsLanguages => Array.Empty<ILanguage>();

    public async Task<LanguageData> GetLanguageDataAsync()
    {
        var stream = await _httpClient.GetStreamAsync(new Uri("https://api.cognitive.microsofttranslator.com/languages?api-version=3.0&scope=translation"));
        var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        var document = JObject.Parse(content);

        var languages = document
            .GetValue("translation")
            .ToObject<JObject>()
            .Properties()
            .Select(x => new ScrapedLanguage(
                x.Value["name"].ToString(),
                x.Name,
                string.Empty,
                x.Value["nativeName"].ToString()))
            .ToArray();

        return new LanguageData { Languages = languages, TtsLanguages = Array.Empty<ILanguage>() };
    }

}