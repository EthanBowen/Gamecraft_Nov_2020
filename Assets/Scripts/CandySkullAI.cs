using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CandySkullAI : MonoBehaviour
{
    public int HP = 2;
    public float speed = 2f;
    public GameObject shot;
    public Transform shootPoint;

    private Transform player;
    private bool wasActive;

    private Vector2 firingPoint;
    private bool cooldown;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        firingPoint = new Vector2(Random.Range(5f, 10f), Random.Range(-5f, 5f));
    }


    // Update is called once per frame
    void Update()
    {
        if(OnScreen() && player)
        {
            if(!wasActive)
                gameObject.AddComponent<Scroller>();
            wasActive = true;
            PrepareAttack();
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

    private void PrepareAttack()
    {
        if (!cooldown)
        {
            Vector3 target = player.position;
            target.x += firingPoint.x;
            target.y += firingPoint.y;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            //Stop moving, start shooting
            if (Vector2.Distance(transform.position, target) < 1)
            {
                cooldown = true;
                StartCoroutine(Shoot());
            }
                
        }
    }

    private IEnumerator Shoot() {
        Instantiate(shot, shootPoint.position, shootPoint.rotation);

        yield return new WaitForSeconds(1);

        firingPoint = new Vector2(Random.Range(5f, 10f), Random.Range(-5f, 5f));
        cooldown = false;
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
