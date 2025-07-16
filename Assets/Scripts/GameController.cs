using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //ゲームステート
    enum State
    {
        Ready,
        CountDown,
        Play,
        GameOver
    }

    State state;
    int nowScore;
    int hightScore;

    public AzarashiController azarashi;
    public GameObject blocks;
    public Text scoreLabel;
    public Text readyLabel;
    public Text groundTachLabel;
    public Text gameOverLabel;
    public Text hightScoreLabel;
    public Text nowScoreLabel;
    public Text countDownLabel;

    public GameObject canvasObject;
    ButtonEvents buttonEventsScript;

    //ゲームオブジェクトが生成されてから初めの1フレームだけ呼ばれる関数
    void Start()
    {
        //開始と同時にReadyステートに移行
        Ready();

        //過去のハイスコアをロード
        hightScore = PlayerPrefs.GetInt("HIGHTSCORE",0);

        buttonEventsScript = canvasObject.GetComponent<ButtonEvents>();
    }

    //更新関数
    void LateUpdate()
    {
        //ゲームのステートごとにイベントを監視
        switch(state)
        {
            case State.Ready:
                //再生ボタンタッチしたらカウントダウンスタート
                if (buttonEventsScript.GetIsGameStart()) CountDownStart();
                break;
            case State.CountDown:
                break;
            case State.Play:
                //キャラクターが死亡したらゲームオーバー
                if (azarashi.IsDead()) GameOver();
                break;
            case State.GameOver:
                //タッチしたらシーンをリロード
                if (buttonEventsScript.GetIsGameStart()) Reload();
                break;
        }
    }

    void Ready()
    {
        state = State.Ready;

        //各オブジェクトを無効状態にする
        azarashi.SetSteerActive(false);
        blocks.SetActive(false);

        //ラベルを更新
        scoreLabel.text = "Score : " + 0;

        readyLabel.gameObject.SetActive(true);
        readyLabel.text = "Ready";

        Debug.Log("ゲーム起動");
    }

    void CountDownStart()
    {
        StartCoroutine("CountDownCoroutine");

        state = State.CountDown;
    }

    //カウントダウンコルーチン
    IEnumerator CountDownCoroutine()
    {
        //ラベルを更新
        readyLabel.gameObject.SetActive(false);
        groundTachLabel.gameObject.SetActive(false);

        countDownLabel.gameObject.SetActive(true);


        for(int countDownNo = 3;countDownNo > 0; countDownNo--)
        {
            countDownLabel.text = countDownNo.ToString();
            yield return new WaitForSeconds(0.5f);

        }
        countDownLabel.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        countDownLabel.gameObject.SetActive(false);
        countDownLabel.text = "";

        GameStart();
    }

    void GameStart()
    {
        state = State.Play;

        //各オブジェクトを有効にする
        azarashi.SetSteerActive(true);
        blocks.SetActive(true);

        //最初の入力だけゲームコントローラーから渡す
        azarashi.Flap();

        Debug.Log("ゲームが開始されました。");
    }

    void GameOver()
    {
        state = State.GameOver;

        //シーンの中のすべてのScrollObjectコンポーネントを探し出す
        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();

        //全ScrollObjectのスクロール処理を無効にする
        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        //ラベルを更新
        gameOverLabel.gameObject.SetActive(true);
        gameOverLabel.text = "GameOver";

        //リザルトに出すハイスコアと今回のスコアを表示
        hightScoreLabel.gameObject.SetActive(true);
        hightScoreLabel.text = "HightScore : " + hightScore;
        nowScoreLabel.gameObject.SetActive(true);
        nowScoreLabel.text = "NowScore  : " + nowScore;

        //今回のスコアのほうが過去のハイスコアよりも高かったらハイスコア更新
        if(hightScore < nowScore)
        {
            //スコアを保存
            PlayerPrefs.SetInt("HIGHTSCORE", nowScore);
            PlayerPrefs.Save();
        }

        //ゲーム再開始ボタンを表示させる
        buttonEventsScript.ActiveGameRestartButton();
    }

    void Reload()
    {
        //現在読み込んでいるシーンを再読込み
        SceneManager.GetActiveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore()
    {
        nowScore++;
        scoreLabel.text = "Score : " + nowScore;
    }
}
