using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour
{
    [SerializeField] Vector2[] locations;
    [SerializeField] GameObject explotion;

    int level = 0;

    private void Start()
    {
        SetNewLocation();
    }

    public void SetNewLocation() 
    {
        if (level >= locations.Length) { return; }

        Instantiate(explotion, locations[level], Quaternion.identity);
        level++;
    }
}
