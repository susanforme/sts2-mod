namespace jhin.Utils;

/// <summary>
/// Shared combat constants used by Jhin's current damage systems.
/// </summary>
public static class ConstantUtil
{
    /// <summary>
    /// Bonus damage granted by each Mark stack when a shoot card consumes it.
    /// </summary>
    public const int MarkDamagePerStack = 3;

    /// <summary>
    /// Whisper's flourish damage multiplier.
    /// </summary>
    public const decimal WhisperFlourishMultiplier = 1.5m;

    /// <summary>
    /// Integer display value for Whisper's flourish bonus percent.
    /// </summary>
    public static int WhisperFlourishBonusPercent => decimal.ToInt32((WhisperFlourishMultiplier - 1m) * 100m);

    /// <summary>
    /// Extra flat damage from Whisper when the target is below the low-HP threshold.
    /// </summary>
    public const int WhisperLowHpBonusDamage = 6;

    /// <summary>
    /// HP ratio below which a target counts as low HP.
    /// </summary>
    public const decimal LowHpThreshold = 0.25m;

    /// <summary>
    /// Integer display value for the low-HP threshold percent.
    /// </summary>
    public static int LowHpThresholdPercent => decimal.ToInt32(LowHpThreshold * 100m);
}
