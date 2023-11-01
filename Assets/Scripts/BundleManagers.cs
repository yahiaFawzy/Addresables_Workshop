using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BundleManagers : MonoBehaviour
{

    void Start()
    {
        CatalogUpdater catalogUpdater = new CatalogUpdater();
        catalogUpdater.CheckForCatalogUpdates();
    }

    //does we add new asset , remove one , update content of one 
   
}


