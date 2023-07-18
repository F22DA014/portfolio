using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
    // ステージセレクトボタン
    [SerializeField] private Button stage_1;
    [SerializeField] private Button stage_2;

    // ステージパネル
    [SerializeField] private GameObject Panel_1;
    [SerializeField] private GameObject Panel_2;

    //スタートボタン
    [SerializeField] private Button start_1;
    [SerializeField] private Button start_2;

    void Start()
    {
        PanelDel();

        // ボタンを押すとパネルが表示されたり、シーンを変えたりする。

        stage_1.onClick.AddListener(Stage_1);
        stage_2.onClick.AddListener(Stage_2);

        start_1.onClick.AddListener(Start_1);
        start_2.onClick.AddListener(Start_2);
    }

    public void PanelDel()
    {
        // パネルをすべて非表示に
        Panel_1.SetActive(false);
        Panel_2.SetActive(false);
    }

    public void Stage_1()
    {
        PanelDel();

        Panel_1.SetActive(true);
    }

    public void Start_1()
    {
        SceneManager.LoadScene("Stage_1");
    }

    public void Stage_2()
    {
        PanelDel();

        Panel_2.SetActive(true);
    }

    public void Start_2()
    {
        //SceneManager.LoadScene("Stage_2");
    }
}

/*
 * 楽曲のセレクトシーンのプログラムです。
 * 
 * Buttonを押すと対応するPanelが表示され、そのPanel内のButtonを押すと
 * ゲームシーン(Stage_~)に遷移します。
 * 
 * 今回は１曲分のみ制作したのでコメントアウトしましたが、
 * ２曲以降制作した場合にも同様のやり方でシーン遷移を制御することができます。
 */
