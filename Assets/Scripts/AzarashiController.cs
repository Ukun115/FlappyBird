using System.Collections;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;

    //public GameObject[] GroundObjectArray = new GameObject[4];
    //GroundTapIvent[] groundTapIventScript = new GroundTapIvent[4];
    //int totalGroundNum = 4;

    public AudioClip jumpSoundClip;
    AudioSource jumpSoundSource;
    public AudioClip deathSoundClip;
    AudioSource deathSoundSource;

    public bool IsDead()
    {
        return isDead;
    }

    //Awake関数はオブジェクトが生成された瞬間に呼ばれるため、
    //コンポーネントの取得を全てのオブジェクトのStart関数より早い段階で行える。
    //だからStart関数を使わずにAwake関数を使っている。
    void Awake()
    {
        //Rigidbody2Dコンポーネントの取得
        rb2d = GetComponent<Rigidbody2D>();
        //Animatorコンポーネントの取得
        animator = sprite.GetComponent<Animator>();

        //AudioSourceコンポーネントの取得
        jumpSoundSource = GetComponent<AudioSource>();
        deathSoundSource = GetComponent<AudioSource>();

        //for(int groundNum = 0;groundNum < totalGroundNum;groundNum++)
        //{
        //    groundTapIventScript[groundNum] = GroundObjectArray[groundNum].GetComponent<GroundTapIvent>();
        //}
    }

    //更新関数
    void Update()
    {
        ////最高硬度に達していない場合に限りタップの入力を受け付ける
        //for (int groundNum = 0; groundNum < totalGroundNum; groundNum++)
        //{
        //    if (groundTapIventScript[groundNum].GetIsTap() && transform.position.y < maxHeight)
        //    {
        //        Flap(groundNum);
        //    }
        //}

        //左クリックが押されたら、
        if (Input.GetMouseButtonDown(0))
        {
            //画面下部だったら、
            if (Screen.height / 4 > Input.mousePosition.y)
            {
                //ジャンプ！
                Flap();
            }
        }

            //角度を反映
            ApplyAngle();

        //angleが水平以上だったら、アニメーターのflapフラグをtrueにする
        animator.SetBool("flap", angle >= 0.0f);
    }

    public void Flap()//(int groundNum = 0)
    {
        //死んだらはばたけない
        if (isDead) return;

        //重力が効いていないときは操作しない
        if (rb2d.isKinematic) return;

        //Velocityを直接書き換えて上方向に加速
            rb2d.velocity = new Vector2(0.0f, flapVelocity);

        //ジャンプ効果音再生
        jumpSoundSource.PlayOneShot(jumpSoundClip);

        ////地面がタップされたという判定を無くす
        //groundTapIventScript[groundNum].SetIsTap(false);
    }

    void ApplyAngle()
    {
        //現在の速度、相対速度から進んでいる角度を求める
        float targetAngle;

        //死亡したら常に下を向く
        if(isDead)
        {
            targetAngle = -90.0f;
        }
        else
        {
            targetAngle =
                Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
        }

        //回転アニメーションをスムージング
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //Rotationの反映
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        //何かにぶつかったら死亡フラグを立てる
        isDead = true;

        //死亡効果音再生
        deathSoundSource.PlayOneShot(deathSoundClip);
    }

    public void SetSteerActive(bool active)
    {
        //Rigidbodyのオン、オフを切り替える
        rb2d.isKinematic = !active;
    }
}
