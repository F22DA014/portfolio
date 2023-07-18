using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    //ノーツのスピードを設定
    float speed = 20;

    void Update()
    {
        //ノーツを左方向に移動させる
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
}

/*
 * ノーツ自体につけるプログラム。
 * ノーツのスピードを設定し、左方向に移動させています。
 */