using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presents : MonoBehaviour
{
    public int maxPresents = 10;
    public int numPresents;

    public Image[] presents;

    public Sprite present;
    public Sprite badPresent;

    private PlayerControl player;

    private void Start()
    {
        player = GetComponent<PlayerControl>();
        numPresents = player.GiftCount;
    }

    private void Update()
    {
        for (int i = 0; i < presents.Length; i++)
        {
            if(i < player.hits)
            {
                presents[i].sprite = badPresent;
            }
            else
            {
                presents[i].sprite = present;
            }
            

            if(i < player.GiftCount)
            {
                presents[i].enabled = true;
            }
            else
            {
                presents[i].enabled = false;
            }
        }
    }
}
