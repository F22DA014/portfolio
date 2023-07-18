using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPoint : MonoBehaviour
{
    [SerializeField] JustPoint justPoint;

    private void Update()
    {
        // 近くにノーツがあるか調べる
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 5, Vector3.zero);

        if (hit2D)　// もしあったら
        {
            // そのノーツとOutpointの距離を調べる
            float distance = Mathf.Abs(hit2D.transform.position.x - transform.position.x);

            if (distance <= 0.1) // 距離が0.1以下なら
            {
                justPoint.Miss();       //Miss()を実行
                Destroy(hit2D.collider.gameObject);  // ノーツを削除
            }
        }
    }
}

/*
 * JustPointの左側に存在するOutPointに紐付けるプログラムです。
 * 常に実行し続け、ノーツが触れたら、見逃したと判断し、Miss()を実行します。
 */