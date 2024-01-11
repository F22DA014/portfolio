using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ziken_1 : MonoBehaviour
{
    [SerializeField] private string MaeNazo;
    [SerializeField] private string NextNazo; //　次の事件シーン格納
    [SerializeField] private int Nazo;　　　　　// その事件の１問目参照


    // UI一覧
    public GameObject Text_Button;

    // NAZO系
    public GameObject Button_1;
    public GameObject Text_1;
    public GameObject Nazo_2;
    public GameObject Button_2;
    public GameObject Text_2;
    public GameObject Nazo_3;
    public GameObject Button_3;
    public GameObject Text_3;
    public GameObject Nazo_A;
    public GameObject Button_A;
    public GameObject Text_A;
    public GameObject Nazo_B;
    public GameObject Button_B;
    public GameObject Text_B;
    public GameObject Nazo_C;
    public GameObject Button_C;
    public GameObject Text_C;


    // 次の事件に進む
    public GameObject Next_P;
    public Button Next_B;

    public Button Back_B;

    // MEMO系統
    public Button Ziken_Syousai;
    public Button Yougisya_Itiran;
    public Button Syouko_1;
    public Button Syouko_2;
    public Button Syouko_3;
    public Button Syouko_A;
    public Button Syouko_B;
    public Button Syouko_C;
    public GameObject Syouko_1_B;
    public GameObject Syouko_2_B;
    public GameObject Syouko_3_B;
    public GameObject Syouko_A_B;
    public GameObject Syouko_B_B;
    public GameObject Syouko_C_B;

    public GameObject Ziken;
    public GameObject Yougisya;
    public GameObject Syouko1;
    public GameObject Syouko2;
    public GameObject Syouko3;
    public GameObject SyoukoA;
    public GameObject SyoukoB;
    public GameObject SyoukoC;

    public Button Close_1;
    public Button Close_2;
    public Button Close_3;
    public Button Close_4;
    public Button Close_5;
    public Button Close_6;
    public Button Close_7;
    public Button Close_8;


    public GameObject Texts;
    public Button Simei;
    public GameObject Simei_P;
    public Button Simei_Back;
    // public Button OK;
    public GameObject Seikai;
    public GameObject Wrong;
    public GameObject Kaimei;
    public Button OneMore;
    public GameObject Seikaias;
    public Button OneMore1;
    public Button Next_1; //解決後つぎにすすむボタン。実装まだ
    public Button Next_2;
    
    int n = 0;
    int m = 0;
    int massage = 0;
    int l = 0;

    // 犯人指名
    public Button Matigai;
    public Button Matigai2;
    public Button Hannin;
    public Button SinHannin;
    private int simei;

    private string Ans;
    [SerializeField] private string nowScene;
    [SerializeField] private int number;


    void Start()
    {
        l = PlayerPrefs.GetInt("Simei");

        // 最初のやつ以外全部非表示(ボタン以外)※ボタンはNazoに格納されているため
        Text_1.SetActive(false);
        Nazo_2.SetActive(false);
        Text_2.SetActive(false);
        Nazo_3.SetActive(false);
        Text_3.SetActive(false);
        Nazo_A.SetActive(false);
        Text_A.SetActive(false);
        Nazo_B.SetActive(false);
        Text_B.SetActive(false);
        Nazo_C.SetActive(false);
        Text_C.SetActive(false);

        Next_P.SetActive(false);

        Ziken.SetActive(false);
        Yougisya.SetActive(false);
        Syouko1.SetActive(false);
        Syouko2.SetActive(false);
        Syouko3.SetActive(false);
        Syouko_1_B.SetActive(false);
        Syouko_2_B.SetActive(false);
        Syouko_3_B.SetActive(false);
        SyoukoA.SetActive(false);
        SyoukoB.SetActive(false);
        SyoukoC.SetActive(false);
        Syouko_A_B.SetActive(false);
        Syouko_B_B.SetActive(false);
        Syouko_C_B.SetActive(false);

        Texts.SetActive(false);
        Simei_P.SetActive(false);
        Seikai.SetActive(false);
        Kaimei.SetActive(false);
        Wrong.SetActive(false);
        Kaimei.SetActive(false);
        Seikaias.SetActive(false);

        if (l == 1)
        {
            // Rep();
        }

        Ziken_Syousai.onClick.AddListener(Zikens);
        Yougisya_Itiran.onClick.AddListener(Yougisyas);
        Syouko_1.onClick.AddListener(Syoukos1);
        Syouko_2.onClick.AddListener(Syoukos2);
        Syouko_3.onClick.AddListener(Syoukos3);
        Syouko_A.onClick.AddListener(SyoukosA);
        Syouko_B.onClick.AddListener(SyoukosB);
        Syouko_C.onClick.AddListener(SyoukosC);

        Close_1.onClick.AddListener(Closes);
        Close_2.onClick.AddListener(Closes);
        Close_3.onClick.AddListener(Closes);
        Close_4.onClick.AddListener(Closes);
        Close_5.onClick.AddListener(Closes);
        Close_6.onClick.AddListener(Closes);
        Close_7.onClick.AddListener(Closes);
        Close_8.onClick.AddListener(Closes);

        Simei.onClick.AddListener(Simeis);
        Matigai.onClick.AddListener(matigai);
        Matigai2.onClick.AddListener(matigai);
        Hannin.onClick.AddListener(hannin);
        SinHannin.onClick.AddListener(sinhannin);

        Simei_Back.onClick.AddListener(Closes);
        OneMore.onClick.AddListener(Closes);
        OneMore1.onClick.AddListener(Closes);

        Next_1.onClick.AddListener(NextZiken);
        Next_2.onClick.AddListener(NextZiken);
        Next_B.onClick.AddListener(NextZiken);
        Back_B.onClick.AddListener(MaenoZiken);

        n = PlayerPrefs.GetInt("Clear");
        m = PlayerPrefs.GetInt("Kaimei");
        massage = PlayerPrefs.GetInt("Message");
        Next();
        Debug.Log(n);
    }

    private void matigai()
    {
        simei = 0;
        Ok();
    }

    private void hannin()
    {
        simei = 1;
        Ok();
    }

    private void sinhannin()
    {
        simei = 2;
        Ok();
    }

    private void Next()
    {
        // クリア数に応じて表示非表示を切り替え
        if (n >= Nazo)
        {
            Text_1.SetActive(true);
            Button_1.SetActive(false);
            Nazo_2.SetActive(true);
            Syouko_1_B.SetActive(true);
        }
        if (n >= Nazo+1)
        {
            Text_2.SetActive(true);
            Button_2.SetActive(false);
            Nazo_3.SetActive(true);
            Syouko_2_B.SetActive(true);
        }
        if (n >= Nazo+2)
        {
            Text_3.SetActive(true);
            Button_3.SetActive(false);
            Syouko_3_B.SetActive(true);
        }
        if (n >= Nazo+23 && m >= number + 7 && massage >= 1)
        {
            Nazo_A.SetActive(true);
        }
        if (n >= Nazo+24)
        {
            Syouko_A_B.SetActive(true);
            Text_A.SetActive(true);
            Button_A.SetActive(false);
            Nazo_B.SetActive(true);
        }
        if (n >= Nazo+25)
        {
            Text_B.SetActive(true);
            Button_B.SetActive(false);
            Nazo_C.SetActive(true);
            Syouko_B_B.SetActive(true);
        }
        if (n >= Nazo+26)
        {
            Text_C.SetActive(true);
            Button_C.SetActive(false);
            Syouko_C_B.SetActive(true);
        }

        // 解決数に応じて
        if (m >= number)
        {
            Next_P.SetActive(true);
        }
    }

    private void NextZiken()
    {
        SceneManager.LoadScene(NextNazo);
    }

    private void MaenoZiken()
    {
        SceneManager.LoadScene(MaeNazo);
    }

    private void Zikens() // 事件詳細を開く
    {
        Close();
        Ziken.SetActive(true);
    }

    private void Yougisyas() //　容疑者一覧を開く
    {
        Close();
        Yougisya.SetActive(true);
    }

    private void Syoukos1() // 証拠情報を開く
    {
        Close();
        Syouko1.SetActive(true);
    }

    private void Syoukos2() 
    {
        Close();
        Syouko2.SetActive(true);
    }
    private void Syoukos3() 
    {
        Close();
        Syouko3.SetActive(true);
    }

    private void SyoukosA()
    {
        Close();
        SyoukoA.SetActive(true);
    }

    private void SyoukosB()
    {
        Close();
        SyoukoB.SetActive(true);
    }
    private void SyoukosC()
    {
        Close();
        SyoukoC.SetActive(true);
    }

    private void Close()　// 全閉じ
    {
        PlayerPrefs.SetString("PlayerAns", null);

        Text_Button.SetActive(false);
        Ziken.SetActive(false);
        Yougisya.SetActive(false);
        Syouko1.SetActive(false);
        Syouko2.SetActive(false);
        Syouko3.SetActive(false);
        SyoukoA.SetActive(false);
        SyoukoB.SetActive(false);
        SyoukoC.SetActive(false);

        Texts.SetActive(false);
        Simei_P.SetActive(false);
        Seikai.SetActive(false);
        Kaimei.SetActive(false);
        Wrong.SetActive(false);
        Kaimei.SetActive(false);
        Seikaias.SetActive(false);
    }

    private void Closes()
    {
        Text_Button.SetActive(true);
        Ziken.SetActive(false);
        Yougisya.SetActive(false);
        Syouko1.SetActive(false);
        Syouko2.SetActive(false);
        Syouko3.SetActive(false);
        SyoukoA.SetActive(false);
        SyoukoB.SetActive(false);
        SyoukoC.SetActive(false);

        Texts.SetActive(false);
        Simei_P.SetActive(false);
        Seikai.SetActive(false);
        Kaimei.SetActive(false);
        Wrong.SetActive(false);
        Kaimei.SetActive(false);
        Seikaias.SetActive(false);
    }

    private void Simeis()
    {
        // クリア数足りない場合はテキスト表示 コピー後はここ変える
        if (n < Nazo+2)
        {
            Texts.SetActive(true);
        }
        // 足りてればパネル
        else
        {
            Close();
            Simei_P.SetActive(true);
        }
    }

    private void Ok()
    {
        PlayerPrefs.SetString("PlayerAns", null);
        Close();
        // クリア数によって答えが変わるのでその処理
        if (n < Nazo+26 && simei == 1)
        {
            // 一周目。犯人を指名→正解
            Seikai.SetActive(true);

            if (m < number)
            {
                m++;
                PlayerPrefs.SetInt("Kaimei", m);
                Next();
            }
        }
        else if (simei == 1)
        {
            // ２周目。犯人を指名→正解？
            Seikaias.SetActive(true);
        }
        else if (n >= Nazo+26 && simei == 2)
        {
            // ２周め真犯人を指名→解明
            Kaimei.SetActive(true);
            if (m < number + 8)
            {
                m++;
                PlayerPrefs.SetInt("Kaimei", m);
                Next();
            }
        }
        else
        {
            // 上記該当がなければ不正解
            Wrong.SetActive(true);
        }
    }

    public void Rep() // 拡大から戻ってきた後
    {
        Simei_P.SetActive(true);
        l = 0;
        PlayerPrefs.SetInt("Simei", l);
    }

    // ↓デバック、テストプレイ用。
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            // 0で初期化
            PlayerPrefs.SetInt("Clear", 47);
            PlayerPrefs.SetInt("Kaimei", 15);
            PlayerPrefs.SetInt("Simei", 0);
        }
    }
}
