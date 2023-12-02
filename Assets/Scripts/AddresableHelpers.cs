using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddresableHelpers 
{
  public static List<string> GetRemoteBundlesKey()
    {
        var locators = Addressables.ResourceLocators;

        List<string> remoteBundlesKeys = new List<string>();

        // Get the resource locator for the key.
        foreach (var locator in locators)
        {
            foreach (var key in locator.Keys)
            {
                if (key.ToString().StartsWith("R/"))
                {
                    remoteBundlesKeys.Add(key.ToString());
                }
            }
        }

        return remoteBundlesKeys;
    }
}
