using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Howto : MonoBehaviour
{
    // 各ページ
    [SerializeField] private GameObject Page_1;
    [SerializeField] private GameObject Page_2;
    [SerializeField] private GameObject Page_3;
    [SerializeField] private GameObject Page_4;
    // ボタン
    [SerializeField] private Button Page_1_Next;
    [SerializeField] private Button Page_2_Back;
    [SerializeField] private Button Page_2_Next;
    [SerializeField] private Button Page_3_Back;
    [SerializeField] private Button Page_3_Next;
    [SerializeField] private Button Page_4_Back;
    [SerializeField] private Button Page_4_Home;

    void Start()
    {
        PageDel();

        Page_1.SetActive(true);

        // ボタンを押したときに表示されるべきPageの番号の処理へ
        Page_1_Next.onClick.AddListener(Page2);
        Page_2_Back.onClick.AddListener(Page1);
        Page_2_Next.onClick.AddListener(Page3);
        Page_3_Back.onClick.AddListener(Page2);
        Page_3_Next.onClick.AddListener(Page4);
        Page_4_Back.onClick.AddListener(Page3);
        Page_4_Home.onClick.AddListener(GoHome);
    }

    public void PageDel()
    {
        // すべて非表示に
        Page_1.SetActive(false);
        Page_2.SetActive(false);
        Page_3.SetActive(false);
        Page_4.SetActive(false);
    }

    public void Page1()
    {
        PageDel();

        Page_1.SetActive(true);
    }

    public void Page2()
    {
        PageDel();

        Page_2.SetActive(true);
    }

    public void Page3()
    {
        PageDel();

        Page_3.SetActive(true);
    }

    public void Page4()
    {
        PageDel();

        Page_4.SetActive(true);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Home"); // ホームへ
    }
}

/*
 * 遊び方を説明するシーン、How のプログラム。
 * ４枚のPanelに渡り説明し、それぞれに Next Back のButtonがあり、
 * 前のページに戻ったり、つぎのページに進める(最後はHomeに戻る)ようになっています。
 * ページの遷移はSetActiveで表示、非表示を変更することで見せるPanel(ページ)を変えることで、
 * つぎのページに遷移したように見せています。
 */