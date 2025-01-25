using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{
    private float cooldown = 1.0f;
    private float MaxCooldown = 1.0f;
    float currCountdownValue;

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer == 7){
           Destroy(collision.gameObject); 
        }
    }
    void Start(){
        cooldown = MaxCooldown;
    }

    public void Attacking(){
        gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown(float countdownValue = 0.5f)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(countdownValue);
            currCountdownValue--;
        }
        gameObject.SetActive(false);
    }
}
