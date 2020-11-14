using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    
    public static Dictionary<string, GiftStatus> Gifts;
    public enum GiftStatus {delivered, lost, stocked }

    static DataManager()
    {
        Gifts = new Dictionary<string, GiftStatus>();

        Gifts.Add("undees", GiftStatus.stocked);
        Gifts.Add("cookie", GiftStatus.stocked);
        Gifts.Add("sock", GiftStatus.stocked);
    }


}
