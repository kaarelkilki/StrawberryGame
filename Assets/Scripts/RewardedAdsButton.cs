using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3953950";
#elif UNITY_ANDROID
    private string gameId = "3953951";
#endif

    Button myButton;
    public string myPlacementId = "rewardedVideo";
    public float timeRemaining = 4;
    public bool timerIsRunning = false;

    void Start()
    {
        
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId & GameObject.Find("Ursus").GetComponent<PlayerController>().extraTime <= 0.0f)
        {
            myButton.gameObject.SetActive(true);
            myButton.interactable = true;
        }
        else if (GameObject.Find("Ursus").GetComponent<PlayerController>().extraTime > 0.0f)
        {
            myButton.gameObject.SetActive(false);
            myButton.interactable = false;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            //Give 12 extra seconds to use
            GameObject ursus = GameObject.Find("Ursus");
            PlayerController playerController = ursus.GetComponent<PlayerController>();
            playerController.extraTime = 12.0f;
        }
        else if (showResult == ShowResult.Skipped)
        {
            timerIsRunning = true;
            timeRemaining = 4;
            TimerRunning();            
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    void TimerRunning()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                GameObject.Find("AdDidNotFinishText").SetActive(true);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameObject.Find("AdDidNotFinishText").SetActive(false);
            }
        }
    }
}
