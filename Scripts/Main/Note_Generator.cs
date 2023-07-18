using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Note_Generator : MonoBehaviour
{
    private int note; //　ノーツの数を数える

    private int rnd; // ランダム用臨時変数

    private int Normals; // ノーマルノーツ用変数。１以上の時、Normalに代入された数だけノーマルノーツが流れる。
    private int Attacks; // アタックノーツ用変数。Normalが-1の時、Attack代入された数だけアタックノーツが流れる。

    private string Notes; // 画面上のノーツの記録
    private string Note; // 画面上のノーツで１番古いものを記録

    private bool Dkey; // キー確認用変数
    private bool Fkey;
    private bool Jkey;
    private bool Kkey;

    [SerializeField]  private Button D_Button; // 各種ボタン
    [SerializeField]  private Button F_Button;
    [SerializeField]  private Button J_Button;
    [SerializeField]  private Button K_Button;

    private int Place; // Characterの場所確認

    [SerializeField] Note Normal, Guard, Walk, Attack; // ノーツ呼び出し

    [SerializeField] JustPoint justPoint = default;
    [SerializeField] PlayerAnimation playerAnimation = default;

    public void Start()
    {
        //　ノーツ、入力系の変数を初期化
        note = 0;
        Normals = 0;
        Attacks = 0;
        Dkey = false;
        Fkey = false;
        Jkey = false;
        Kkey = false;
        Place = 0;

        D_Button.onClick.AddListener(DKey);
        F_Button.onClick.AddListener(FKey);
        K_Button.onClick.AddListener(KKey);
        J_Button.onClick.AddListener(JKey);
    }

    public void SpawnNote() // どのノーツを流すか判断する
    {
        note++;

        if (Normals > 0)
        {
            Normals--;

            rnd = Random.Range(1, 11); // １〜１０の整数でランダムに摘出
            if (rnd >= 9)　// それが９以上なら
            {
                GuardSpawn(); // ガードノーツ
            }
            else
            {
                NormalSpawn(); // ノーマルノーツ
            }
        }

        else if (Normals == 0)
        {
            Normals--;
            WalkSpawn(); // ウォークノーツ
        }

        else if (Attacks > 1)
        {
            Attacks--;
            AttackSpawn(); // アタックノーツ
        }

        else
        {
            Attacks--;
            WalkSpawn(); // ウォークノーツ
        }
    }

    public void NormalSpawn()　// ノーマルノーツ出現
    {
        Notes += "N"; // ノーマルノーツの出現を記録。
        Instantiate(Normal, new Vector3(10, 4, 0), Quaternion.identity);　//座標(10, 4, 0)にノーマルノーツを出現させる
    }

    public void GuardSpawn()　// ガードノーツ出現
    {
        Notes += "G"; // ガードノーツの出現を記録。
        Instantiate(Guard, new Vector3(10, 4, 0), Quaternion.identity);　//ガードノーツを出現させる
    }

    public void WalkSpawn()　// ウォークノーツ出現
    {
        Notes += "W"; // ウォークノーツの出現を記録。
        Instantiate(Walk, new Vector3(10, 4, 0), Quaternion.identity);　//ウォークノーツを出現させる
    }

    public void AttackSpawn()　// アタックノーツ出現
    {
        Notes += "A"; // アタックノーツの出現を記録。
        Instantiate(Attack, new Vector3(10, 4, 0), Quaternion.identity);　//アタックノーツを出現させる
    }

    private void Update()
    {

        if (Normals <= 0 && Attacks <= 0) // 待機しているノーツがなければ
        {
            Normals = Random.Range(1, 10);　// ノーマルノールをいくつ流すか決める。
            Attacks = Random.Range(2, 6); // アタックノーツをいくつ流すか決める。※実際に流すのはAttacks−１
        }

        if (Input.GetKeyDown(KeyCode.F)) //Fキーを押したとき
        {
            FKey();
        }
        if (Input.GetKeyDown(KeyCode.J)) //Jキーを押したとき
        {
            JKey();
        }
        if (Input.GetKeyDown(KeyCode.D)) //Dキーを押したとき
        {
            DKey();
        }
        if (Input.GetKeyDown(KeyCode.K)) //Kキーを押したとき
        {
            KKey();
        }
    }

    // 各入力による処理
    public void FKey()
    {
        Fkey = true; // 押したキーはFであることを記録。
        justPoint.NoteClass(); // タイミング評価へ
    }

    public void JKey()
    {
        Jkey = true; // 押したキーはJであることを記録。
        justPoint.NoteClass();
    }

    public void DKey()
    {
        Dkey = true; // 押したキーはDであることを記録。
        justPoint.NoteClass();
    }

    public void KKey()
    {
        Kkey = true; // 押したキーはKであることを記録。
        justPoint.NoteClass();
    }

    public void Non() // ノーツが近くになかった場合の処理
    {
        // 入力記録をリセット
        Dkey = false;
        Fkey = false;
        Jkey = false;
        Kkey = false;
    }

    public void Miss() 
    {
        Note = Notes.Substring(0, 1);// ノーツを見逃したときも、ノーツの記録は１つ消す
        Debug.Log(Note);

        if (Note == "W") // ウォークノーツならミスでもAnimation再生
        {
            if (Place == 0)
            {
                Place = 1;
            }
            else if (Place == 1)
            {
                Place = 0;
            }

            playerAnimation.Walk();
        }

        Notes = Notes.Remove(0, 1);
    }

    public void Animation()
    {
        Note = Notes.Substring(0, 1); // 次のノーツを取得

        if (Note ==  "N" && Fkey == true) // ノーマルノーツなら
        {
            Fkey = false;　// キーの記録をリセット
            Notes = Notes.Remove(0, 1); // ノーツの記録を１つ消す

            justPoint.Correct(); // スコア加算などをするため別プログラムへ

            playerAnimation.Normal();　// 通常モーション再生へ

        }
        else if (Note == "G" && Jkey == true) // ガードノーツなら
        {
            Jkey = false; // キーの記録をリセット
            Notes = Notes.Remove(0, 1);

            justPoint.Correct();
            playerAnimation.Guard(); // 防御ノーション再生へ
        }
        else if (Note == "W" && Dkey == true) // ウォークノーツなら
        {
            Dkey = false; // キーの記録をリセット
            Notes = Notes.Remove(0, 1);

            justPoint.Correct();

            if (Place == 0)
            {
                Place = 1;
            }
            else if (Place == 1)
            {
                Place = 0;
            }

            playerAnimation.Walk(); // 歩きノーション再生へ

        }
        else if (Note == "A" && Kkey == true) // アタックノーツなら
        {
            Kkey = false; // キーの記録をリセット
            Notes = Notes.Remove(0, 1);

            justPoint.Correct();
            playerAnimation.Attack(); // 攻撃ノーション再生へ
        }
        else　// 入力を間違えている場合
        {
            Note = Notes.Substring(0, 1);

            if (Note == "W") // ウォークノーツならミスでもAnimation再生
            {
                if (Place == 0)
                {
                    Place = 1;
                }
                else if (Place == 1)
                {
                    Place = 0;
                }

                playerAnimation.Walk();
            }

            Notes = Notes.Remove(0, 1);

            justPoint.Wrong(); // 入力を間違えた場合のプログラムへ
        }
    }

    public void GameEnd()
    {
        PlayerPrefs.SetInt("Note", note); // ノーツの数を保存
    }
}

