using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public int HP = 1;
    public float rotateSpeed = 2f;
    [Range(0, 1)]
    public float shotSpeed = 0.5f;
    public GameObject shot;

    private Transform player;
    private bool wasActive;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (OnScreen() && player)
        {
            if (!wasActive)
                InvokeRepeating(nameof(Shoot), 0.0f, shotSpeed);
            wasActive = true;
            RotateTowardsPlayer();
        }
        else
        {
            if (wasActive)
            {
                Invoke(nameof(RemoveFromScene), 1.0f);
            }
        }
    }

    private bool OnScreen()
    {
        var point = Camera.main.WorldToViewportPoint(transform.position);
        // https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html
        return point.x >= 0 && point.x <= 1 && point.y >= 0 && point.y <= 1;
    }

    private void RotateTowardsPlayer()
    {
        float t = Time.deltaTime * rotateSpeed;

        // https://answers.unity.com/questions/650460/rotating-a-2d-sprite-to-face-a-target-on-a-single.html
        Vector3 vectorToTarget = transform.position - player.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, t);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            ReceiveDamage(HP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
            ReceiveDamage(1);
    }

    private void Shoot()
    {
        var rot = transform.rotation.eulerAngles;
        rot.z += 90;
        Instantiate(shot, transform.position, Quaternion.Euler(rot));
    }

    private void ReceiveDamage(int points)
    {
        HP -= points;
        if (HP <= 0)
            Destroy(gameObject);
    }

    private void RemoveFromScene()
    {
        Destroy(gameObject);
    }
}
