using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Planet : MonoBehaviour
{
    [Header("Planet Status")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("Planet Face Sprite")]
    [SerializeField] private GameObject faceSprite;

    [Header("Player")]
    [SerializeField] private MovementController player;

    [SerializeField] private Collider2D planetCentralCol;
    
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
        player.RotatePlanet += RotatePlanet;

    }

    private void Update()
    {
        
    }

    private void TakeDamage()
    {
        // if (!player.GetIsGrounded()) return;
        var _chargeScript = player.gameObject.GetComponent<ChargeScript>();
        var _jumpDamage = _chargeScript.startJumpForce * _chargeScript.JumpCharge;
        currentHealth -= (int) _jumpDamage;
            
        Material _material = GetComponent<MeshRenderer>().material;
        var _matTween = _material.DOColor(Color.red, 0.25f);
        _matTween.OnComplete(() =>
        {
            _material.DOColor(Color.white, 0.25f);
        });
        
        // hurtFeedback.PlayFeedbacks();
    }
    
    private void RotatePlanet()
    {
        var _zRotateValue = player.GetComponent<ChargeScript>().JumpCharge;
        planetCentralCol.isTrigger = false;
        var _rotateTween = transform.DORotate(new Vector3(0, 0, gameObject.transform.localRotation.eulerAngles.z +  _zRotateValue), 0.25f, RotateMode.FastBeyond360);
        _rotateTween.OnComplete(() =>
        {
            planetCentralCol.isTrigger = true;
        });
    }
}
