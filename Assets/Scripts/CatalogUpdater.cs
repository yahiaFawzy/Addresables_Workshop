using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.AddressableAssets.ResourceLocators;
using System;
/// <summary>
/// In this script, Addressables.CheckForCatalogUpdates is used to check if there are updates available for any catalogs. 
/// If updates are available, it logs the number of catalogs that need to be updated and then calls Addressables.UpdateCatalogs to update those catalogs.
/// This script should be attached to an empty game object in your scene. You can create an empty game object by going to GameObject->CreateEmpty in Unity. Then drag this script onto the empty game object you just created.
/// </summary>
public class CatalogUpdater
{
    public Action<CatlaogueUpdateResult> onUpdateFinished;

    public void CheckForCatalogUpdates()
    {
        Addressables.CheckForCatalogUpdates(false).Completed += OnCheckForCatalogUpdatesCompleted;
    }

    void OnCheckForCatalogUpdatesCompleted(AsyncOperationHandle<List<string>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            List<string> catalogsToUpdate = handle.Result;
            if (catalogsToUpdate != null && catalogsToUpdate.Count > 0)
            {
                Debug.Log("Updates available for " + catalogsToUpdate.Count + " catalogs. Updating now...");
                Addressables.UpdateCatalogs(catalogsToUpdate, false).Completed += OnCatalogsUpdated;
            }
            else
            {
                Debug.Log("No updates available.");
                onUpdateFinished?.Invoke(CatlaogueUpdateResult.NoUpdatesAvaliable);
            }
        }
        else
        {
            Debug.Log("Failed to check for catalog updates.");
            onUpdateFinished?.Invoke(CatlaogueUpdateResult.FailToCheckUpdates);
        }
    }

    void OnCatalogsUpdated(AsyncOperationHandle<List<IResourceLocator>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Catalogs updated successfully.");
            onUpdateFinished?.Invoke(CatlaogueUpdateResult.Updated);
            // Here you can trigger any actions that should happen after the catalogs are updated.
            // For example, you might want to reload any assets that were updated.
        }
        else
        {
            onUpdateFinished?.Invoke(CatlaogueUpdateResult.FailToUpDates);
            Debug.Log("Failed to update catalogs.");
        }
    }

    public enum CatlaogueUpdateResult { 
      FailToCheckUpdates,FailToUpDates,NoUpdatesAvaliable,Updated
    }
}