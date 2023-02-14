public interface IPageControl
{
    PageSiblingIndex SiblingIndex { get; set; }

    void PageShow();

    void PageHide();

    void PageCancleInteraction(bool canInteraction);
}
