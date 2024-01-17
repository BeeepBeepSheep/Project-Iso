using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPoolBar : MonoBehaviour
{
    [SerializeField] Image bar; // Reference to the Image component representing the pool bar

    ValuePool targetPool; // Reference to the ValuePool object associated with this UI pool bar

    // Method to show the UI pool bar and link it to a specific ValuePool
    public void Show(ValuePool targetPool)
    {
        this.targetPool = targetPool; // Assigns the provided ValuePool to the targetPool variable
        gameObject.SetActive(true); // Activates the UI pool bar
    }

    // Method to clear the UI pool bar and disconnect it from any ValuePool
    public void Clear()
    {
        this.targetPool = null; // Removes the association with any ValuePool
        gameObject.SetActive(false); // Deactivates the UI pool bar
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks if a target pool is assigned to this UI pool bar
        if (targetPool == null)
        {
            return; // Exits the method if no target pool is set
        }

        // Updates the fill amount of the bar based on the current and maximum values of the associated ValuePool
        bar.fillAmount = Mathf.InverseLerp(0f, targetPool.maxValue.integer_value, targetPool.currentValue);
        // Mathf.InverseLerp maps the current value in the range between 0 and the maximum value to determine the fill amount
    }
}
