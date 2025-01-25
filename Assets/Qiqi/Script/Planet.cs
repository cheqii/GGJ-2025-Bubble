using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Planet : MonoBehaviour
{
    #region -Planet Status-
    
    [Header("Planet Status")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    #endregion

    [Header("Planet Face Sprite")]
    [SerializeField] private GameObject faceSprite;

    [Header("Player")]
    [SerializeField] private MovementController player;
    
    [Header("Planet Core Collider")]
    [SerializeField] private Collider2D planetCentralCol;
    
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player hurtFeedback;
    
    private PlanetEmote planetEmote;
    
    private void Start()
    {
        currentHealth = maxHealth;

        planetEmote = GetComponent<PlanetEmote>();
        
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

    private void CheckHealthState()
    {
        
    }

    private void TakeDamage()
    {
        planetEmote.ChangeEmote(PlanetEmotions.Hurt);
        // if (!player.GetIsGrounded()) return;
        var _chargeScript = player.gameObject.GetComponent<ChargeScript>();
        var _jumpDamage = _chargeScript.JumpCharge;
        currentHealth -= (int) _jumpDamage;
            
        Material _material = GetComponent<MeshRenderer>().material;
        var _matTween = _material.DOColor(Color.red, 0.25f);
        _matTween.OnComplete(() =>
        {
            _material.DOColor(Color.white, 0.25f);
            planetEmote.ChangeEmote(planetEmote.TempEmote);
        });
        
        hurtFeedback.PlayFeedbacks();
    }
    
    private void RotatePlanet()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var _zRotateValue = player.GetComponent<ChargeScript>().JumpCharge;
        if (mousePosition.x > player.transform.position.x)
        {
            // right side
            _zRotateValue *= 1;
        }
        else if (mousePosition.x < player.transform.position.x)
        {
            // left side
            _zRotateValue *= -1;
        }
        // var _zRotateValue = player.GetComponent<ChargeScript>().JumpCharge;
        planetCentralCol.isTrigger = false;
        var _rotateTween = transform.DORotate(new Vector3(0, 0, gameObject.transform.localRotation.eulerAngles.z +  _zRotateValue), 0.25f, RotateMode.FastBeyond360);
        _rotateTween.OnComplete(() =>
        {
            planetCentralCol.isTrigger = true;
        });
    }
}
