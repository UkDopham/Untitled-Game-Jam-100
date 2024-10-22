using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    [TextArea(10, 10)]
    private string _text;
    
    public string Text
    {
        get
        {
            return _text;
        }
    }
}
