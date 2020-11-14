using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LeprechaunAI : MonoBehaviour
{
    public int HP = 3;
    public float speed = 3f;
    public float chargeSpeed = 20f;

    private Transform player;
    new private SpriteRenderer renderer;
    private bool wasActive;

    private bool charging;
    private bool aggro;
    private Vector2 chargePoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        renderer = GetComponent<SpriteRenderer>();
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
        if (charging)
        {
            //Charge stage
            chargePoint.x = player.position.x;
            transform.position = Vector2.MoveTowards(transform.position, chargePoint, chargeSpeed * Time.deltaTime);
            
            //Stop charging, start moving
            if (Vector2.Distance(transform.position, chargePoint) < 1)
                charging = false;
                aggro = false;
        }
        else if (!aggro)
        {
            //Move stage
            renderer.color = Color.white;
            Vector2 target = player.position;
            target.x += 10;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            //Stop moving, start aggroing
            if (Vector2.Distance(transform.position, target) < 1)
                StartCoroutine(Aggro());
        }
    }

    private IEnumerator Aggro() {
        renderer.color = Color.red;
        aggro = true;

        yield return new WaitForSeconds(2);

        //Start charging
        charging = true;
        chargePoint = transform.position;
        chargePoint.x -= 10;
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
