using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public int playerDamage = 1;
    public Transform cursorTransform;
    public GameObject bulletPrefab;
    public LayerMask hitLayer;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - cursorTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cursorTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        //RaycastHit2D hit = Physics2D.Raycast(cursorTransform.position, cursorTransform.up, 10f,hitLayer);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(bulletPrefab, cursorTransform.position, cursorTransform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * 100f;
            //hit.collider.GetComponent<AstroObject>().TakeDamage(playerDamage);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(cursorTransform.position, cursorTransform.up * 10f);
    }
}
