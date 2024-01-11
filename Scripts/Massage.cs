using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Massage : MonoBehaviour
{
    public Button OpenNazo;
    public Button NextNazo;
    public GameObject NextPanel;
    public Button BackNazo;

    int n = 0;
    int m = 0;

    void Start()
    {
        n = PlayerPrefs.GetInt("Clear");
        m = PlayerPrefs.GetInt("Kaimei");

        if (n < 48 || m < 16)
        {
            NextPanel.SetActive(false);
        }

        OpenNazo.onClick.AddListener(NazoOpen);
        NextNazo.onClick.AddListener(Next);
        BackNazo.onClick.AddListener(MaeNazo);
    }

    private void NazoOpen()
    {
        if (n >= 48)
        {
            SceneManager.LoadScene("Message_Nazo_Re");
        }
        else
        {
            SceneManager.LoadScene("Message_Nazo");
        }
    }

    private void Next()
    {
        SceneManager.LoadScene("Last");
    }

    private void MaeNazo()
    {
        SceneManager.LoadScene("Ziken_8");
    }
}
