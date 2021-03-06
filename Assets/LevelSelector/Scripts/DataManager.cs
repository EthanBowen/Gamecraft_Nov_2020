﻿using System.Collections.Generic;

public static class DataManager
{
    public static Dictionary<string, Status> Gifts;
    public static string CurrentGiftLevel;
    public static int Points;
    public static string FailText;
    public static string SuccessText;
    public static bool LoadedIntoLevel;
    public static bool WonLevel;

    static DataManager()
    {
        Reset();
    }

    public static bool AllUsed()
    {
        foreach(var Gift in Gifts)
        {
            if(Gift.Value.GiftStatus == GiftStatus.selected
                || Gift.Value.GiftStatus == GiftStatus.stocked)
            return false;
        }

        return true;
    }

    public static bool AllDelivered()
    {
        foreach(KeyValuePair<string, Status> kvp in Gifts)
        {
            if (kvp.Value.GiftStatus != GiftStatus.delivered)
                return false;
        }

        return true;
    }

    public static void Reset()
    {
        Gifts = new Dictionary<string, Status>();
        Points = 0;
        LoadedIntoLevel = false;
        WonLevel = false;

        Status status;
        status.GiftStatus = GiftStatus.stocked;
        status.LevelUsedOn = "";

        Gifts.Add("sweater", status);
        Gifts.Add("toycar", status);
        Gifts.Add("ornament", status);
        Gifts.Add("stocking", status);
        Gifts.Add("N64", status);
        Gifts.Add("undees", status);
        Gifts.Add("money", status);
        Gifts.Add("cookie", status);
        Gifts.Add("coal", status);
        Gifts.Add("sock", status);
    }

    public static int GetNumGiftDelivered()
    {
        int giftsDelivered = 0;

        foreach(KeyValuePair<string, Status> kvp in Gifts)
        {
            if (kvp.Value.GiftStatus == GiftStatus.delivered)
                giftsDelivered++;
        }

        return giftsDelivered;
    }

    public struct Status
    {
        public GiftStatus GiftStatus;
        public string LevelUsedOn;
    }
}


public enum GiftStatus { delivered, lost, stocked, selected }