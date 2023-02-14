using UnityEngine;
public class PageBase : MonoBehaviour, IPageControl
{
    private CanvasGroup canvasGroup;
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    private PageSiblingIndex siblingIndex;
    public PageSiblingIndex SiblingIndex { get => siblingIndex; set => siblingIndex = value; }

    public virtual void PageShow()
    {
        transform.SetSiblingIndex((int)SiblingIndex);
        gameObject.SetActive(true);
    }

    public virtual void PageHide()
    {
        gameObject.SetActive(false);
    }

    public virtual void PageCancleInteraction(bool canInteraction)
    {
        CanvasGroup.blocksRaycasts = canInteraction;
    }
}
