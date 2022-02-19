﻿namespace GTranslate.Results;

/// <summary>
/// Represents a translation result from Microsoft Translator.
/// </summary>
public class MicrosoftTranslationResult : ITranslationResult<Language>, ITranslationResult
{
    internal MicrosoftTranslationResult(string translation, string source, Language targetLanguage,
        Language sourceLanguage, float? score)
    {
        Translation = translation;
        Source = source;
        TargetLanguage = targetLanguage;
        SourceLanguage = sourceLanguage;
        Score = score;
    }

    /// <inheritdoc/>
    public string Translation { get; }

    /// <inheritdoc/>
    public string Source { get; }

    /// <inheritdoc/>
    public string Service => "MicrosoftTranslator";

    /// <inheritdoc/>
    public Language TargetLanguage { get; }

    /// <inheritdoc/>
    public Language SourceLanguage { get; }

    /// <summary>
    /// Gets the language detection score.
    /// </summary>
    public float? Score { get; }

    /// <inheritdoc />
    ILanguage ITranslationResult<ILanguage>.SourceLanguage => SourceLanguage;

    /// <inheritdoc />
    ILanguage ITranslationResult<ILanguage>.TargetLanguage => TargetLanguage;
}