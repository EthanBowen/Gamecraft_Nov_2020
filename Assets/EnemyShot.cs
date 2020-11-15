using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyShot : MonoBehaviour
{
    public float speed = 5f;
    public float despawnSeconds = 3f;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
        StartCoroutine(nameof(Timeout));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit player projectile
        if(collision.CompareTag("Projectile"))
        {
            StopCoroutine(nameof(Timeout));
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if(collision.CompareTag("Player"))
        {
            StopCoroutine(nameof(Timeout));
            Destroy(gameObject);
        }
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(despawnSeconds);
        Destroy(gameObject);
    }
}
