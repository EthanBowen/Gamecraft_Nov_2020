using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody2D))]
public class ShotController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator animator;

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
        if(!collision.CompareTag("Player"))
        {
            StopCoroutine(nameof(Timeout));
            rb.velocity = Vector2.zero;
            Destroy(collision.gameObject);
            animator.SetBool("Collided", true);
        }
    }

    private IEnumerator Timeout()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void Finish()
    {
        Destroy(gameObject);
    }
}
