using UnityEngine;
using UnityEngine.UI;

public class Health_System : MonoBehaviour
{
    public int health;
    public int currentHearts;

    [Header("Sprites")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        if(health > currentHearts)
        {
            health = currentHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < hearts.Length)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDMG(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
