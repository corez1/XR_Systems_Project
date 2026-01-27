using UnityEngine;
// For VR input
using UnityEngine.InputSystem;

public class ColorChanger : MonoBehaviour
{
    // To add VR button
    public InputActionReference changeColorAction;
    // Variable to hold the light component
    private Light targetLight;
    void Start()
    {
        // Get the light component for the linked object
        targetLight = GetComponent<Light>();

        // If configured button is pressed calls ChangeTheColor()
        if (changeColorAction != null)
        {
            changeColorAction.action.Enable();
            changeColorAction.action.performed += (ctx) =>
            {
                ChangeTheColor();
            };
        }
    }

    void ChangeTheColor()
    {
        // Chagne target light to random color
        targetLight.color = new Color(Random.value, Random.value, Random.value);
    }
}
