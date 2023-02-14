using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = nameof(LevelManager);
                instance = obj.AddComponent<LevelManager>();
            }
            return instance;
        }
    }

    private Dictionary<SceneName, string> SceneConfigDic = new Dictionary<SceneName, string>();

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        SceneConfigDic.Clear();
        var datas = LoadSceneData();
        if (datas)
        {
            var sceneDatas = datas.sceneDatas;
            var length = sceneDatas.Count;
            for (int i = 0; i < length; i++)
            {
                var key = sceneDatas[i].sceneKey;
                var value = sceneDatas[i].sceneName;
                if (!SceneConfigDic.ContainsKey(key))
                {
                    SceneConfigDic.Add(key, value);
                }
                else
                {
                    Debug.Log("scene key repeat" + key);
                }
            }
        }
        else
        {
            Debug.Log("None Config");
        }
    }

    SceneConfig LoadSceneData()
    {
        return Resources.Load<SceneConfig>(SceneConfig.LoadPath);
    }

    string GetSceneName(SceneName sceneName)
    {
        if (SceneConfigDic.ContainsKey(sceneName))
        {
            return SceneConfigDic[sceneName];
        }
        else
            return string.Empty;
    }

    public void LoadSceneAsync(SceneName sceneName)
    {
        var levelName = GetSceneName(sceneName);
        if (!string.IsNullOrEmpty(levelName))
        {
            StartCoroutine(LoadSceneAsync(levelName));
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// 场景加载完毕调用
    /// </summary>
    /// <param name="callback"></param>
    public void AddListener(UnityAction<Scene, LoadSceneMode> callback)
    {
        SceneManager.sceneLoaded += callback;
    }

    public void RemoveListner(UnityAction<Scene, LoadSceneMode> callback)
    {
        SceneManager.sceneLoaded -= callback;
    }
}
