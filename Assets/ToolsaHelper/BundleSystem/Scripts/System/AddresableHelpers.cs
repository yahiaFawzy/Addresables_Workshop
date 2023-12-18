using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AddresableHelpers 
{
  public static List<string> GetRemoteBundlesKey(List<IResourceLocator> locators)
  {
        //var locators = Addressables.ResourceLocators;

        List<string> remoteBundlesKeys = new List<string>();       

        // Get the resource locator for the key.
        foreach (var locator in locators)
        {
            foreach (var key in locator.Keys)
            { 
                string keyName = key.ToString();
                if (keyName.Contains("/Remote"))
                remoteBundlesKeys.Add(keyName);               
            }
        }

        return remoteBundlesKeys;
  }

   

    public static void LogBundlesKeys(MonoBehaviour context,List<IResourceLocator> locators) {
        List<string> list = GetRemoteBundlesKey(locators);
        for (int i = 0; i < list.Count; i++) {
            Debuger.Log(context,list[i]);        
        }
  }

    
}
