using System.Collections;
using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private ItemsUI _itemsUI;
    private TextMeshProUGUI _textMeshPro;
    private CanvasGroup _canvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        this._canvasGroup = GetComponent<CanvasGroup>();
    }

    public void AddItem(Item item)
    {
        StartCoroutine(DisplayText(item));
    }
    private IEnumerator DisplayText(Item item)
    {
        this._canvasGroup.alpha = 1;
        this._textMeshPro.text = $"The knight has aquired a new item {item.name} !";
        this._itemsUI.AddItem(item.Sprite);
        yield return new WaitForSeconds(1f);
        this._canvasGroup.alpha = 0;
        yield return null;
    }
}
