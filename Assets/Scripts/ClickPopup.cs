using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickPopup : MonoBehaviour
{
    [SerializeField] GameObject clickPopup;
    [SerializeField] float popupGravity;

    public void SpawnClickPupup(long moneyPerClick)
    {
        Vector2 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject popup = (GameObject)Instantiate(clickPopup, spawnPos, Quaternion.identity);

        string[] units = { "" ,"K" ,"M" ,"B" ,"T" ,"QA" , "QI" };
        int unitIndex = 0;

        while (moneyPerClick >= 1000 && unitIndex < units.Length - 1)
        {
            moneyPerClick /= 1000;
            unitIndex++;
        }

        popup.transform.GetChild(0).GetComponent<TextMesh>().text = moneyPerClick.ToString("F2") + units[unitIndex];

        popup.transform.position = (Vector2)popup.transform.position - new Vector2(popup.transform.position.x, popup.transform.position.y + popupGravity);
        popup.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0f);
    }
}
