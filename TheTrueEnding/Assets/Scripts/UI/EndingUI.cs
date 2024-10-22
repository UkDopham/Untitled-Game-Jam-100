using TMPro;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _endingCounter;
    [SerializeField]
    private int _endingTotalCount = 5;

    private void Awake()
    {
        this._endingCounter.text = $"{EndingManager.CurrentEndingCount} / {this._endingTotalCount} endings";
    }
}
