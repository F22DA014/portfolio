using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Button start;
    public Button Howto;
    public Button Reset;

    void Start()
    {
        start.onClick.AddListener(Game_Start);
        Howto.onClick.AddListener(HowtoPlay);
        Reset.onClick.AddListener(Reset_Game);
    }

    private void Game_Start()
    {
        SceneManager.LoadScene("Ziken_1");
    }

    private void HowtoPlay()
    {
        // 実装まだ
    }

    private void Reset_Game()
    {
        PlayerPrefs.SetInt("Clear", 0);
        PlayerPrefs.SetInt("Kaimei", 0);
        PlayerPrefs.SetInt("Simei", 0);
    }
}
