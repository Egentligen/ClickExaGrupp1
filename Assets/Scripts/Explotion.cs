using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Explotion : MonoBehaviour
{
    [SerializeField] Sprite[] spriteStages;

    int level = 0;

    private void Update()
    {
        UpgradeBombArea(level);
    }

    public void UpgradeBombArea(int aLevel)
    {
        if (aLevel >= spriteStages.Length) { return; }

        GetComponent<SpriteRenderer>().sprite = spriteStages[aLevel];
        level = aLevel;
    }
}
