using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DummyController : MonoBehaviour
{
    public int HP = 3;
    public float speed = 2f;
    public float rotateSpeed = 2f;

    private Transform player;
    private bool wasActive;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        if(OnScreen())
        {
            wasActive = true;
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
        else
        {
            if (wasActive)
                Invoke(nameof(RemoveFromScene), 1.0f);
        }
    }

    private bool OnScreen()
    {
        var point = Camera.main.WorldToViewportPoint(transform.position);
        // https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html
        return point.x >= 0 && point.x <= 1 && point.y >= 0 && point.y <= 1;
    }

    private void MoveTowardsPlayer()
    {
        // Passed the player, continue forward
        if ((transform.position - player.position).x < 0)
            transform.position += transform.right * speed * Time.deltaTime;
        else
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void RotateTowardsPlayer()
    {
        float t = Time.deltaTime * speed;
        if ((transform.position - player.position).x < 0)
            t = Time.deltaTime * 0.5f;

        // https://answers.unity.com/questions/650460/rotating-a-2d-sprite-to-face-a-target-on-a-single.html
        Vector3 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, t);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Projectile"))
            ReceiveDamage(1);
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
