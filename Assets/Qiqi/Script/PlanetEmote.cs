using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public enum PlanetEmotions
{
    Happy = 0,
    Nervous = 1,
    Sad = 2,
    Hurt = 3
}

public class PlanetEmote : MonoBehaviour
{
    #region -Emotions Material-
    
    [Header("Emote Face Sprite and Animation")]
    
    [SerializeField] private SpriteRenderer faceSpriteRenderer;
    [SerializeField] private Animator faceAnimator;
    
    [SerializeField] private List<SpriteAndEmotion> emotions = new List<SpriteAndEmotion>();

    [SerializeField] private PlanetEmotions currentEmote;
    public PlanetEmotions CurrentEmote => currentEmote;

    #endregion
    

    public void ChangeEmote(PlanetEmotions _emote)
    {
        currentEmote = _emote;
        // need transition or something when change the planet emote
        
        if(!faceSpriteRenderer && !faceAnimator) return;
        faceSpriteRenderer.sprite = emotions[(int) _emote].DefaultFaceSprite;
        faceAnimator.runtimeAnimatorController = emotions[(int) _emote].FaceAnimation;
        faceAnimator.SetTrigger("Play");
    }
    
}
