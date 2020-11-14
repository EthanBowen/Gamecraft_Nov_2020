using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGifts : MonoBehaviour
{
    public List<GameObject> GiftSprites = new List<GameObject>();
    public Transform StartLocation;
    public float OffSet = 1f;
    List<string> Gifts;

    // Start is called before the first frame update
    void Start()
    {
        Gifts = DataManager.Gifts;
        DrawSprites();

        foreach (GameObject go in GiftSprites)
            go.SetActive(true);
    }

    public void DrawSprites()
    {
        //draws all the sprites contained in the DataManager

        Gifts = DataManager.Gifts;

        foreach(GameObject go in GiftSprites)
        {
            Debug.Log(go.name);
            string name = go.name;

            if ( Gifts.Contains(name)  && go.activeSelf)
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
