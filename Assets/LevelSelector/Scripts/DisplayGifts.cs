﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGifts : MonoBehaviour
{
    public List<GameObject> GiftSprites = new List<GameObject>();
    Dictionary<string, DataManager.Status> Gifts;

    // Start is called before the first frame update
    void Start()
    {
        Gifts = DataManager.Gifts;

        foreach (GameObject go in GiftSprites)
            go.SetActive(true);

        DrawSprites();
    }

    public void DrawSprites()
    {
        //draws all the sprites contained in the DataManager

        Gifts = DataManager.Gifts;

        foreach(GameObject go in GiftSprites)
        {
            string name = go.name;

            //If the player has the gift with them the gift is displayed
            if ( Gifts.TryGetValue(name, out DataManager.Status value) && value.GiftStatus == GiftStatus.stocked)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
