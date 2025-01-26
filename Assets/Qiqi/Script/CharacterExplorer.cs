using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterExplorer : MonoBehaviour
{
    public Transform character; // The blue square (character) transform
    public Transform planet;    // The planet transform (used as the center of the orbit)

    public float orbitRadius = 2f; // The distance from the center of the planet to the character

    void Start()
    {
        if (character != null && planet != null) return;
        Debug.LogError("Please assign the Character, and Planet in the Inspector.");
    }
    
    
    public void MoveCharacter(float progress)
    {
        if (character == null)
        {
            character = GameObject.FindGameObjectWithTag("Player").transform;
        }
        // Calculate the angle (0 to 360 degrees) based on progress
        float angle = Mathf.Lerp(0f, 360f, progress);

        // Offset the angle by 90 degrees to start from the top
        float adjustedAngle = (angle - 90f) * -1;

        // Convert the adjusted angle to radians
        float angleRad = adjustedAngle * Mathf.Deg2Rad;

        // Calculate the character's position around the planet
        Vector3 offset = new Vector3(
            Mathf.Cos(angleRad) * orbitRadius,
            Mathf.Sin(angleRad) * orbitRadius,
            0f // Assuming 2D movement; adjust if needed for 3D
        );

        // Set the character's position relative to the planet
        character.position = planet.position + offset;
    }
}
