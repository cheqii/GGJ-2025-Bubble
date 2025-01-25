using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class Planet : MonoBehaviour
{
    [Header("Planet Status")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("Planet Face Sprite")]
    [SerializeField] private GameObject faceSprite;

    [Header("Player")]
    [SerializeField] private MovementController player;

    [SerializeField] private MMF_Player hurtFeedback;
    
    private void Start()
    {
        currentHealth = maxHealth;

        #region > planet scale up start transition

        var _faceTween = faceSprite.transform.DOScale(3, 1f);
        _faceTween.OnComplete(() =>
        {
            // play trigger animation after finish the transition
            var _faceAnimator = faceSprite.GetComponent<Animator>();
            _faceAnimator.SetTrigger("Play");
        });
        
        var _platnetTween = transform.DOScale(5f, 1f);
        _platnetTween.OnComplete(() =>
        {
            // point to soft body after finish the transition
            var _blob = GetComponent<Blob>();
            
            _blob.CreateReferencePoints();
            _blob.CreateMesh();
            _blob.MapVerticesToReferencePoints();
        });

        #endregion

        player.PlanetTakeDamage += TakeDamage;

    }

    private void Update()
    {
        
    }

    private void TakeDamage()
    {
        if (player.currentState == MovementController.State.Jumping)
        {
            var _chargeScript = player.gameObject.GetComponent<ChargeScript>();
            var _jumpDamage = _chargeScript.jumpForce * _chargeScript.JumpCharge;
            currentHealth -= (int) _jumpDamage;
            
            // hurtFeedback.PlayFeedbacks();
        }
    }
}
