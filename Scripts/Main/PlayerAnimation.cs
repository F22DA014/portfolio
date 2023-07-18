using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // キャラクタ(プレイヤー)のアニメーション処理
    private Animator anim = null;

    private int Place = 0;

    [SerializeField] Wave wave;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Normal() // 通常モーション再生
    {
        anim.SetTrigger("Attack"); // モーション自体はAttackと同じ

        StartCoroutine(Wave());
    }

    public void Guard() // 防御モーション再生
    {
        anim.SetTrigger("Guard");
    }

    public void Walk() // 歩きモーション再生
    {
        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;

        anim.SetTrigger("Walk");
        if (Place == 0)
        {
            Place = 1;
            pos.x += 4f;
        }
        else
        {
            Place = 0;
            pos.x -= 4f;
        }
        myTransform.position = pos;  // 座標を設定
    }

    public void Attack() // 攻撃モーション再生
    {
        anim.SetTrigger("Attack");
    }

    IEnumerator Wave()
    {
        yield return new WaitForSeconds(0.5f);　// 0.5秒待つ
        Instantiate(wave, new Vector3(-4, -4.5f, 0), Quaternion.identity); // Waveを出す
    }
}

/*
 * アニメーションを制御するプログラム
 * ノーツと押したキーが正しければそれぞれのアニメーションを再生させます。
 * Normal()では、衝撃波のようなものを出すためWave()を実行します。
 * Walk()では位置も変えています。
 */