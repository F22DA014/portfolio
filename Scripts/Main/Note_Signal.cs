using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Signal : MonoBehaviour
{
    // 音楽に合わせてノーツを出現させる命令をだすスクリプト

    [SerializeField] Note_Generator noteGenerator = default;

    public void Note_Event()
    {
        noteGenerator.SpawnNote(); //ノーツ出現プログラムへ
    }
}

/*
 * アセットのプログラムより命令を受け取りノーツを出現させる命令を送るプログラムです。
 */