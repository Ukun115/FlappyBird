using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTapIvent : MonoBehaviour
{
    //タップされたかどうか
    bool isTap = false;

    //地面がタップされたとき、
    public void OnClicActor()
    {
        //タップされた判定にする
        isTap = true;

        //デバック
        Debug.Log("地面がタップされました。");
    }

    //タップされたかどうかを設定するセッター
    public void SetIsTap(bool istap) { isTap = istap; }
    //タップされたかどうかを取得するゲッター
    public bool GetIsTap() { return isTap; }
}