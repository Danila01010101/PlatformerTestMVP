using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject 
        deathChunkParticle,
        deathBloodParticle;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        //Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        //Instantiate(deathChunkParticle,transform.position, deathChunkParticle.transform.rotation);
        Destroy(gameObject);
    }
}
