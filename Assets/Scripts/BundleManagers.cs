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

        // Addressables.InitializeAsync().Completed+=(AsyncOperationHandle<IResourceLocator> resourceLocator) => {
          //   var catalog = resourceLocator.Result;
             var catalog = Addressables.ResourceLocators;
          //   Debug.Log(catalog.LocatorId);
          //   IList<IResourceLocation> locations;

             // Iterate over all of the keys in the catalog.
             //foreach (var key in catalog)
           //  {                 
                 //catalog.Locate(key, typeof(object), out locations);


                 // Get the resource locator for the key.
                 foreach (var location in catalog)
                 {
                    foreach (var key in location.Keys)
                    {
                            Debug.Log("key " + key.ToString());
                    }
                 }
             //}

         //};

    }




}


