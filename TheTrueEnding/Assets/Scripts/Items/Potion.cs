using System.Collections;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]
    private int _speed = 2;
    [SerializeField]
    private int _durationInSeconds = 2;

    public int Speed
    {
        get 
        { 
            return _speed; 
        }
    }
    public int DurationInSeconds
    {
        get
        {
            return this._durationInSeconds;
        }
    }
}
