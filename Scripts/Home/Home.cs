using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [SerializeField] private Button play; // はじめるボタン
    [SerializeField] private Button how;　// 遊び方ボタン

    void Start()
    {
        play.onClick.AddListener(Play);
        how.onClick.AddListener(How);
    }

    public void Play()
    {
        // セレクトへ
        SceneManager.LoadScene("Select");
    }

    public void How()
    {
        // 遊び方説明へ
        SceneManager.LoadScene("Howto");
    }
}

/*
 * Home(ホーム画面(最初のシーン))のプログラム。
 * セレクト画面へ移動する Select ボタンと遊び方を説明するシーンに移動する Howto ボタンの2つのButtonがあり、
 * クリックするとそれぞれのシーンに遷移するようになっています。
 */
