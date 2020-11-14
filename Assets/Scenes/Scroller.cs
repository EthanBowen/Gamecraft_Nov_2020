using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        Vector2 lerpedPosition = Vector2.right * speed * Time.deltaTime;
        transform.position += new Vector3(lerpedPosition.x, lerpedPosition.y, 0);
    }
}
