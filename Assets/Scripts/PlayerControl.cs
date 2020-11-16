using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;

    private float velx;
    private float vely;

    public Transform shootPoint;
    public Transform giftPoint;
    public GameObject shot;
    public GameObject Gift;

    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode attack = KeyCode.Space;
    public KeyCode dropGift = KeyCode.S;

    //Debug commands
    public KeyCode fail = KeyCode.F;
    public KeyCode succed = KeyCode.G;

    //public int HP = 3;
    public int hits = 0;
    public float SPEED = 3f;
    public int DamageFromProjectiles = 1;
    public int DamageFromCollisions = 2;
    public int GiftCount = 10;
    public float TimeBetweenGifts = 1;
    public float shootingSpeed = 0.2f;

    private bool CanDropGift = true;
    private bool firing = false;
    public string Scene = "MenuScene 1";

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep within bounds of the screen
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f);
        pos.y = Mathf.Clamp(pos.y, 0.05f, 0.95f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetKey(up))
            vely = SPEED;
        else if (Input.GetKey(down))
            vely = -SPEED;
        else
            vely = 0;
        
        if (Input.GetKey(right))
            velx = SPEED;
        else if (Input.GetKey(left))
            velx = -SPEED;
        else
            velx = 0;
        
        rb.velocity = new Vector3(velx, vely, 0);

        if (Input.GetKey(attack))
        {
            if(!firing)
            {
                Instantiate(shot, shootPoint.position, shootPoint.rotation);
                firing = true;
                InvokeRepeating(nameof(Fire), shootingSpeed, shootingSpeed);
            }
        }
        else
        {
            firing = false;
            CancelInvoke(nameof(Fire));
        }

        if (Input.GetKeyDown(dropGift))
            DropGift();

        //Debug Commands
        if (Input.GetKeyDown(fail))
            Fail();

        if (Input.GetKeyDown(succed))
            Succeed();
    }

    private void Succeed()
    {
        var temp = DataManager.Gifts[DataManager.CurrentGiftLevel];
        temp.GiftStatus = GiftStatus.delivered;
        temp.LevelUsedOn = SceneManager.GetActiveScene().name;
        DataManager.Gifts[DataManager.CurrentGiftLevel] = temp;
        DataManager.WonLevel = true;

        SceneManager.LoadScene(Scene);
    }

    private void Fail()
    {
        var temp = DataManager.Gifts[DataManager.CurrentGiftLevel]; //current gift
        temp.GiftStatus = GiftStatus.lost; // sets it to lost
        temp.LevelUsedOn = SceneManager.GetActiveScene().name; 
        DataManager.Gifts[DataManager.CurrentGiftLevel] = temp;
        DataManager.WonLevel = false;

        SceneManager.LoadScene(Scene);
    }

    private void DropGift()
    {
        if (CanDropGift && ( GiftCount > hits || GiftCount <= -1 ) )
        {
            Instantiate(Gift, giftPoint.position, giftPoint.rotation);
            CanDropGift = false;
            GiftCount--;
            StartCoroutine(GiftCorountine());
        }
    }

    private IEnumerator GiftCorountine()
    {
        yield return new WaitForSeconds(TimeBetweenGifts);
        CanDropGift = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyProjectile"))
            ReceiveDamage(DamageFromProjectiles);

        if (collision.CompareTag("Enemy"))
            ReceiveDamage(DamageFromCollisions);

        if (collision.CompareTag("EndLevel"))
            Succeed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
            ReceiveDamage(1);
    }

    private void ReceiveDamage(int points)
    {
        hits += points;
        if (hits >= GiftCount)
            Fail();
            //Destroy(gameObject);
    }

    void Fire()
    {
        if (firing) 
        {
            animator.SetTrigger("CanShoot");
            Instantiate(shot, shootPoint.position, shootPoint.rotation);
        }
    }
}
