using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGifts : MonoBehaviour
{
    public GameObject[] GiftSprites = new GameObject[10];
    public Transform StartLocation;
    public float OffSet = 3f;
    List<string> Gifts;

    // Start is called before the first frame update
    void Start()
    {
        Gifts = DataManager.Gifts;
        DrawSprites();
    }

    public void DrawSprites()
    {
        //draws all the sprites contained in the DataManager

        Gifts = DataManager.Gifts;

        for (int i = 0; i < GiftSprites.Length; i++)
        {

            if (Gifts.Contains(GiftSprites[i].name) && GiftSprites[i].activeSelf)
            {
                GiftSprites[i] = Instantiate(GiftSprites[i], StartLocation.position + new Vector3((OffSet * i), 0, 0), StartLocation.rotation);
            }
            else
            {
                GiftSprites[i].SetActive(false);
            }
        }
    }

}
