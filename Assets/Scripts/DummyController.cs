using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DummyController : MonoBehaviour
{
    public int HP = 3;
    public float speed = 2f;

    private Transform player;
    private bool wasActive;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var rot = transform.rotation.eulerAngles;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }


    // Update is called once per frame
    void Update()
    {
        if(OnScreen() && player)
        {
            if(!wasActive)
                gameObject.AddComponent<Scroller>();
            wasActive = true;
            MoveTowardsPlayer();
        }
        else
        {
            if (wasActive)
            {
                // Keep moving forward
                transform.position += transform.right * speed * Time.deltaTime;
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

    private void MoveTowardsPlayer()
    {
        // Passed the player, continue forward
        if ((transform.position - player.position).x < 0)
            transform.position += transform.right * speed * Time.deltaTime;
        else
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
            ReceiveDamage(HP);
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
