#nullable enable

namespace jhin;

/// <summary>
/// Centralized resource paths for this mod.
/// Keep asset references here to avoid scattering hard-coded res:// paths.
/// </summary>
public static class JhinAssets
{
    private static string Res(params string[] parts)
        => "res://" + string.Join("/", parts);

    public static class Audio
    {
        public static string Placeholder => Res(MainFile.ModId, "Assets", "placeholder.mp3");
    }
}
