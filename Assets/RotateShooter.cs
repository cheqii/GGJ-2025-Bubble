using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateShooter : MonoBehaviour
{
    public float speed = 5f;

    public GameObject explorer;

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(explorer, spawnPoint.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + speed), 0.2f, RotateMode.FastBeyond360);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z - speed), 0.2f, RotateMode.FastBeyond360);
        }
    }
}
