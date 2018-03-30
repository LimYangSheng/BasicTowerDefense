using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour {
    // Variables for lock on enemy
    public string enemyTag = "Enemy";
    private GameObject nearestEnemy = null;
    public Transform pointOfRotation;
    
    // Variables for firing / bullet
    private float fireCoolDownTime;
    public GameObject bulletPrefab;
    public Transform pointOfFire;

    // Variables for firing audio
    public AudioClip firingSound;
    private AudioSource source;

    // Public attributes of the turret
    [Header("Attributes")] 
    public float range;
    public float damage;
    public float rotationSpeed;
    public float fireRate; // how many shots per second
    public int cost;
    public int upgradeCost;

    // Variables for upgrading and sale
    private int turretLevel;
    private int upgradeBasePrice;
    private int sellPrice;

    // Use this for initialization
    void Start () {
        // Initialize values
        turretLevel = 1;
        upgradeBasePrice = 50;
        sellPrice = cost / 2;
        fireCoolDownTime = 0f;
        source = GetComponent<AudioSource>();

        // Check for and lock on to enemy every 0.2s
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        // Is there an enemy in range?
        if (nearestEnemy != null)
        { 
            // Yes! Rotate turret to face the enemy
            RotateTurret();

            // Can the turret fire again now?
            if (fireCoolDownTime <= 0f)
            {
                // Yes! Fire!
                Shoot();

                // Reset firing cool down
                fireCoolDownTime = 1f / fireRate;
            }

            // Decrease cool down time every update
            fireCoolDownTime -= Time.deltaTime;
        }   
	}

    // Fire the turret
    void Shoot()
    {
        // Play the firing sound only if game has not ended
        if (!GameOver.gameOverUI.isGameOver())
        {
            source.PlayOneShot(firingSound, 1);
        }

        // Spawn bullet from object pool
        GameObject tempBullet = ObjectPooler.objectPool.GetPooledObject();
        // Is there a valid bullet object?
        if (tempBullet != null)
        {
            // Yes! Set its location and rotation and make it visible
            tempBullet.transform.position = transform.position;
            tempBullet.transform.rotation = transform.rotation;
            tempBullet.SetActive(true);
        }
        Bullet bullet = tempBullet.GetComponent<Bullet>();
    // Move towards the target
        bullet.SeekTarget(nearestEnemy, damage);
    }

    // Check for and lock on to enemy
    void UpdateTarget()
    {
        // Find all object in a sphere range from the turret
        Collider[] objectInRange = Physics.OverlapSphere(transform.position, range);
        float minDistanceToEnemy = Mathf.Infinity;

        // Loop through all objects in range
        for (int i = 0; i < objectInRange.Length; i++)
        {
            nearestEnemy = null;
            // Is the object an enemy?
            if (objectInRange[i].gameObject.tag == enemyTag)
            {
                // Yes! Calculate distance to enemy
                float distanceToEnemy = Vector3.Distance(transform.position, objectInRange[i].transform.position);

                // Is this the closest enemy so far?
                if (distanceToEnemy < minDistanceToEnemy)
                {
                    // Yes! Set object as nearest enemy (so far) and the enemy distance as the min distance (so far)
                    minDistanceToEnemy = distanceToEnemy;
                    nearestEnemy = objectInRange[i].gameObject;
                }
            }
        }
    }

    // Rotate the turret towards the enemy
    void RotateTurret()
    {
        // Get vector direction from turret to enemy
        Vector3 direction = nearestEnemy.transform.position - transform.position;

        // Find rotation value for that direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate from current position to the vector direction rotation
        Vector3 rotation = Quaternion.Lerp(pointOfRotation.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        pointOfRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    // Improve the turret attributes
    public void UpgradeTurretAttributes()
    {
        if (CanBeUpgraded())
        {
            // Yes! Upgrade the values
            range += 2;
            fireRate += 0.5f;
            damage += 3;
            turretLevel++;
            sellPrice = sellPrice + (upgradeCost / 2);
            upgradeCost = upgradeBasePrice * turretLevel;
        }
    }

    // Check if the turret can still be upgraded
    private bool CanBeUpgraded()
    {
        // Yes!
        if (turretLevel < 4)
        {
            return true;
        }
        // No!
        return false;
    }

    // Sell and destroy the turret
    public int Sell()
    {
        Destroy(gameObject);
        return sellPrice;
    }

    // Return upgrade cost for turret
    public int GetUpgradeCost()
    {
        return upgradeCost;
    }

    // Return sell price for turret
    public int GetSellPrice()
    {
        return sellPrice;
    }

}
