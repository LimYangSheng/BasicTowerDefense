using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour {

    public static TurretUI turretUI;

    // Variables to set up the turret UI
    public GameObject ui;
    private Base target;

    // Variables to update text value
    public Text upgradeText;
    public Text sellText;

    // Variables to interact with turret UI
    public Button upgradeButton;
    public Button sellButton;

    void Awake()
    {
        turretUI = this;
    }

    // Set the base block to be the target of the turret UI
    public void SetTarget(Base _target)
    {
        // Is the player clicking on the same base block?
        if (target == _target)
        {
            // Yes! Toggle turret UI display
            ToggleDisplayTurretUI();
        }
        else
        {
            // No! Set new target base block
            target = _target;
            // Set position of turret UI
            transform.position = target.transform.position + target.GetTurretSpawnOffset();
            DisplayUI();
        }
    }

    // Upgrade the selected turret
    public void UpgradeSelectedTurret()
    {
        target.UpgradeTurret();
        HideUI();
    }

    // Update and show correct upgrade price
    public void UpdateUpgradeText()
    {
        upgradeText.text = "<b>UPGRADE</b>\n" + "$" + target.GetUpgradeCostOfTurret().ToString();
    }

    // Sell the turret
    public void SellSelectedTurret()
    {
        target.SellTurret();
        HideUI();
    }

    // Update and show correct sell price
    public void UpdateSellText()
    {
        sellText.text = "<b>SELL</b>\n" + "$" + target.GetSellPriceOfTurret().ToString();
    }

    // Hide the display of turret UI
    public void HideUI()
    {
        ui.SetActive(false);
    }

    // Display of turret UI with corrected upgrade and sell values
    public void DisplayUI()
    {
        UpdateUpgradeText();
        UpdateSellText();
        ui.SetActive(true);
    }

    // Return display visibility
    public bool CheckDisplayVisibility()
    {
        return ui.activeSelf;
    }

    // Display turret UI if it is hidden, else hide turret UI
    public void ToggleDisplayTurretUI()
    {
        // Is the turret display shown?
        if (CheckDisplayVisibility())
        {
            // Yes! Turn display off
            HideUI();
        }
        else
        {
            // No! Turn display on
            DisplayUI();
        }
    }

}
