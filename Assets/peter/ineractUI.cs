using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ineractUI : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas containing the UI elements
    private bool isCanvasVisible = false;

    private void Start()
    {
        canvas.enabled = false; // Ensure the canvas is initially hidden
    }

    private void Update()
    {
        // Check for the interaction input (e.g., a button press)
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCanvas();
        }
    }

    private void ToggleCanvas()
    {
        isCanvasVisible = !isCanvasVisible;
        canvas.enabled = isCanvasVisible;
    }
}
