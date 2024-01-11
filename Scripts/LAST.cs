using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Last : MonoBehaviour
{
    public Button OpenNazo;
    public Button BackNazo;

    int n = 0;

    void Start()
    {
        n = PlayerPrefs.GetInt("Clear");

        OpenNazo.onClick.AddListener(NazoOpen);
        BackNazo.onClick.AddListener(MaeNazo);
    }

    private void NazoOpen()
    {
        SceneManager.LoadScene("Last_Nazo");
    }

    private void MaeNazo()
    {
        SceneManager.LoadScene("Message");
    }
}
