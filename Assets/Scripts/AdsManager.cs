using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "3953950";
#elif UNITY_ANDROID
    private string gameId = "3953951";
#endif

    string placement = "rewardedVideo";
    
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
    }

    public void ShowAd(string p)
    {
        Advertisement.Show(p);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            //Give 12 extra seconds to use
            GameObject ursus = GameObject.Find("Ursus");
            PlayerController playerController = ursus.GetComponent<PlayerController>();
            playerController.extraTime = 12.0f;
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }
}
