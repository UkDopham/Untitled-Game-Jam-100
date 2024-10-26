using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab; 
    [SerializeField]
    private Transform _iconContainer;
    public void AddItem(Sprite sprite)
    {
        GameObject icon = Instantiate(this._prefab, _iconContainer);
        Image spriteRenderer = icon.GetComponent<Image>();
        spriteRenderer.sprite = sprite;
        icon.transform.localScale = Vector3.one;
    }
}
