using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BombClicker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button bombButton;
    public Sprite clickedSprite;
    public Sprite defaultSprite;

    private int score = 0;

    private void Start()
    {
        bombButton.onClick.AddListener(OnClickBomb);
        UpdateScoreText();
    }

    private void OnClickBomb()
    {
        bombButton.image.sprite = clickedSprite;
        score++;
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
}

