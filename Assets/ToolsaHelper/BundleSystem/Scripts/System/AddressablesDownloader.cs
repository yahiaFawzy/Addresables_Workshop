using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesDownloader 
{
    public  Action OnDownloadStart;
    public  Action OnDownloadFinish;
    public  Action<string,float,float,float> OnDownloadProgress;

    public bool isDownloading = false;
    public string downloadingBundlekey;
    private MonoBehaviour context;

    public AddressablesDownloader(MonoBehaviour context) {
        this.context = context;
    }

    public bool DownloadBundle(string bundleKey)
    {
        if (!isDownloading)
        {
            Debuger.Log(this, "bundle start downloading"+ bundleKey);
            context.StartCoroutine(DownloadBundleIE(bundleKey));
            return isDownloading;
        }
        else {
            Debuger.Log(this, "download blocked cause other bundle is downloading");
            return false;
        }
    }  

    IEnumerator DownloadBundleIE(string key)
    {
        isDownloading = true;
        downloadingBundlekey = key;
        OnDownloadStart?.Invoke();

        var handle = Addressables.DownloadDependenciesAsync(key.ToString(), false); // Download dependencies
        float totalSizeMB = handle.GetDownloadStatus().TotalBytes / (1024.0f * 1024.0f); // Convert bytes to megabytes

        while (!handle.IsDone)
        {
            float downloadedSizeMB = handle.GetDownloadStatus().Percent * totalSizeMB;
            //float size = $"{(int)downloadedSizeMB} / {(int)totalSizeMB}MB";
            float progress = handle.GetDownloadStatus().Percent;
            OnDownloadProgress?.Invoke(key, downloadedSizeMB ,totalSizeMB, progress);
            yield return null;
        }
        PlayerPrefs.SetInt(key, (int)CashStatus.cased);
        isDownloading = false;
        OnDownloadFinish?.Invoke();
        Addressables.Release(handle);
    }
}

