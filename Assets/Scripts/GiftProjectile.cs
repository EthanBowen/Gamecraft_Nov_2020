using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftProjectile : MonoBehaviour
{
    public float Speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (-1 * (transform.up)) * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("chimney"))
            Destroy(gameObject);
    }
}
