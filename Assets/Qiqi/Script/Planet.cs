using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [SerializeField] private GameObject faceSprite;
    [SerializeField] private GameObject planetSpriteMockup;
    
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

    }

    private void Update()
    {
        
    }
    
}
