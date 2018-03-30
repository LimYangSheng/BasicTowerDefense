using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash : MonoBehaviour {

    public static Cash cashLogic;

    // Public variables to manage cash
    public int startingCash;
    public Text cashText;

    private int currentCash;

    void Awake()
    {
        cashLogic = this;

        // Initialize the starting cash value
        currentCash = startingCash;
        cashText.text = "$" + currentCash.ToString();
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Decrement the cash value if possible
    public bool DeductCash(int value)
    {
        // Is there enough cash?
        if (currentCash >= value)
        { 
            // Yes! Decrement cash and proceed on with spending it
            currentCash -= value;
            cashText.text = "$" + currentCash.ToString();
            return true;
        }

        // No! Permission not granted to spend
        return false;
    }

    // Increment the cash value
    public void IncrementCash(int value)
    {
        // Update the cash value
        currentCash += value;
        cashText.text = "$" + currentCash.ToString();
    }
}
