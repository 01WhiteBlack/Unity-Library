using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/PageLoadConfig", fileName = "UILoadData")]
public class PageLoadConfig : ScriptableObject
{
    public static string LoadPath = "UILoadData";
    public GameObject Canvas;
    public GameObject EventSystem;
    public List<PagePath> pages;
}

[System.Serializable]
public class PagePath
{
    public PageName PageName;
    public PageLayer PageLayer;
    public PageSiblingIndex PageSiblingIndex;
    public string LoadPath;

    public override string ToString()
    {
        return (PageName, PageLayer, PageSiblingIndex, LoadPath).ToString();
    }
}

/// <summary>
/// 页面名称
/// </summary>
public enum PageName
{
    PageMain,
}

/// <summary>
/// 页面层级
/// </summary>
public enum PageLayer
{
    LayerZero,
    LayerOne,
    LayerTwo,
}

/// <summary>
/// 页面同级索引
/// </summary>
public enum PageSiblingIndex
{
    Zero,
    One,
    Two,
    Three
}