using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

public class BundleManagers : MonoBehaviour
{

    void Start()
    {
        CheckForCatalogUpdates();
    }

    

    //does we add new asset , remove one , update content of one 
    void CheckForCatalogUpdates()
    {
        CatalogUpdater catalogUpdater = new CatalogUpdater();
        catalogUpdater.onUpdateFinished += CatalogUpdateCallBack; 
        catalogUpdater.CheckForCatalogUpdates();
    }

    private void CatalogUpdateCallBack(CatalogUpdater.CatlaogueUpdateResult result)
    {
        List<String> keys = AddresableHelpers.GetRemoteBundlesKey();
        for (int i = 0; i < keys.Count; i++) {
            Debug.Log("key " +i + " : " +keys[i]);
        }
    }

   



}


