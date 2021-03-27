﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuaweiMobileServices.Analystics;
using HuaweiMobileServices.Utils;
using System;

public class HMSAnalyticsManager : HMSSingleton<HMSAnalyticsManager>
{
    private HiAnalyticsInstance hiAnalyticsInstance;

    void InitilizeAnalyticsInstane()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");

        HiAnalyticsTools.EnableLog();
        hiAnalyticsInstance = HiAnalytics.GetInstance(activity);
        hiAnalyticsInstance.SetAnalyticsEnabled(true);

    }

    public void SendEventWithBundle(String eventID, String key, String value)
    {
        Bundle bundleUnity = new Bundle();
        bundleUnity.PutString(key, value);
        Debug.Log($"[HMS] : Analytics Kits Event Id:{eventID} Key:{key} Value:{value}");
        hiAnalyticsInstance.OnEvent(eventID, bundleUnity);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitilizeAnalyticsInstane();
    }
}
