using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public static PlayerHealth healthInstance;

    // Public variables to manage health of player
    public int startingHealth;
    public Text playerHealthText;

    private int currentHealth;

    void Awake()
    {
        healthInstance = this;

        // Initialize the starting heath of player
        currentHealth = startingHealth;
        playerHealthText.text = currentHealth.ToString();
    }

    // Use this for initialization
    void Start () {
        // Check if health <= 0 every 0.2s
        InvokeRepeating("CheckHP", 0f, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void CheckHP()
    {
        // Is health lower than 0 or is game over?
        if (currentHealth <= 0)
        {
            // Yes! Show game over display UI
            GameOver.gameOverUI.DisplayGameOverUI();
        }
    }

    // Decrease the health of player
    public void DecrementHealth(int damage)
    {
        // Decrease health of player and update its displayed value
        currentHealth -= damage;
        playerHealthText.text = currentHealth.ToString();
    }
}
