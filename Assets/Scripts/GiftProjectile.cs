using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftProjectile : MonoBehaviour
{
    public float Speed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.position += (-1 * (transform.up)) * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("chimney"))
        {
            DataManager.Points++;
            Debug.Log(DataManager.Points);
            Destroy(gameObject);
        }
    }
}
