using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scorePerSecondText;

    [Header("Colors")]
    [SerializeField] Color maxColor = Color.black;
    [SerializeField] Color toExpenciveColor = Color.red;
    [SerializeField] Color canAffordColor = Color.green;

    [SerializeField] Upgrades[] upgrades;

    BombClicker bombClicker;

    private float timer = 1;
    private float timerAtStart;
    private long scoreToAdd;

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

        public bool useDescriptions;

        public long scorePerSecond;

        [Space]

        public TextMeshProUGUI priceText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
    }

    private void Awake()
    {
        bombClicker = FindObjectOfType<BombClicker>();

        timerAtStart = timer;
    }

    private void Start()
    {
        foreach (Upgrades upgrade in upgrades)
        {
            if (upgrade.useDescriptions)
            {
                upgrade.nameText.text = upgrade.descriptions[0];
            }
        }
    }

    private void Update()
    {
        UpdateShop();
        Towers();
    }

    private void UpdateShop()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            Upgrades upgrade = upgrades[i];

            if (upgrade.useDescriptions) 
            {
                upgrade.nameText.text = upgrade.title + ":\n" + upgrade.descriptions[upgrade.currentLevel];
            }
            if (!upgrade.useDescriptions)
            {
                upgrade.nameText.text = upgrade.title;
            }


            upgrade.levelText.text = upgrade.currentLevel.ToString();

            if (upgrade.currentLevel >= upgrade.maxLevel)
            {
                upgrade.priceText.text = "MAX";
                upgrade.priceText.color = maxColor;
            }
            else
            {
                upgrade.priceText.color = bombClicker.GetScore() < upgrade.price ? toExpenciveColor : canAffordColor;

                Abreviate(i);
            }
        }
    }

    private void Towers() 
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            scoreToAdd = 0;
            timer = timerAtStart;

            foreach (var upgrade in upgrades)
            {
                for (int i = 0; i < upgrade.currentLevel; i++)
                {
                    scoreToAdd += upgrade.scorePerSecond;
                }
            }

            scorePerSecondText.text = scoreToAdd.ToString();
            bombClicker.AddScore(scoreToAdd);
        }
    }

    public void BuyUpgrade(int upgradeNumber)
    {
        if (upgrades[upgradeNumber].currentLevel == upgrades[upgradeNumber].maxLevel 
            || bombClicker.GetScore() < upgrades[upgradeNumber].price) { return; }

        bombClicker.AddScore(-upgrades[upgradeNumber].price);
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

        if (upgrades[upgradeNumber].price > 9999)
        {
            upgrades[upgradeNumber].priceText.text = amount.ToString("0.###") + units[unitIndex];
        }
        else
        {
            upgrades[upgradeNumber].priceText.text = amount + units[unitIndex];
        }
    }
}
