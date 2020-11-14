using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;

    private int velx;
    private int vely;

    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    public KeyCode attack;

    public int SPEED;

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
    }
}
