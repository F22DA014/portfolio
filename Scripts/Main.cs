using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // 使用するボタン
    public Button Memo;　// メモボタン
    public Button Nazo;　// なぞボタン

    // 対応するページ(オブジェクト)
    public GameObject Memo_Page; // メモページ
    public GameObject Nazo_Page; // なぞページ

    int l;


    void Start()
    {
        l = PlayerPrefs.GetInt("Simei");
        if (l != 1)
        {
            // メモページは最初は非表示
            Memo_Page.SetActive(false);
        }
        else
        {
            Nazo_Page.SetActive(false);
        }

        // ボタンが押されたら対応するページを表示する。
        Memo.onClick.AddListener(OpenMemo);
        Nazo.onClick.AddListener(OpenNazo);
    }

    private void OpenMemo()
    {
        // なぞページを非表示
        Nazo_Page.SetActive(false);
        // メモページを表示
        Memo_Page.SetActive(true);
    }

    private void OpenNazo()
    {
        // メモページを非表示
        Memo_Page.SetActive(false);
        // なぞページを表示
        Nazo_Page.SetActive(true);
    }

}
