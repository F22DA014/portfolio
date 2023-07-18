using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] JustPoint justPoint;
    [SerializeField] Note_Generator note_Generator;

    [SerializeField] GameObject NextButtonObject_1; // 各種ボタン
    [SerializeField] Button NextButton_1;
    [SerializeField] GameObject NextButtonObject_2;
    [SerializeField] Button NextButton_2;
    [SerializeField] Button EndButton;

    [SerializeField] GameObject StartPanel;    //スタートパネル

    [SerializeField] GameObject EndPanel_1;　  //エンドパネル１枚目
    [SerializeField] private Text Score_1;　   //スコア表示
    [SerializeField] private Text MaxCombo_1;　//最大コンボ数表示

    [SerializeField] GameObject EndPanel_2;　　//エンドパネル２枚目
    [SerializeField] private Text Score_2;
    [SerializeField] private Text ScoreMission;
    [SerializeField] private Text MaxCombo_2;
    [SerializeField] private Text ComboMission;
    [SerializeField] private Text MISSandWRONG;
    [SerializeField] private Text MandWMission;
    [SerializeField] private Text CorF1; // ミッションをクリアしたか否かで表示を変えるテキスト×３
    [SerializeField] private Text CorF2;
    [SerializeField] private Text CorF3;

    [SerializeField] GameObject EndPanel_3;   //エンドパネル３枚目
    [SerializeField] private Text Result;     //最終的なゲームの結果
    [SerializeField] private Text Comment;    //結果に対してのコメント

    private int Progress = 0; // はじめか終わりか。０ならはじめ、１以降は終わりで、各EndPanelの番号と対応。

    private int score; // スコアを受け取る変数
    private int combo; // 最大コンボ数を受け散る変数
    private int mandw;  // MISSとWRONGの合計を受け取る変数
    private int note;   // ノーツの合計数を受け取る変数ボーダーライン(ミッション)の計算に必要
    private int ScoreM; //各種クリアライン代入変数
    private int ComboM;
    private int MandWM;
    private int CMission = 0; // クリアしたミッションの数を数える。最大で３

    public void Start()
    {
        // パネル・ボタンは非表示。
        StartPanel.SetActive(false);
        EndPanel_1.SetActive(false);
        EndPanel_2.SetActive(false);
        EndPanel_3.SetActive(false);
        NextButtonObject_1.SetActive(false);
        NextButtonObject_2.SetActive(false);

        NextButton_1.onClick.AddListener(NextPanel);
        NextButton_2.onClick.AddListener(NextPanel);
        EndButton.onClick.AddListener(EndGame);

        StartCoroutine(GameSystem());
    }

    public void End()
    {
        justPoint.GameEnd();
        note_Generator.GameEnd();
        StartCoroutine(GameSystem());
    }

    public void NextPanel()
    {
        StartCoroutine(GameSystem());
    }

    IEnumerator GameSystem()
    {
        if (Progress == 0)
        {
            // 音楽が始まるまでの処理。

            // 1秒後StartPanelを表示。2秒で非表示
            yield return new WaitForSeconds(1);
            StartPanel.SetActive(true);
            yield return new WaitForSeconds(2);
            StartPanel.SetActive(false);

            // その後3秒後にTimeLine再生
            yield return new WaitForSeconds(3);

            Progress = 1;
            playableDirector.Play();
        }
        else if (Progress == 1)
        {
            // 音楽が終わったあとの処理

            // スコアなどの結果をPlayerPrefsから受け取る
            score = PlayerPrefs.GetInt("Score");
            combo = PlayerPrefs.GetInt("MaxCombo");
            mandw = PlayerPrefs.GetInt("MISSandWRONG");

            // 1枚目
            // 1秒後EndPanel_1を表示。
            yield return new WaitForSeconds(1);
            EndPanel_1.SetActive(true);
            // １秒後にスコア表示。更に１秒後に最大コンボ数を表示。
            yield return new WaitForSeconds(1);
            Score_1.text = string.Format("{0}", score);
            MaxCombo_1.text = string.Format("{0}", combo);
            // 2秒後NextButton_1を表示
            yield return new WaitForSeconds(2);
            NextButtonObject_1.SetActive(true);
            Progress++;
        }

        else if (Progress == 2)
        {
            // ノーツの数を受け取ってボーダーラインを計算
            note = PlayerPrefs.GetInt("Note");
            ScoreM = note * 500;
            ComboM = note / 3;
            MandWM = note / 10;

            // 2枚目
            EndPanel_1.SetActive(false);
            EndPanel_2.SetActive(true);
            // 1秒後に各種記録を表示
            yield return new WaitForSeconds(1);
            Score_2.text = string.Format("{0}", score);
            MaxCombo_2.text = string.Format("{0}", combo);
            MISSandWRONG.text = string.Format("{0}", mandw);
            // 1秒後にクリアラインを表示、その1秒後にCorFを表示
            // これを各項目ずつ行う(計３回)
            // SCORE
            yield return new WaitForSeconds(1);
            ScoreMission.text = string.Format("{0}", ScoreM);
            yield return new WaitForSeconds(1);
            if (score >= ScoreM)
            {
                CMission++;
                CorF1.text = string.Format("CLEAR");
            }
            else
            {
                CorF1.color = new Color(0.0f, 0.0f, 1.0f, 1.0f); // テキストの色を青に変える
                CorF1.text = string.Format("FAIL");
            }
            // COMBO
            yield return new WaitForSeconds(1);
            ComboMission.text = string.Format("{0}", ComboM);
            yield return new WaitForSeconds(1);
            if (combo >= ComboM)
            {
                CMission++;
                CorF2.text = string.Format("CLEAR");
            }
            else
            {
                CorF2.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                CorF2.text = string.Format("FAIL");
            }
            // MISS&WRONG
            yield return new WaitForSeconds(1);
            MandWMission.text = string.Format("{0}", MandWM);
            yield return new WaitForSeconds(1);
            if (mandw <= MandWM) // MISS&WRONGに限り不等号の向き逆。
            {
                CMission++;
                CorF3.text = string.Format("CLEAR");
            }
            else
            {
                CorF3.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                CorF3.text = string.Format("FAIL");
            }
            // 2秒後NextButton_2を表示
            yield return new WaitForSeconds(2);
            NextButtonObject_2.SetActive(true);
            Progress++;
        }

        else
        {
            // 3枚目
            EndPanel_2.SetActive(false);
            EndPanel_3.SetActive(true);
            yield return new WaitForSeconds(1);
            // ミッションを全部クリアしていれば”討伐成功”
            if (CMission == 3)
            {
                Result.text = string.Format("討伐成功");
                yield return new WaitForSeconds(1);
                Comment.text = string.Format("GAME CLEAR!!");
            }
            else
            {
                Result.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                Result.text = string.Format("討伐失敗");
                yield return new WaitForSeconds(1);
                Comment.text = string.Format("NOT CLEAR...");

            }
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene("Home");
    }
}

