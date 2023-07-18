using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    float speed = 20;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        Vector3 posi = this.transform.position;
        if (posi.x >= 3)
        {
            Destroy(this.gameObject); 
        }
    }
}

/*
 * ノーマルノーツのアニメーションで衝撃波のようなものをだすプログラムです。
 * キャラクターの剣を振り下ろした位置から出現させ２０のスピードで右に動かします。
 */
