﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGifts : MonoBehaviour
{
    public List<GameObject> GiftSprites = new List<GameObject>();
    public Transform StartLocation;
    Dictionary<string, bool> Gifts;

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
            Debug.Log(go.name);
            string name = go.name;

            if ( Gifts.TryGetValue(name, out bool value) && value)
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
