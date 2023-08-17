using UnityEngine;
using DG.Tweening;

public class Screen : MonoBehaviour
{
    public CanvasGroup rootGroup;

    public virtual void Initialize() 
    {
        gameObject.SetActive(false);
        rootGroup.blocksRaycasts = false;
        rootGroup.interactable = false;
        rootGroup.alpha = 0;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        rootGroup.blocksRaycasts = true;
        rootGroup.interactable = true;
        rootGroup.DOFade(1, 0.2f);
    }

    public virtual void Hide()
    {
        rootGroup.blocksRaycasts = false;
        rootGroup.interactable = false;
        rootGroup.DOFade(0, 0.2f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