/*
 * ノーツの種類を決めて、そのノーツを流すプログラム。
 * ノーツに対して、押されたキーが正しいかどうかや、どのアニメーションを再生するかも判断します。
 * 
 * SpawnNote()
 * Note_Signal からノーツを出現させる命令を得たときに実行します。
 * Rondomで得た整数によって処理を変更し、どのノーツを流すのか判断します。
 * ノーツの流れ方にはある程度の決まりはありますが、これによって
 * この音楽ゲームの特徴である、”遊ぶごとに譜面(ノーツの順番)が変わる"を実現させています。
 * 
 * 〜〜〜〜Spawn()
 * Notes に譜面状況を追加し、それぞれのノーツを(10, 4, 0)に出現させます。
 * 
 * Update()
 * 待機ノーツがないときにRandomを再実行します。
 * D,F,J,Kキーの入力を受け取り、それぞれの処理をさせます。
 * 
 * 〜Key()
 * それぞれ押したキーを記録させ、JustPoint で近くにノーツがあるか調べます。
 * 
 * Non()
 * 調べた結果ノーツが近くになかった時に行います。入力状況をリセットさせます。
 * 
 * Miss()
 * 見逃したノーツを検出したときに実行します。
 * ノーツの記録を１つ消去し、ノーツも削除します。
 * 基本的に見逃した場合、アニメーションは再生させませんがウォークノーツ(W)だった場合のみ
 * アニメーションを再生します。
 * また、ウォークノーツは、奇数個目には前に移動し、偶数個目でもとの位置に戻るように、
 * キャラクターの位置も変えているため、変数Placeで移動も管理させます。
 * 
 * Animation()
 * 入力があったとき、JustPointの近くにのーつがあれば実行されます。
 * Notesより一番古い(JustPointに近い)ノーツを１つ読み取り、それと入力があっていれば、
 * キー入力の記録をリセットし、ノーツの記録を１つ削除。
 * JustPointでキー入力が正しかったときの処理を行います。(Correct())
 * PlayerAnimationでそれぞれのアニメーション再生を行います。
 * また、入力が違った場合にはMiss()とほぼ同じ処理をさせますが、
 * Wrongの場合は、JustPointのWrong()を実行させるようにします。
 * 
 * GameEnd()
 * ノーツの数をPlayerPrefsに記録させます。
 * これはミッションのボーダーラインの計算に使います。
 */