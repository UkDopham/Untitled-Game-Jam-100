using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    [TextArea(10, 10)]
    private string _text;
    private ScrollUI _scrollUI;

    private void Awake()
    {
       this._scrollUI = FindAnyObjectByType<ScrollUI>();
    }
    public void DisplayScroll()
    {
        this._scrollUI.DisplayScroll(this._text);
    }
}
