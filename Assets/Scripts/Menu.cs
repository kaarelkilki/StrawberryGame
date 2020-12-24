using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