/*
 * End()
 * TimeLineにフラグを立て、そこを通過したらここを実行します。
 * JustPointとnote_Generatorに命令を送り、ミッションのボーダーラインを計算するために
 * 必要な要素を記録させます。
 * また、IEnumerator GameSystem()を再び使うため StartCoroutine(GameSystem());を記述します
 * 
 * NextPanel()
 * Panel内のButtonが押された時に実行します。
 *
 * IEnumerator GameSystem()
 * 
 * 開始前の処理(Progress == 0)
 * 曲名、難易度を記入したPanelを似秒間だけ表示します。
 * 
 * 終了後の処理(Progress <= 1)
 * 
 * １枚目のEndPanel(Progress == 1)
 * スコアや最大コンボ数を表示。
 *
 * ２枚目のEndPanel(Progress == 2)
 * 他のプログラムが数えたノーツ数に応じて、
 * スコア、最大コンボ数、MissとWrongの許容数を計算で出し、それぞれ達成していれば、
 * そのままの色で CLEAR　未達成なら、テキストの色を青に変えて　FAIL　と表示します。
 * 
 * ３枚目のEndPanel(else (Progress == 3))
 * ２枚目のEndPanelでのミッションがすべて達成していれば、
 * そのままの色で　GAME CLEAR!!　未達成なら　テキストの色を青に変えて NOT CLEAR... と表示します。
 * 
 * EndGame()
 * 3枚目のEndPanel内のButtonを押すと実行します。
 * Homeに戻ります。
 */