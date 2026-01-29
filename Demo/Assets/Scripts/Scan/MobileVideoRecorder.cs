using System;
using System.IO;
using UnityEngine;
using TMPro;

public class MobileVideoRecorder : MonoBehaviour
{
    [Header("Scan Settings")]
    public string wallId = "wall_01";
    public int maxDurationSeconds = 20;

    [Header("UI (optional)")]
    public TMP_Text statusText;

    public void RecordVideo()
    {
#if UNITY_ANDROID || UNITY_IOS
        SetStatus("Requesting permission...");

        var perm = NativeCamera.CheckPermission(true);
        if (perm != NativeCamera.Permission.Granted)
            perm = NativeCamera.RequestPermission(true);

        if (perm != NativeCamera.Permission.Granted)
        {
            SetStatus("Camera permission denied.");
            return;
        }

        ScanStorage.EnsureWallFolders(wallId);
        SetStatus("Opening camera...");

        NativeCamera.RecordVideo((path) =>
        {
            if (string.IsNullOrEmpty(path))
            {
                SetStatus("Recording cancelled.");
                return;
            }

            try
            {
                string dest = ScanStorage.VideoPath(wallId);
                File.Copy(path, dest, true);

                SetStatus("Saved to:\n" + dest);
                Debug.Log("[MobileVideoRecorder] Video saved: " + dest);
            }
            catch (Exception e)
            {
                SetStatus("Save failed, see Console.");
                Debug.LogError(e);
            }
        }, maxDurationSeconds);
#else
        SetStatus("Only works on Android/iOS build.");
#endif
    }

    public void LogPaths()
    {
        string root = ScanStorage.WallRoot(wallId);
        Debug.Log("[MobileVideoRecorder] Wall folder: " + root);
        SetStatus("Wall folder:\n" + root);
    }

    private void SetStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
        Debug.Log("[MobileVideoRecorder] " + msg);
    }
}
