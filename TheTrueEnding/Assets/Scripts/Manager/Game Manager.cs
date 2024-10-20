using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Knight _knight;
    private void Awake()
    {
        this._knight = FindAnyObjectByType<Knight>();
    }
}
