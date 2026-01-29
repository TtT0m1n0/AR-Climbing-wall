using UnityEngine;

public class MenuFadeController : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    [Range(0f, 1f)]
    public float fadedAlpha = 0.3f;

    void Start()
    {
        SetFaded();
    }

    public void SetFaded()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void SetActive()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Toggle()
{
    if (canvasGroup == null) return;

    bool isActive = canvasGroup.alpha >= 0.99f;

    if (isActive)
        SetFaded();
    else
        SetActive();
}

}
