using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 注意:确保场景添加到Bulid Settings->Scenes In Build中
/// </summary>
[CreateAssetMenu(menuName = "Data/LoadSceneConfig", fileName = "LoadScene")]
public class SceneConfig : ScriptableObject
{
    public static string LoadPath= "SceneConfig/LoadScene";
    public List<SceneData> sceneDatas;
}

[System.Serializable]
public class SceneData
{
    public SceneName sceneKey;
    public string sceneName;
}

public enum SceneName
{
    Scene1,
    Scene2,
    Scene3,
}