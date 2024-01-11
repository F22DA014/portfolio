using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Nazo_Answer : MonoBehaviour
{
    public InputField answer_zone;　// テキストボックス
    public Text Player_answer;     // ユーザーの答え
    public Button Answer_Button;   // 解答ボタン
    public Button BackButton_1;    //戻るボタン×２
    public Button BackButton_2;
    [SerializeField] private string Answer;  // 謎の答え
    public GameObject Panel;     // パネル
    public GameObject Wrong;     // 不正解表示
    public Button Hint_B;        // ヒントボタン
    public Button Miru;          // ヒントを見る
    public GameObject Mirus;
    public Button Modoru;        // ヒント閉じる
    public GameObject Hint_P;    // ヒント枠
    public GameObject hint;      // ヒント本体

    public Button Moji_close_1;
    public Button Moji_close_2;

    public GameObject MojiPanel;
    public GameObject MojiPanel2;

    public Button Moji_Next1;
    public Button Moji_Next2;

    public Button INPUT;

    // public Button KakudaiModosi;      // 拡大をもとに戻す
    // public GameObject KakudaiModosi_O;

    private string Ans;

    int n;

    public int Num;

    [SerializeField] private string str;

    void Start()
    {
        // KakudaiModosi_O.SetActive(false);

        //Componentを扱えるようにする
        answer_zone = answer_zone.GetComponent<InputField>();
        Player_answer = Player_answer.GetComponent<Text>();

        Panel.SetActive(false);
        Wrong.SetActive(false);
        Hint_P.SetActive(false);
        hint.SetActive(false);
        MojiPanel2.SetActive(false);
        MojiPanel.SetActive(false);

        Answer_Button.onClick.AddListener(Answered);
        Hint_B.onClick.AddListener(Hint);
        Miru.onClick.AddListener(Miruyo);
        Modoru.onClick.AddListener(Close);
        Moji_close_1.onClick.AddListener(Close);
        Moji_close_2.onClick.AddListener(Close);
        BackButton_1.onClick.AddListener(Back);
        BackButton_2.onClick.AddListener(Back);
        Moji_Next1.onClick.AddListener(Next1);
        Moji_Next2.onClick.AddListener(Next2);
        INPUT.onClick.AddListener(inp);

        n = PlayerPrefs.GetInt("Clear");

        // KakudaiModosi.onClick.AddListener(Mini);
    }

    private void inp()
    {
        MojiPanel.SetActive(true);
    }

    public void InputMOJI(string MOJI)
    {
        if (MOJI != "KESU")
        {
            answer_zone.text += MOJI;
        }
        else if (answer_zone.text != "")
        {
            answer_zone.text = answer_zone.text[..^1];
        }
    }

    public void Answered()
    {
        Hint_P.SetActive(false);
        MojiPanel2.SetActive(false);
        MojiPanel.SetActive(false);

        // テキスト反映
        Player_answer.text = answer_zone.text;

        // 正解か不正解家で処理を変える
        if (Player_answer.text == Answer)
        {
            // 正解
            Debug.Log("正解");
            Wrong.SetActive(false);
            Panel.SetActive(true);
            // クリア数加算
            if (SceneManager.GetActiveScene().name == "Message_Nazo")
            {
                PlayerPrefs.SetInt("Message", 1);
            }
            else if (SceneManager.GetActiveScene().name == "Message_Nazo_Re")
            {
                PlayerPrefs.SetInt("Message", 2);
            }
            else if (Num > n)
            {
                n++;
            }
            PlayerPrefs.SetInt("Clear", n);
        }
        else
        {
            // 不正解
            Debug.Log("不正解");
            Wrong.SetActive(true);
        }
    }

    public void Hint()
    {
        MojiPanel2.SetActive(false);
        MojiPanel.SetActive(false);
        Hint_P.SetActive(true);
    }

    public void Miruyo()
    {
        MojiPanel2.SetActive(false);
        MojiPanel.SetActive(false);
        Mirus.SetActive(false);
        hint.SetActive(true);
    }

    public void Close()
    {
        Hint_P.SetActive(false);
        MojiPanel2.SetActive(false);
        MojiPanel.SetActive(false);
    }

    public void Back()
    {
        PlayerPrefs.SetString("PlayerAns",null);
        SceneManager.LoadScene(str);
    }

    public void Next1()
    {
        MojiPanel2.SetActive(true);
        MojiPanel.SetActive(false);
    }

    public void Next2()
    {
        MojiPanel2.SetActive(false);
        MojiPanel.SetActive(true);
    }
}
