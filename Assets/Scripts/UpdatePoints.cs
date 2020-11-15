using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePoints : MonoBehaviour
{
    public TextMeshProUGUI PointsText;

    // Start is called before the first frame update
    void Start()
    {
        PointsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        PointsText.text = "Gifts Delivered:  " + DataManager.Points.ToString();
    }
}
