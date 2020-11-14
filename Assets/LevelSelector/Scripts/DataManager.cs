using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static List<string> Gifts;

    static DataManager()
    {
        Gifts = new List<string>();

        Gifts.Add("undees");
        Gifts.Add("cookie");
        Gifts.Add("sock");
    }

}
