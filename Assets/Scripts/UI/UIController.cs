using DG.Tweening;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public RectTransform[] buttons;
    public float slideDuration = 0.6f;
    public float delayBetweenButtons = 0.2f;

    private void Start()
    {
        SlideButtons();
    }

    void SlideButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform btn = buttons[i];
            Vector2 targetPos = btn.anchoredPosition;
            btn.anchoredPosition = new Vector2 (-500f, targetPos.y);
            btn.DOAnchorPos(targetPos, slideDuration)
                .SetEase(Ease.OutBack)
                .SetDelay(i * delayBetweenButtons);
        }
    }
}
