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
    public Action OnCatalogUpdated;
    AddressablesDownloader addressablesDownloader;
    CatalogUpdater catalogUpdater;

    void Start()
    {
        CheckForCatalogUpdates();
        addressablesDownloader = new AddressablesDownloader(this);
    }
       

    //does we add new asset , remove one , update content of one 
    void CheckForCatalogUpdates()
    {
        catalogUpdater = new CatalogUpdater();
        catalogUpdater.onUpdateFinished += CatalogUpdateCallBackListener; 
        catalogUpdater.CheckForCatalogUpdates();
    }

    private async void CatalogUpdateCallBackListener(CatlaogueUpdateResult catlaogueUpdateResult, List<IResourceLocator> handle = null)
    {
        if (catlaogueUpdateResult == CatlaogueUpdateResult.Updated)
        {
            //sign updated bundles as uncashed
            var list = await catalogUpdater.GetListOfUpdatedBundles();
            AddresableHelpers.SignUpdatesKeysAsUnCashed(list);
        }
        OnCatalogUpdated?.Invoke();
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