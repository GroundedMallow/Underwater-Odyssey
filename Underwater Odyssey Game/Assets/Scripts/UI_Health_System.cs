using UnityEngine;
using UnityEngine.UI;

public class UI_Health_System : MonoBehaviour
{
    public int health;
    public int currentHearts;

    [Header("Sprites")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        health = currentHearts;
    }

    private void Update()
    {
        if (health > currentHearts)
        {
            health = currentHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < hearts.Length)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}