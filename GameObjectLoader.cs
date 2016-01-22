using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameObjectLoader
{
    
    private static AssetBundle LoadObjectFromAssetFile(string assetName)
    {
        WWW www = new WWW(Statics.AssetBundleStoragePath +  assetName);
        AssetBundle bundle = www.assetBundle;
        AssetsBag.GetInstance().Add(assetName, bundle);
        return bundle;
    }

    private static bool IfLoadedBefore(string assetName, out AssetBundle ao)
    {
        var alist = AssetsBag.GetInstance().GetAllLoadedAssetsName();
        foreach (var a in alist)
        {
            if (a.Equals(assetName))
            {
                ao = AssetsBag.GetInstance().GetAssetBundleByName(a);
                return true;
            }
        }
        ao = null;
        return false;
    }

    public static UnityEngine.Object LoadGameObject(string assetName)
    {
        AssetBundle ao = null;
        if (!IfLoadedBefore(assetName, out ao))
        {
            ao = LoadObjectFromAssetFile(assetName);
        }
        return ao;
    }


  

}