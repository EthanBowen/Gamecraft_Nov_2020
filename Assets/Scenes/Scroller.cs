using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        var lerpedPosition = Vector2.right * speed * Time.deltaTime;
        transform.position += new Vector3(lerpedPosition.x, lerpedPosition.y, 0);
    }
}
