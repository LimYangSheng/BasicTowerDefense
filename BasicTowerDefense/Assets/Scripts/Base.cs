using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    // Variables to set the colour of the base
    public Color hoverOverColor;
    private Color startingColor;
    private Renderer baseRenderer;

    // Variables to help with spawning the turret
    private GameObject turret;
    public GameObject turretPrefab;
    private Vector3 turrentSpawnOffset;

    // Variables to handle turret UI
    private bool isTurretUIDisplayShown;

	// Use this for initialization
	void Start () {
        // Set the initial colour variable
        baseRenderer = GetComponent<Renderer>();
        startingColor = baseRenderer.material.color;

        // Set turrent offset values on spawn
        turrentSpawnOffset = new Vector3(0.0f, 1.5f, 0.0f);
	}

    // Change colour of base block when mouse enters the base block. This makes the game responsive to the player
    private void OnMouseEnter()
    {
        // We only allow change of colour when turret UI display is hidden
        // Is turret UI hidden?
        if (!TurretUI.turretUI.CheckDisplayVisibility())
        {
            // Yes! Change colour
            baseRenderer.material.color = hoverOverColor;
        }
    }

    // Change colour of base block back to original when mouse exits the base block. This makes the game responsive to the player
    private void OnMouseExit()
    {
        baseRenderer.material.color = startingColor;
    }

    private void OnMouseDown()
    {
        // Is there a turret on the base block?
        if (turret != null)
        {
            // Yes! Set base block reference for turret UI
            TurretUI.turretUI.SetTarget(this);
        }
        else
        {
            // No!
            // Is the turret UI hidden? We only allow building when it is hidden
            if (!TurretUI.turretUI.CheckDisplayVisibility())   
            {
                // Yes! 
                // Is there enough cash to build the turret?
                if (Cash.cashLogic.DeductCash(turretPrefab.GetComponent<Turret>().cost))
                {
                    // Yes!
                    // Spawn turret on the block 
                    turret = (GameObject)Instantiate(turretPrefab, transform.position + turrentSpawnOffset, transform.rotation);
                }
            }
        }
    }

    // Uprade the turret build on the base block
    public void UpgradeTurret()
    {
        // Is there enough cash to upgrade the turret?
        if (Cash.cashLogic.DeductCash(turret.GetComponent<Turret>().upgradeCost))
        {
            // Yes! Upgrade turret on the block
            turret.GetComponent<Turret>().UpgradeTurretAttributes();
        }
    }

    public void SellTurret()
    {
        int sellPrice = 0;
        // Is turret not null?
        if (turret != null)
        {
            // Yes!
            sellPrice = turret.GetComponent<Turret>().Sell();
            Cash.cashLogic.IncrementCash(sellPrice);
            turret = null;
        }
    }

    // Return upgrade cost for turret on base block
    public int GetUpgradeCostOfTurret()
    {
        return turret.GetComponent<Turret>().GetUpgradeCost();
    }

    // Return sell price for turret on base block
    public int GetSellPriceOfTurret()
    {
        return turret.GetComponent<Turret>().GetSellPrice();
    }

    // Return offset for spawning turret (used for TurretUI)
    public Vector3 GetTurretSpawnOffset()
    {
        return turrentSpawnOffset;
    }
}
