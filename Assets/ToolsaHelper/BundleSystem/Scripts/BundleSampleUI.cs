using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundleSampleUI : MonoBehaviour,IBundleCallBack
{
    [SerializeField] TMPro.TMP_InputField bundleKey;
    [SerializeField] TMPro.TMP_Text status;
    [SerializeField] TMPro.TMP_Text progress;

    [SerializeField] Button DownloadButton;
    [SerializeField] Button ClearCashButton;

    [SerializeField] BundleManagers bundleManagers;

    // Start is called before the first frame update
    void Start()
    {
        DownloadButton.onClick.AddListener(DownloadBundle);
        ClearCashButton.onClick.AddListener(ClearBundleCash);
    }

    private void ClearBundleCash()
    {
        Caching.ClearCache();
    }

    private void DownloadBundle()
    {
        bundleManagers.DownloadBundle(bundleKey.text,this);
    }

    public void OnStartDownload()
    {
        Debuger.Log(this, "start download");
    }

    public void OnFinishDownload()
    {
        Debuger.Log(this, "finish download");       
    }

  
    void IBundleCallBack.OnProgress(string message, float downloadedSizeMB, float totalSizeMB, float downloadPecentage)
    {
        string textMessage = $"{message}:{(int)downloadedSizeMB} / {(int)totalSizeMB}MB";
        status.text = textMessage;
        progress.text = (float)Math.Round(downloadPecentage, 1) + "";
    }
}
