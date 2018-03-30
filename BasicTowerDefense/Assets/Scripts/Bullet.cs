using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Target for this bullet object
    private GameObject target;

    // Public attributes of the bullet
    public GameObject particleEffect;
    private float damage;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Is there a valid target?
        if (target == null)
        {
            // No! Place bullet back into the pool
            gameObject.SetActive(false);
            return;
        }
        // Set movement direction of the bullet
        Vector3 direction = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Is the bullet going to reach the target in this frame?
        if (distanceThisFrame >= direction.magnitude)
        {
            // Yes! Hit the target
            HitTarget();
            return;
        }

        // Move the bullet in the direction every update
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}

    // Sets the bullet with its target and its damage that is passed on from its turret
    public void SeekTarget(GameObject target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    // Upon hitting the target...
    private void HitTarget()
    {
        // Spawn particle effect
        GameObject particleEffectReference = (GameObject)Instantiate(particleEffect, target.transform.position, target.transform.rotation);
        // Destroy the particle effect game object after 1.5 seconds
        Destroy(particleEffectReference, 1.5f);

        // Is it a fast enemy?
        if (target.GetComponent<EnemyFast>() != null)
        {
            // Yes! Do damage to fast enemy type
            target.GetComponent<EnemyFast>().TakeDamage(damage);
        }
        else
        {
            // No! Do damage to slow enemy type
            target.GetComponent<EnemySlow>().TakeDamage(damage);
        }

        // Place the bullet back into the pool
        gameObject.SetActive(false);
    }
}
