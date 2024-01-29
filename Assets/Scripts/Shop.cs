using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shop : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color maxColor = Color.black;
    [SerializeField] Color toExpenciveColor = Color.red;
    [SerializeField] Color canAffordColor = Color.green;

    [Header("Upgrades")]
    [SerializeField] Upgrades[] upgrades;

    [System.Serializable]

    public struct Upgrades
    {
        public float upgradeAmount;

        [Space]

        public string title;
        public string[] descriptions;

        public long price;
        public int priceMultiplier;

        public int maxLevel;
        public int currentLevel;

        [Space]

        public TextMeshProUGUI priceText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;

    }

    private void Start()
    {
        foreach (Upgrades upgrade in upgrades)
        {
            upgrade.nameText.text = upgrade.descriptions[0];
        }
    }

    private void Update()
    {
        UpdateShop();
    }

    private void UpdateShop()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].nameText.text = upgrades[i].title + " " + upgrades[i].descriptions[upgrades[i].currentLevel];
            upgrades[i].levelText.text = upgrades[i].currentLevel.ToString();

            if (upgrades[i].currentLevel >= upgrades[i].maxLevel)
            {
                upgrades[i].priceText.text = "MAX";
                upgrades[i].priceText.color = maxColor;
            }
            else
            {
                Abreviate(i);
                upgrades[i].priceText.color = canAffordColor;
            }
        }
    }

    public void BuyUpgrade(int upgradeNumber)
    {
        if (upgrades[upgradeNumber].currentLevel == upgrades[upgradeNumber].maxLevel) { return; }

        upgrades[upgradeNumber].currentLevel++;
        upgrades[upgradeNumber].price *= upgrades[upgradeNumber].priceMultiplier;
    }

    private void Abreviate(int upgradeNumber)
    {
        string[] units = { "", "K", "M", "B", "T", "QA", "QI" };
        double amount = upgrades[upgradeNumber].price;
        int unitIndex = 0;

        while (amount >= 1000 && unitIndex < units.Length - 1)
        {
            amount /= 1000;
            unitIndex++;
        }

        upgrades[upgradeNumber].priceText.text = amount.ToString("F1") + units[unitIndex];
    }
}
