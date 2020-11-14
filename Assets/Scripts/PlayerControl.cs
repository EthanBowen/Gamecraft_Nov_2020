using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;

    private float velx;
    private float vely;

    public Transform shootPoint;
    public GameObject shot;

    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode attack = KeyCode.Space;

    public float SPEED = 3f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(attack))
            Fire();
    }

    void Fire()
    {
        Instantiate(shot, shootPoint.position, shootPoint.rotation);
    }
}
