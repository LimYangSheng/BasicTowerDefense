using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlow : MonoBehaviour {

    // public attributes of slow enemy
    public float speed;
    public float health;
    public int attack;
    public int bounty;

    // Variables to maintain healthbar
    public Image healthBar;
    private float initialHealth;

    private float changeDirectionTime;
    private float savedTime;

    private bool isMovingLeftOrRight;
    private Vector3 currentDirection;
    private int facingDirection;

    // Use this for initialization
    void Start()
    {
        // Make savedTime the current time
        savedTime = Time.time;

        // Initial moving direction in -z direction
        currentDirection = new Vector3(0, 0, -1);

        // Time set before a change in direction
        changeDirectionTime = Random.Range(1.0f, 2.0f);

        // Set initial health value
        initialHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Is it time to change direction?
        if (Time.time - savedTime >= changeDirectionTime)
        {
            // Yes!
            // Is it moving left or right currently?
            if (isMovingLeftOrRight)
            {
                // Yes! Change moving direction back to -z
                ChangeBackDirection(); 
            }
            else
            {
                // No! Change moving direction to left or right
                ChangeDirection();  
            }
        }

        // Move the enemy in set direction every update
        this.transform.Translate(currentDirection * speed * Time.deltaTime);
    }

    // Change moving direction to either left or right
    void ChangeDirection() {
        // Set boolean to true to make sure the next change in direction changes movement back to -z direction
        isMovingLeftOrRight = true;

        // Randomize and set the moving direction
        facingDirection = (Random.Range(0.0f, 2.0f) <= 1 ? -1 : 1);
        currentDirection = new Vector3(facingDirection, 0, 0);

        // Reset the next time to change direction again
        changeDirectionTime = Random.Range(0.5f, 1.0f);
        savedTime = Time.time;
    }

    // Change moving direction back to -z
    void ChangeBackDirection()
    {
        // Set boolean to false to make sure the next change in direction is to the left or the right
        isMovingLeftOrRight = false;

        // Set moving direction to -z
        currentDirection = new Vector3(0, 0, -1);

        // Reset the next time to change direction again
        changeDirectionTime = Random.Range(1.0f, 2.0f);
        savedTime = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        // Did enemy collide with the end / player damage zone?
        if (other.gameObject.tag == "End")
        {
            // Yes! Destroy the enemy object
            PlayerHealth.healthInstance.DecrementHealth(attack);
            Destroy(gameObject);
        }

        // Did enemy collide into the wall?
        if (other.gameObject.tag == "Edges")
        {
            // Yes! Correct its direction
            CorrectDirection(other);
        }
    }

    // Collide the enemy direction so it does not move pass the wall
    void CorrectDirection(Collider other)
    {
        // Set boolean to true so that the next change in direction will be in the -z direction
        isMovingLeftOrRight = true;
        
        // Is the wall to the right of the enemy?
        if (other.transform.position.x > gameObject.transform.position.x)
        {
            // Yes! Make enemy face left
            facingDirection = -1;
        }
        else
        {
            // No! Make enemy face right
            facingDirection = 1;
        }

        // Set the enemy moving direction to its facing direction
        currentDirection = new Vector3(facingDirection, 0, 0);

        // Reset the next time to change direction again 
        changeDirectionTime = 1.0f;
        savedTime = Time.time;
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
}
