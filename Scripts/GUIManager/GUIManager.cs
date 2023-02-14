using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制界面的显示，隐藏
/// 开启，关闭页面的交互
/// </summary>
public class GUIManager
{
    private static GUIManager instance;
    public static GUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GUIManager();
            return instance;
        }
    }

    private GameObject Canvas;
    private GameObject EventSystem;

    private Dictionary<PageName, PagePath> PageLoadInfoDic = new Dictionary<PageName, PagePath>();
    private Dictionary<PageName, IPageControl> PageDic = new Dictionary<PageName, IPageControl>();

    private GUIManager()
    {
        Init();
    }

    #region 初始化
    void Init()
    {
        var pageInfos = LoadPageInfos();
        if (pageInfos == null)
        {
            Debug.Log("页面数据加载失败" + PageLoadConfig.LoadPath);
            return;
        }
        GetLoadPageData(pageInfos.pages);
        CreateCanvasAndEventSystem(pageInfos.Canvas, pageInfos.EventSystem);
    }

    PageLoadConfig LoadPageInfos()
    {
        return Resources.Load<PageLoadConfig>(PageLoadConfig.LoadPath);
    }

    void GetLoadPageData(List<PagePath> pagePaths)
    {
        PageLoadInfoDic.Clear();
        var length = pagePaths.Count;
        for (int i = 0; i < length; i++)
        {
            var value = pagePaths[i];
            var key = value.PageName;
            if (PageLoadInfoDic.ContainsKey(key))
            {
                Debug.Log("Page repeat" + key.ToString());
            }
            else
            {
                PageLoadInfoDic.Add(key, value);
            }
        }
        //PrintDic();
        //void PrintDic()
        //{
        //    Debug.Log("打印字典路径数据");
        //    foreach (var item in PageLoadInfoDic.Keys)
        //    {
        //        Debug.Log((item, PageLoadInfoDic[item]));
        //    }
        //}
    }

    void CreateCanvasAndEventSystem(GameObject canvas, GameObject eventsystem)
    {
        Canvas = Object.Instantiate(canvas);
        EventSystem = Object.Instantiate(eventsystem);

        Canvas.name = nameof(Canvas);

        EventSystem.name = nameof(EventSystem);

        EventSystem.transform.SetAsFirstSibling();
        Canvas.transform.SetAsFirstSibling();

        Object.DontDestroyOnLoad(Canvas);
        Object.DontDestroyOnLoad(EventSystem);
    }
    #endregion

    public void PageShow(PageName pageName)
    {
        var page = GetPage(pageName);
        if (page != null)
        {
            page.PageShow();
        }
    }

    public void PageHide(PageName pageName)
    {
        var page = GetPage(pageName);
        if (page != null)
        {
            page.PageHide();
        }
    }

    public void PageCancleInteraction(PageName pageName, bool isPause)
    {
        var page = GetPage(pageName);
        if (page != null)
        {
            page.PageCancleInteraction(isPause);
        }
    }

    IPageControl GetPage(PageName pageName)
    {
        IPageControl page = null;
        if (PageDic.ContainsKey(pageName))
        {
            page = PageDic[pageName];
        }
        else
        {
            page = LoadPage(pageName);
            if (page != null)
            {
                PageDic.Add(pageName, page);
            }
        }
        return page;
    }

    IPageControl LoadPage(PageName pageName)
    {
        IPageControl page = null;

        if (PageLoadInfoDic.ContainsKey(pageName))
        {
            var value = PageLoadInfoDic[pageName];
            var obj = Resources.Load<GameObject>(value.LoadPath);
            if (obj == null)
            {
                Debug.Log("界面路径配置错误" + value.LoadPath);
                return null;
            }
            else
            {
                //实例化页面 
                var newObj = Object.Instantiate(obj);
                newObj.name = obj.name;
                var newObjTrans = newObj.transform;
                //设置页面层级
                var pageLayer = Canvas.transform.GetChild((int)value.PageLayer);
                if (pageLayer)
                {
                    newObjTrans.SetParent(pageLayer, false);
                    page = newObj.GetComponent<PageBase>() as IPageControl;

                    if (page != null)
                        page.SiblingIndex = value.PageSiblingIndex;
                    else
                        Debug.Log("该页面没有添加BaseView脚本");
                }
                else
                {
                    Debug.Log("界面层级不存在" + value.PageLayer);
                }
            }
        }
        else
        {
            Debug.Log("该界面没有配置相关数据");
        }
        return page;
    }
}
