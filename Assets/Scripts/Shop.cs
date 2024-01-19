using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] Upgrades[] upgrades;

    [System.Serializable]

    public struct Upgrades
    {
        public float upgradeAmount;

        [Space]

        public string[] titles;
        public long price;
        public int priceMultiplier;
        public int maxLevel;
        [NonSerialized] public int curerntLevel;

        [Space]

        public TextMeshProUGUI priceText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;

    }

    private void Start()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.nameText.text = upgrade.titles[0];
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
            upgrades[i].nameText.text = upgrades[i].titles[upgrades[i].curerntLevel];
            upgrades[i].levelText.text = upgrades[i].curerntLevel.ToString();

            if (upgrades[i].curerntLevel < upgrades[i].maxLevel)
            {
                Abreviate();
            }
            else 
            {
                upgrades[i].priceText.text = string.Empty;
            }
        }
    }

    public void BuyUpgrade(int upgradeNumber)
    {
        if (upgrades[upgradeNumber].curerntLevel >= upgrades[upgradeNumber].maxLevel) { return; }

        upgrades[upgradeNumber].price *= upgrades[upgradeNumber].priceMultiplier;
        upgrades[upgradeNumber].curerntLevel++;
    }

    private void Abreviate()
    {
        for (int i = 0; i < upgrades.Length; i++) 
        {
            string[] units = { "", "K", "M", "B", "T", "QA", "QI"};
            double amount = upgrades[i].price;
            int unitIndex = 0;

            while (amount >= 1000 && unitIndex < units.Length - 1)
            {
                amount /= 1000;
                unitIndex++;
            }

            upgrades[i].priceText.text = amount.ToString("F1") + units[unitIndex];
        }
    }
}
