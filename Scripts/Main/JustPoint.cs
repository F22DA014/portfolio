using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustPoint : MonoBehaviour
{
    [SerializeField] Note_Generator note_Generator = default;

    [SerializeField] private Text Score; //スコア表示用テキスト
    [SerializeField] private Text Combo; //コンボ数表示用テキスト
    [SerializeField] private Text Class; //クラス表示用テキスト

    private int score; //スコア代入変数
    private int combo; //コンボ数代入変数
    private int maxcombo;　//最大コンボ数を記憶する変数。
    private int mandw; // MISSとWRONGの数を数える(※MISSandWRONG)
    private float magnification; //コンボ数によるスコア倍率

    private float distance;  // ノーツとジャストポイントの距離代入変数

    private int i, j; //配列用変数
    int[] scores = new int[10];　//スコア保存用配列

    public void Start()
    {
        i = 0;
        j = 0;
        score = 0;
        combo = 0;
        maxcombo = 0;
        distance = 0; //初期化
        Score.text = string.Format("SCORE:{0}", score); //スコアは0でも表示。コンボは不要
    }

    public void NoteClass() // タイミングの評価、スコア・コンボの制御など
    {
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 5, Vector3.zero); //近くにノーツがあるか調べる。
        if (hit2D) //ノーツがあれば
        {
            //　アニメーションを再生するため別プログラムへ
            note_Generator.Animation();

            //　ノーツとJustPointとの近さを調べる
            distance = Mathf.Abs(hit2D.transform.position.x - transform.position.x);

            //　ノーツを消す
            Destroy(hit2D.collider.gameObject);
        }
        else
        {
            // ノーツが近くにないため別の処理
            note_Generator.Non();
        }
    }

    public void Correct() // ノーツと入力が正しかったときの処理
    {
        // 距離によってノーツの評価を変える
        if (distance < 0.5)
        {
            //　PERFECT
            Class.text = string.Format("PERFECT"); //ノーツの評価を表示

            ComboPlus();        //コンボ加算プログラムへ(GOOD以上で)
            score += (int)(1000 * magnification);　// スコア加算
        }
        else if (distance < 1)
        {
            // GREAT
            Class.text = string.Format("GREAT");

            ComboPlus();
            score += (int)(700 * magnification);
        }
        else if (distance < 2)
        {
            // GOOD
            Class.text = string.Format("GOOD");

            ComboPlus();
            score += (int)(500 * magnification);
        }
        else
        {
            // BUT
            Class.text = string.Format("BAD");

            score += 100;
            combo = 0; //スコアは加算するがコンボ数はリセットする。
        }

        // スコアを表示
        Score.text = string.Format("SCORE:{0}", score);

        scores[i] = score; //配列に今のスコアを代入
        i++;
        if (i == 10)　//配列の要素は10個しか用意していないため、iが10になったらリセット
        {
            i = 0;
        }

        // 1秒後にClassDaleteを実行
        Invoke(nameof(ClassDelete), 1);

        // コンボ関係のTextの設定
        if (combo == 0)
        {
            Combo.text = (""); //0のときは何も表示しない。
        }
        else
        {
            Combo.text = string.Format("{0}COMBO!", combo); //コンボを表示
        }

        // 必要に応じて、maxcomboを更新
        if (combo > maxcombo)
        {
            maxcombo = combo;
        }
    }

    private void ComboPlus()
    {
        combo += 1;             //コンボを加算。

        magnification = 1 + (combo - 1) * 0.1f; //倍率数式。コンボ数によって変更
    }

   public void Wrong() // 入力を間違えたときの処理
    {
        // WRONG
        Class.text = string.Format("WRONG");

        combo = 0; //スコアは加算されない。コンボもリセット
        Combo.text = ("");
        mandw++;

        scores[i] = score;

        i++;
        if (i == 10)
        {
            i = 0;
        }

        Invoke(nameof(ClassDelete), 1);
    }

    public void Miss() //ノーツを見の逃したときの処理
    {
        // MISS
        note_Generator.Miss();

        Class.text = string.Format("MISS");

        combo = 0; //スコアは加算されない。コンボもリセット
        Combo.text = ("");
        mandw++;

        scores[i] = score;

        i++;
        if (i == 10)
        {
            i = 0;
        }

        Invoke(nameof(ClassDelete), 1);
    }

    public void ClassDelete() // クラスの表示を削除するかどうか判断する処理
    {
        if (scores[j] == score) //現在のスコアが保存した時のスコアと等しければ
        {
            Class.text = string.Format(""); //クラスの表示を消す。
        }
        j++;
        if (j == 10) //iと同じで10になったらリセット
        {
            j = 0;
        }
    }

    public void GameEnd()
    {
        PlayerPrefs.SetInt("Score", score); // スコアをPlayerPrefsに保存
        PlayerPrefs.SetInt("MaxCombo", maxcombo); // 最大コンボ数をPlayerPrefsに保存
    }
}

/*
 * 流れるノーツに対して、ボタンを押すジャストのポイント(JustPoint)のプログラム。
 * 
 * NoteClass()
 * このゲームで使う、D,F,J,Kのいずれかが押されたときに呼び出され、
 * 近くにノーツがあれば、アニメーションの再生プログラムを呼び出したり、
 * ノーツとの距離を調べ、ノーツを消す処理を行い、ない場合はノーツがなかった場合の処理をNote_Generatorで行います。
 * 
 * Correct() ,  Wrong() ,  Miss()
 * Note_Generatorを経由して、ノーツに対して、押したボタンが正しいかどうかを判定し、
 * 正しければ Correct() 間違っていれば Wrong() を実行し、ノーツとJustPointの距離によって、
 * 判定をかえてそれぞれの処理を実行します。
 * また、見逃したノーツを検出したときは、 Miss() の処理を行います。
 * 
 * ClassDelete()
 * 表示したClassのテキスト(PERFECT、MISSなど)を消去して良いかを判断します。
 * 判断には配列を使い、Classのテキストが変わるごとにスコアを代入し、一定時間スコアが同じなら、
 * 表示を消すようにしています。今回はノーツの密度は低かったため、配列の要素数は１０個に設定し、
 * それをループするようにしています。
 * 
 * GameEnd()
 * 終了後に実行します。スコアと最大コンボ数を、PlayerPrefsに保存し GameManeger で使用します。
 */