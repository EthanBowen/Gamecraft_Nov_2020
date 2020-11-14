using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    //public static List<string> Gifts;
    public static Dictionary<string, bool> Gifts;

    static DataManager()
    {
        Gifts = new Dictionary<string, bool>();
                //new List<string>();

        Gifts.Add("undees", true);
        Gifts.Add("cookie", true);
        Gifts.Add("sock", true);
    }

}
