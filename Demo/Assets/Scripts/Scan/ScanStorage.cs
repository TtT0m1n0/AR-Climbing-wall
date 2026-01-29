using System.IO;
using UnityEngine;

public static class ScanStorage
{
    public static string Root => Path.Combine(Application.persistentDataPath, "Scans");

    public static string WallRoot(string wallId) => Path.Combine(Root, wallId);
    public static string RawDir(string wallId) => Path.Combine(WallRoot(wallId), "raw");

    public static void EnsureWallFolders(string wallId)
    {
        Directory.CreateDirectory(RawDir(wallId));
    }

    public static string VideoPath(string wallId)
    {
        return Path.Combine(RawDir(wallId), $"wall_video.mp4");
    }
}
