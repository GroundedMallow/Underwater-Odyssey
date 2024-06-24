using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DMG : MonoBehaviour
{
    public int health;
    public int currentHealth;

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
}
