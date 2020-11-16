using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

// These are the Player's shots
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public float speed = 5f;
    public float despawnSeconds = 2f;
    public AudioClip shotHitClip;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.velocity = new Vector2(speed, 0);
        StartCoroutine(nameof(Timeout));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player") && !collision.CompareTag("Projectile"))
        {
            source.clip = shotHitClip;
            source.Play();
            StopCoroutine(nameof(Timeout));
            rb.velocity = Vector2.zero;
            animator.SetBool("Collided", true);
        }
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(despawnSeconds);
        Destroy(gameObject);
    }

    public void Finish()
    {
        Destroy(gameObject);
    }
}
