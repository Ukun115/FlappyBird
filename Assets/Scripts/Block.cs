using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float minHeight;     //最大高さ
    public float maxHeight;     //最大幅
    public GameObject pivot;    //基点

    //オブジェクトが生成されてから初めのフレームに呼ばれる関数
    void Start()
    {
        //開始時に隙間の高さを変更
        ChangeHeight();
    }

    void ChangeHeight()
    {
        //ランダムな高さを生成して設定
        float height = Random.Range(minHeight, maxHeight);
        pivot.transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }

    //ScrollObjectスクリプトからのメッセージを受け取って高さを変更
    void OnScrollEnd()
    {
        ChangeHeight();
    }
}
