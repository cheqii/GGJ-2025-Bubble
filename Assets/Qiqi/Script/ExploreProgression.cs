using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExploreProgression : MonoBehaviour
{
    #region -Slider Progression Variables-

    [Header("Explore Progression Slider")]
    [SerializeField] private Slider progressSlider;
    
    public TextMeshProUGUI progressText;
    
    public float totalDuration = 300f; // Total time (in seconds) to reach 100% when using normal speed
    public float normalSpeed = 1f; // Normal multiplier for speed
    public float slowSpeed = 0.5f; // Slow multiplier for speed when condition is true
    public bool condition;        // Condition to slow down the slider's progress

    private float elapsedTime = 0f; // Tracks the elapsed time
    private float effectiveDuration; // Adjusted duration based on the condition

    #endregion

    [SerializeField] private CharacterExplorer character;

    [SerializeField] private MovementController characterMovement;

    void Start()
    {
        if (progressSlider == null)
        {
            Debug.LogError("Slider is not assigned! Please assign a slider in the Inspector.");
            return;
        }

        // Initialize slider values
        progressSlider.minValue = 0f;
        progressSlider.maxValue = 100f;
        progressSlider.value = 0f;
        
        // Start with normal duration
        effectiveDuration = totalDuration / normalSpeed;
    }

    void Update()
    {
        if(!characterMovement.GetIsGrounded()) return;
        // Adjust the effective duration based on the condition
        effectiveDuration = totalDuration / (condition ? slowSpeed : normalSpeed);

        // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate progress based on elapsed time and effective duration
        float _progress = Mathf.Clamp01(elapsedTime / effectiveDuration);

        // Update the slider value
        progressSlider.value = _progress * 100f;
        
        character.MoveCharacter(_progress);
        
        // Update progress text
        progressText.text = progressSlider.value.ToString("F0") + "%";

        // Optional: Reset when complete
        if (_progress >= 1f)
        {
            Debug.Log("Progress complete!");
            // Finish Stage
        }
    }
    
}
