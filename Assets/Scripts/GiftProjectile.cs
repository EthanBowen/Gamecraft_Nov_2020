using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftProjectile : MonoBehaviour
{
    public float Speed = 4f;
    public float TimeAlive = 4;


    private void Start()
    {
        StartCoroutine(TimeOut(TimeAlive));
    }

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

    private IEnumerator TimeOut(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
