using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
/// <summary>
/// manage bundle workflow
/// </summary>
public class BundleManagers : MonoBehaviour
{
    //check catlog updates , update palyer prefs , download updates bundle , cash bundle ,clear bundle cash

    AddressablesDownloader addressablesDownloader;

    void Start()
    {
        CheckForCatalogUpdates();
        addressablesDownloader = new AddressablesDownloader(this);
    }
       

    //does we add new asset , remove one , update content of one 
    void CheckForCatalogUpdates()
    {
        CatalogUpdater catalogUpdater = new CatalogUpdater();
        catalogUpdater.onUpdateFinished += CatalogUpdateCallBackListener; 
        catalogUpdater.CheckForCatalogUpdates();
    }

    private void CatalogUpdateCallBackListener(CatlaogueUpdateResult catlaogueUpdateResult)
    {
        UpdateCash();
    }

    private void UpdateCash()
    {
        List<String> keys = AddresableHelpers.GetRemoteBundlesKey();
        for (int i = 0; i < keys.Count; i++)
        {
            PlayerPrefs.SetInt(keys[i],0);
        }
    }

    public void DownloadBundle(string key,IBundleCallBack bundleCallBack) {
        if (addressablesDownloader.isDownloading)
        {
            if (key == addressablesDownloader.downloadingBundlekey)
            {
                //todo: handle the bundle is alredy downloading
            }
            else
            {
                //todo:handle download is blocked and other bundle is dowmloading
            }
        }
        else {
            addressablesDownloader.OnDownloadStart = bundleCallBack.OnStartDownload;
            addressablesDownloader.OnDownloadFinish = bundleCallBack.OnFinishDownload;
            addressablesDownloader.OnDownloadProgress = bundleCallBack.OnProgress;
            addressablesDownloader.DownloadBundle(key);
        }
    }

  
}


public interface IBundleCallBack
{
    void OnStartDownload();
    void OnFinishDownload();
    void OnProgress(string message , float size,float totalSize, float downloadPecentage);
}