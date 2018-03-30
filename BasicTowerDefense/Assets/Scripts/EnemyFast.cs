using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFast : MonoBehaviour {

    // Public attributes of the fast enemy
    public float speed;
    public float health;
    public int attack;
    public int bounty;

    // Variables to maintain healthbar
    public Image healthBar;
    private float initialHealth;

	// Use this for initialization
	void Start ()
    {
        // Set initial health value
        initialHealth = health;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Set the moving direction to -z direction
        Vector3 direction = new Vector3(0, 0, -1);

        // Move the enemy in -z direction every update
        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    // Enemy taking damage from bullet
    public void TakeDamage(float damage)
    {
        // Decrease health
        health -= damage;

        // Is the enemy dead?
        if (health <= 0)
        {
            // Yes! Destroy enemy object
            EnemyDeath();
            return;
        }

        // Update healthbar image
        healthBar.fillAmount = health / initialHealth;
    }

    // Destroy enemy and give cash to player
    private void EnemyDeath()
    {
        Cash.cashLogic.IncrementCash(bounty);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // Did the enemy collide with the end / player damage zone?
        if (other.gameObject.tag == "End")
        {
            // Yes! Do damage and Destroy the enemy object
            PlayerHealth.healthInstance.DecrementHealth(attack);
            Destroy(gameObject);
        }
    }
}
