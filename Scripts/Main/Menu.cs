using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button PauseButton;　//一時停止ボタン
    [SerializeField] private Button ResumeButton; //再開ボタン
    [SerializeField] private Button HomeButton; //戻るボタン

    [SerializeField] private GameObject PausePanel; //一時停止時の画面

    void Start()
    {
        // PausePanelは非表示
        PausePanel.SetActive(false);

        PauseButton.onClick.AddListener(Pause);
        ResumeButton.onClick.AddListener(Resume);
        HomeButton.onClick.AddListener(Home);
    }

    private void Pause()
    {
        // 時間を止める
        Time.timeScale = 0;
        // PausePanelを表示
        PausePanel.SetActive(true);
    }

    private void Resume()
    {
        // 時間を再び流す
        Time.timeScale = 1;
        // PausePanelを非表示に。
        PausePanel.SetActive(false);
    }

    private void Home()
    {
         // ホームへ
        SceneManager.LoadScene("Home");
    }
}

/*
 * ゲーム中のメニューを制御するプログラム。
 * PauseButton を押すとPausePanel を表示し、ゲームを止めます。
 * その後 ResumeButton を押せばゲーム再開、
 * HomeButton を押せば Home にシーン遷移します。
 */