using System.Collections;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    //UI_Health_System healthUI;
    //Enemy enemyS;

    public int playerHealth;
    public int maxHealth;

    private void Start()
    {
        playerHealth = maxHealth;
    }

    public void TakeDMGPlayer(int amount)
    {
        playerHealth -= amount;
        StartCoroutine(waitAfterHit());

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Remaining Health: " + playerHealth);
    }

    private IEnumerator waitAfterHit()
    {
        Debug.Log("You took dmg");

        Physics2D.IgnoreLayerCollision(9, 10);
        yield return new WaitForSeconds(3);

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}