using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundlesManager : Sington<AssetBundlesManager>
{
    Dictionary<string, AssetBundle> abCacheDic = new Dictionary<string, AssetBundle>();

    AssetBundle mainBundle;//主包
    AssetBundleManifest manifest;//依赖信息

    const string mainBundleName = "AssetBundles";//主包名
    string AssetBundlePath
    {
        get
        {
#if UNITY_EDITOR||UNITY_STANDALONE
            return Application.dataPath + "/../AssetBundles/";
#else
            return Application.dataPath + "/AssetBundles/";
#endif
        }
    }//ab包所在路径

    public void LoadAsset<T>(string abName, string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        StartCoroutine(LoadAssetBundleAsync(abName, (value) =>
        {
            callback.Invoke(value.LoadAsset<T>(assetName));
        }));
    }

    public void LoadAssetAsync<T>(string abName, string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        StartCoroutine(LoadAssetBundleAsync(abName, (value) =>
        {
            if (value != null)
            {
                value.LoadAssetAsync<T>(assetName).completed += (operation) =>
                {
                    var asset = (operation as AssetBundleRequest).asset;
                    callback?.Invoke(asset as T);
                };
            }
        }));
    }

    IEnumerator LoadAssetBundleAsync(string abName, Action<AssetBundle> callback)
    {
        //加载主包
        if (mainBundle == null)
        {
            using (var req = UnityWebRequestAssetBundle.GetAssetBundle(AssetBundlePath + mainBundleName))
            {
                yield return req.SendWebRequest();

                if (req.isHttpError || req.isNetworkError)
                {
                    Debug.Log(req.error);
                }
                else
                {
                    //Debug.Log("主包存在：" + (mainBundle != null));
                    if (mainBundle == null)
                    {
                        mainBundle = DownloadHandlerAssetBundle.GetContent(req);
                        manifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    }
                }
            }
        }
        //加载依赖包
        string[] depens = manifest.GetAllDependencies(abName);
        var length = depens.Length;
        for (int i = 0; i < length; i++)
        {
            if (!abCacheDic.ContainsKey(depens[i]))
            {
                using (var req = UnityWebRequestAssetBundle.GetAssetBundle(AssetBundlePath + depens[i]))
                {
                    yield return req.SendWebRequest();
                    if (req.isHttpError || req.isNetworkError)
                    {
                        Debug.Log(req.error);
                    }
                    else
                    {
                        //Debug.Log("依赖包存在：" + abCacheDic.ContainsKey(depens[i]));
                        if (!abCacheDic.ContainsKey(depens[i]))
                        {
                            var ab = DownloadHandlerAssetBundle.GetContent(req);
                            abCacheDic.Add(depens[i], ab);
                        }
                    }
                }
            }
        }
        //加载目标ab包
        if (!abCacheDic.ContainsKey(abName))
        {
            using (var req = UnityWebRequestAssetBundle.GetAssetBundle(AssetBundlePath + abName))
            {
                yield return req.SendWebRequest();
                if (req.isHttpError || req.isNetworkError)
                {
                    Debug.Log(req.error);
                }
                else
                {
                    //Debug.Log("目标包存在：" + abCacheDic.ContainsKey(abName));
                    if (!abCacheDic.ContainsKey(abName))
                    {
                        var ab = DownloadHandlerAssetBundle.GetContent(req);
                        abCacheDic.Add(abName, ab);
                    }
                }
            }
        }
        callback.Invoke(abCacheDic[abName]);
    }

    //打印缓存的ab名称
    Vector2 pos = Vector2.zero;
    private void OnGUI()
    {
        GUILayout.BeginScrollView(pos);
        foreach (var item in abCacheDic.Values)
        {
            GUILayout.Label(item.name);
        }
        GUILayout.EndScrollView();
    }
}
