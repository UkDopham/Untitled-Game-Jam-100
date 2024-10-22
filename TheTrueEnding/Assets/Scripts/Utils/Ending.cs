using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]
    private List<ItemType> _endingConditions = new List<ItemType>();

    public void Interact(List<ItemType> itemTypes)
    {
        bool isContitionsMet = IsContitionsMet(itemTypes);

        if(!isContitionsMet)
        {
            return;
        }

        // End
        print("END");
        Time.timeScale = 0f;
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
}
