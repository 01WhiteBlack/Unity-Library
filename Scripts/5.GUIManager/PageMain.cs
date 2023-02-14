using UnityEngine.UI;
using UnityEngine;
public class PageMain : PageBase
{
    Button start;

    private void Awake()
    {
        start = transform.Find("Start").GetComponent<Button>();
    }

    private void Start()
    {
        if (start != null)
        {
            start.onClick.AddListener(Begin);
        }
    }

    void Begin()
    {
        Debug.Log("Click Start");
    }
}
