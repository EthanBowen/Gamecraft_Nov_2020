using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGifts : MonoBehaviour
{
    public GameObject[] GiftSprites = new GameObject[10];
    public Transform StartLocation;
    public float OffSet = 1f;
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
            Debug.Log(GiftSprites[i].name);
            if ( Gifts.Contains(GiftSprites[i].name) )// && GiftSprites[i].activeSelf)
            {
                GiftSprites[i] = Instantiate(GiftSprites[i], StartLocation.position - new Vector3((OffSet * i), 0, 0), StartLocation.rotation);
                GiftSprites[i].SetActive(true);
            }
            else
            {
                GiftSprites[i].SetActive(false);
            }
        }
    }

}
