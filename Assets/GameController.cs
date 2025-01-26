using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Planet planet;
    public TextMeshProUGUI progressText;
    public Slider progressSlider;

    private void Update()
    {
        float currentProgress = planet.transform.rotation.eulerAngles.z / 3.6f;
        progressSlider.value = currentProgress;
        progressText.text = Convert.ToInt32(progressSlider.value) + "%";

    }
}
