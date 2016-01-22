using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AssetsBag
{
    private Dictionary<string, AssetBundle> loadedAssets = new Dictionary<string, AssetBundle>();
    private static AssetsBag _instance = null;

    public static AssetsBag GetInstance() {
        return _instance;
    }

    public void Add(string assetName, AssetBundle abn)
    {
        loadedAssets.Add(assetName, abn);
    }

    public void Delete(string assetName)
    {
        throw new NotImplementedException();
    }

    public List<string> GetAllLoadedAssetsName()
    {
        return new List<string>(loadedAssets.Keys);
    }

    public AssetBundle GetAssetBundleByName(string assetName)
    {
        return loadedAssets[assetName];
    }

    private AssetsBag()
    {
        _instance = this;
    }

    
}