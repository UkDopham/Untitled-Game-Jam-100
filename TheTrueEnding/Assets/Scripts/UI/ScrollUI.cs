using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollUI : MonoBehaviour, IPointerDownHandler
{
    private TextMeshProUGUI _scrollText;
    private CanvasGroup _canvasGroup;
    private bool _displayed = false;

    private void Awake()
    {
        this._scrollText = GetComponentInChildren<TextMeshProUGUI>();
        this._canvasGroup = GetComponent<CanvasGroup>();
        this._canvasGroup.alpha = 0f;
    }
    public void DisplayScroll(string text)
    {
        if(this._displayed) 
            return;

        this._displayed = true;
        this._scrollText.text = text;
        this._canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this._canvasGroup.alpha = 0f;
    }
}
