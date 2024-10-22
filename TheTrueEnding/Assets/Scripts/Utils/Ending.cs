using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField]
    [TextArea(10, 10)]
    private string _endingText;
    [SerializeField]
    private List<ItemType> _endingConditions = new List<ItemType>();

    private void Awake()
    {
        
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
        EndingManager.CurrentEndingCount++;
        Time.timeScale = 0f;
    }
}
