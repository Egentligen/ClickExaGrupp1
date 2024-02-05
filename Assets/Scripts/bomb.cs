using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BombClicker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button bombButton;
    public Sprite clickedSprite;
    public Sprite defaultSprite;

    private long score = 0;
    private long scoreOnClick = 1;

    public long GetScore() 
    { 
        return score;
    } 

    private void Start()
    {
        bombButton.onClick.AddListener(OnClickBomb);
        UpdateScoreText();
    }

    private void Update()
    {
        scoreText.text = Abreviation(score);
    }

    private void OnClickBomb()
    {
        bombButton.image.sprite = clickedSprite;
        AddScore(scoreOnClick);
        UpdateScoreText();

        // Schedule the ResetSprite method to be called after a delay
        Invoke("ResetSprite", 0.07f); // Adjust the delay as needed
    }

    private void ResetSprite()
    {
        bombButton.image.sprite = defaultSprite;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Bombs: " + score;
    }

    public void AddScore(long scoreToAdd) 
    {
        score += scoreToAdd;
    }

    private string Abreviation(long number) 
    {
        string[] units = { "", " Million", " Billion", " Trillion", " Quadrillion", " Quintillion" };
        double amount = number;
        int unitIndex = 0;

        while (amount >= 1000 && unitIndex < units.Length - 1)
        {
            amount /= 1000;
            unitIndex++;
        }

        if (score > 999999)
        {
             return amount.ToString("0.###") + units[unitIndex];
        }
        else
        {
            return amount + units[unitIndex];
        }
    }
}

