using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddresableHelpers 
{
  public static List<string> GetRemoteBundlesKey()
    {
        var locators = Addressables.ResourceLocators;

        List<string> remoteBundlesKeys = new List<string>();

        string bundleKeyPrefixFilter = "R/";

        // Get the resource locator for the key.
        foreach (var locator in locators)
        {
            foreach (var key in locator.Keys)
            {
                if (key.ToString().StartsWith(bundleKeyPrefixFilter))
                {
                    remoteBundlesKeys.Add(key.ToString());
                }
            }
        }

        return remoteBundlesKeys;
    }

    public void LogBundlesKeys() {
        List<string> list = GetRemoteBundlesKey();
        for (int i = 0; i < list.Count; i++) {
            Debug.Log(list[i]);        
        }
    } 

}
