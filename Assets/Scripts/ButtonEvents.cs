using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    //設定ボタン
    public GameObject settingButton;
    //ゲームタイムを停止しているかどうか
    bool isStop = false;

    //ゲーム開始ボタン&ゲーム再開始ボタン
    public GameObject gameStartOrRestartButton;
    //ゲームが開始されたかどうか
    bool isGameStart = false;

    public Sprite restartButtonSprite;

    public AudioClip pushSoundClip;
    AudioSource pushSoundSource;

    void Start()
    {
        //AudioSourceコンポーネントの取得
        pushSoundSource = GetComponent<AudioSource>();
    }

    //設定ボタンが押されたら呼ばれる関数
    public void PushSettingButton()
    {
        //ゲームタイム停止中だったら、
        if(isStop)
        {
            //ゲームタイムを動かす
            Time.timeScale = 1f;
            //停止していない判定にする
            isStop = !isStop;
        }
        //ゲームタイム稼働中だったら、
        else
        {
            //ゲームタイムを止める
            Time.timeScale = 0f;
            //停止している判定にする
            isStop = !isStop;
        }

        //プッシュ効果音再生
        pushSoundSource.PlayOneShot(pushSoundClip);
    }

    //ゲーム開始ボタンが押されたら呼ばれる関数
    public void PushGameStartButton()
    {
        isGameStart = true;
        gameStartOrRestartButton.SetActive(false);
        //スプライトを再開始ボタンのスプライトに変更しておく
        gameStartOrRestartButton.GetComponent<Image>().sprite = restartButtonSprite;
    }

    //ゲーム開始されたかどうかを取得するゲッター
    public bool GetIsGameStart() { return isGameStart; }

    //ゲーム再開始ボタンを表示させる関数
    public void ActiveGameRestartButton()
    {
        gameStartOrRestartButton.SetActive(true);
        isGameStart = false;
    }
}
