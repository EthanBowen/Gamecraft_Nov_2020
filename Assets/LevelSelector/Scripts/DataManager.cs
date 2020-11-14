using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static Dictionary<string, Status> Gifts;
    //public static Dictionary<string, string> LevelConsumedOn;
    public static string CurrentGiftLevel;

    static DataManager()
    {
        Gifts = new Dictionary<string, Status>();

        Status status;
        status.GiftStatus = GiftStatus.stocked;
        status.LevelUsedOn = "";

        Gifts.Add("undees", status);
        Gifts.Add("cookie", status);
        Gifts.Add("sock", status);
    }


    public struct Status
    {
        public GiftStatus GiftStatus;
        public string LevelUsedOn;
    }
}


public enum GiftStatus { delivered, lost, stocked }