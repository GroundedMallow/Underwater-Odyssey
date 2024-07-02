using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int currentHealth;


    [Header("Player")]
    public Health_Player playerHealth;
    public int dmg;

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDMG(int dmgTaken)
    {
        currentHealth -= dmgTaken;

        if(currentHealth < 0)
        {
            Debug.Log("Enemy died");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDMGPlayer(dmg);
        }
    }
}
