using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static List<string> Gifts;
    //public static Dictionary<string, bool> Gifts;

    static DataManager()
    {
        Gifts = new List<string>();//new Dictionary<string, bool>();


        Gifts.Add("undees");
        Gifts.Add("cookie");
        Gifts.Add("sock");
    }

}
