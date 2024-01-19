using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    long money;

    private void Update()
    {
        Abreviate();
    }

    public void AddMoney(int moneyToAdd) 
    { 
        ClickPopup popup = FindObjectOfType<ClickPopup>();

        money += moneyToAdd;
        popup.SpawnClickPupup(moneyToAdd);
    }

    private void Abreviate()
    {
        string[] units = { "", " Million", " Billion", " Trillion", " Quadrillion", " Quintillion" };
        double amount = money;
        int unitIndex = 0;

        while (amount >= 1000 && unitIndex < units.Length - 1)
        {
            amount /= 1000;
            unitIndex++;
        }

        moneyText.text = amount.ToString("F2") + units[unitIndex] + " $";
    }
}
