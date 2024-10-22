using Assets.Scripts.Constants;
using TMPro;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _endingCounter;
    [SerializeField]
    private TextMeshProUGUI _endingText;
    [SerializeField]
    private int _endingTotalCount = 5;

    private void Awake()
    {
        this._endingCounter.text = $"{EndingManager.CurrentEndings.Count} / {this._endingTotalCount} endings completed";
        this._endingText.gameObject.SetActive(false);
    }
    public void LoadEndingText(string text)
    {
        this._endingText.gameObject.SetActive(true);
        this._endingText.text = text;  
    }
}
