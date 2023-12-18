using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.AddressableAssets.ResourceLocators;
using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
/// <summary>
/// In this script, Addressables.CheckForCatalogUpdates is used to check if there are updates available for any catalogs. 
/// If updates are available, it logs the number of catalogs that need to be updated and then calls Addressables.UpdateCatalogs to update those catalogs.
/// This script should be attached to an empty game object in your scene. You can create an empty game object by going to GameObject->CreateEmpty in Unity. Then drag this script onto the empty game object you just created.
/// </summary>
public class CatalogUpdater
{
    public Action<CatlaogueUpdateResult, List<IResourceLocator>> onUpdateFinished;

    public void CheckForCatalogUpdates()
    {
        Addressables.CheckForCatalogUpdates(false).Completed += OnCheckForCatalogUpdatesCompleted;
    }

    void OnCheckForCatalogUpdatesCompleted(AsyncOperationHandle<List<string>> handle) //list of updated catalog
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            LogUpdatedBundleKeys(handle);
            List<string> catalogsToUpdate = handle.Result;
            if (catalogsToUpdate != null && catalogsToUpdate.Count > 0)
            {
                Debuger.Log(this,"Updates available for " + catalogsToUpdate.Count + " catalogs. Updating now...");
                Addressables.UpdateCatalogs(catalogsToUpdate, false).Completed += OnCatalogsUpdated;
            }
            else
            {
                Debuger.Log(this,"No updates available.");
                onUpdateFinished?.Invoke(CatlaogueUpdateResult.NoUpdatesAvaliable,null);
            }
        }
        else
        {
            Debuger.Log(this,"Failed to check for catalog updates.");
            onUpdateFinished?.Invoke(CatlaogueUpdateResult.FailToCheckUpdates,null);
        }

    }

    private void LogUpdatedBundleKeys(AsyncOperationHandle<List<string>> handle) {
        for(int i=0;i<handle.Result.Count;i++)
        Debug.Log($"bundle with key { handle.Result[i] } is updated");
    }


    public async Task<List<string>> GetListOfUpdatedBundles()
    {
        var handle = Addressables.CheckForCatalogUpdates(false);

        await handle.Task; // Await the completion of the async operation

        List<string> ListOfUpdatedBundlesNames = new List<string>();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var locator in handle.Result)
            {
                string bundleAddress = locator;

                // Check if the bundle was updated or added
                if (bundleAddress.Length > 0)
                {
                    // Bundle was updated or added, add it to the list
                    ListOfUpdatedBundlesNames.Add(bundleAddress);
                }
                else
                {                 
                    
                }
            }
        }

        Addressables.Release(handle); // Release the handle to free up resources
        return ListOfUpdatedBundlesNames;
    }

    void OnCatalogsUpdated(AsyncOperationHandle<List<IResourceLocator>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Catalogs updated successfully.");
            onUpdateFinished?.Invoke(CatlaogueUpdateResult.Updated, handle.Result);
            // Here you can trigger any actions that should happen after the catalogs are updated.
            // For example, you might want to reload any assets that were updated.
        }
        else
        {
            onUpdateFinished?.Invoke(CatlaogueUpdateResult.FailToUpDates,handle.Result);
            Debug.Log("Failed to update catalogs.");
        }
    }


   

}

public class BundleUpdatesData {
    public string bundleName;
    public BundleUpdateType updateType;
}

public enum BundleUpdateType { 
 Added,Updated,Removed
}

public enum CatlaogueUpdateResult
{
    FailToCheckUpdates, FailToUpDates, NoUpdatesAvaliable, Updated
}