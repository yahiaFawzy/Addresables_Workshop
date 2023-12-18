using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
/// <summary>
/// manage bundle workflow
/// </summary>
public class BundleManagers : MonoBehaviour
{
    //1- check catlog updates return list of updates keys using CatalogUpdater.cs
    //2- update palyer prefs of updates 
    //3- download updates bundle using AssestBundleDownloader.cs
    //4- cash bundle ,clear bundle cash

    AddressablesDownloader addressablesDownloader;

    void Start()
    {
        CheckForCatalogUpdates();
        //addressablesDownloader = new AddressablesDownloader(this);
    }
       

    //does we add new asset , remove one , update content of one 
    void CheckForCatalogUpdates()
    {
        CatalogUpdater catalogUpdater = new CatalogUpdater();
        catalogUpdater.onUpdateFinished += CatalogUpdateCallBackListener; 
        catalogUpdater.CheckForCatalogUpdates();
    }

    private void CatalogUpdateCallBackListener(CatlaogueUpdateResult catlaogueUpdateResult, List<IResourceLocator> handle = null)
    {
        AddresableHelpers.LogBundlesKeys(this, new List<IResourceLocator>(Addressables.ResourceLocators));

        if (catlaogueUpdateResult == CatlaogueUpdateResult.Updated)
        {
            SignUpdatesKeysAsUnCashed(handle);
        }
    }
  
    private void SignUpdatesKeysAsUnCashed(List<IResourceLocator> handle)
    {
        List<string> keys = AddresableHelpers.GetRemoteBundlesKey(handle);

        for (int i = 0; i < keys.Count; i++)
        {
            //flag it as un cashed
            PlayerPrefs.SetInt(keys[i],(int)CashStatus.NotCased);
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

public enum CashStatus{ 
 cased = 1,NotCased = 0
}