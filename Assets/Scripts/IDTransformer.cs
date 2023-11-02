using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
using UnityEngine;

public static class IDTransformer
{
    //Implement a method to transform the internal ids of locations
   public static string MyCustomTransform(IResourceLocation location)
    {
        if (location.ResourceType == typeof(IAssetBundleResource)
            && location.InternalId.StartsWith("http", System.StringComparison.Ordinal))
            return location.InternalId + "?customQueryTag=customQueryValue";

        return location.InternalId;
    }

    //Override the Addressables transform method with your custom method.
    //This can be set to null to revert to default behavior.
    [RuntimeInitializeOnLoadMethod]
    static void SetInternalIdTransform()
    {
        Addressables.InternalIdTransformFunc = MyCustomTransform;
    }
}
