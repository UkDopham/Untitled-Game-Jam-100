using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]
    [TextArea(10, 10)]
    private string _endingText;
    [SerializeField]
    private List<ItemType> _endingConditions = new List<ItemType>();
    [SerializeField]
    private Endings _endingType;
    private Knight _knight;
    private EndingUI _endingUI;

    private void Awake()
    {
        this._endingUI = FindAnyObjectByType<EndingUI>();
        this._knight = FindAnyObjectByType<Knight>();
    }
    public void Interact(List<ItemType> itemTypes)
    {
        bool isContitionsMet = IsContitionsMet(itemTypes);

        if(!isContitionsMet)
        {
            return;
        }

        // End
        End();
    }
    private bool IsContitionsMet(List<ItemType> itemTypes)
    {
        foreach (ItemType type in this._endingConditions)
        {
            if (!itemTypes.Contains(type))
            {
                return false;
            }
        }
        return true;
    }
    private void End()
    {
        print("END");
        SetActionByEnding();
        EndingManager.CurrentEndings.Add(this._endingType);
        Time.timeScale = 0f;
        this._endingUI.LoadEndingText(this._endingText);
    }
    private void SetActionByEnding()
    {
        switch (this._endingType)
        {
            case Endings.trap:
                break;
        }
    }
}
