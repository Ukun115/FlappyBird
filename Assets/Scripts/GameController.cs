using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //�Q�[���X�e�[�g
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

    //�Q�[���I�u�W�F�N�g����������Ă��珉�߂�1�t���[�������Ă΂��֐�
    void Start()
    {
        //�J�n�Ɠ�����Ready�X�e�[�g�Ɉڍs
        Ready();

        //�ߋ��̃n�C�X�R�A�����[�h
        hightScore = PlayerPrefs.GetInt("HIGHTSCORE",0);

        buttonEventsScript = canvasObject.GetComponent<ButtonEvents>();
    }

    //�X�V�֐�
    void LateUpdate()
    {
        //�Q�[���̃X�e�[�g���ƂɃC�x���g���Ď�
        switch(state)
        {
            case State.Ready:
                //�Đ��{�^���^�b�`������J�E���g�_�E���X�^�[�g
                if (buttonEventsScript.GetIsGameStart()) CountDownStart();
                break;
            case State.CountDown:
                break;
            case State.Play:
                //�L�����N�^�[�����S������Q�[���I�[�o�[
                if (azarashi.IsDead()) GameOver();
                break;
            case State.GameOver:
                //�^�b�`������V�[���������[�h
                if (buttonEventsScript.GetIsGameStart()) Reload();
                break;
        }
    }

    void Ready()
    {
        state = State.Ready;

        //�e�I�u�W�F�N�g�𖳌���Ԃɂ���
        azarashi.SetSteerActive(false);
        blocks.SetActive(false);

        //���x�����X�V
        scoreLabel.text = "Score : " + 0;

        readyLabel.gameObject.SetActive(true);
        readyLabel.text = "Ready";

        Debug.Log("�Q�[���N��");
    }

    void CountDownStart()
    {
        StartCoroutine("CountDownCoroutine");

        state = State.CountDown;
    }

    //�J�E���g�_�E���R���[�`��
    IEnumerator CountDownCoroutine()
    {
        //���x�����X�V
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

        //�e�I�u�W�F�N�g��L���ɂ���
        azarashi.SetSteerActive(true);
        blocks.SetActive(true);

        //�ŏ��̓��͂����Q�[���R���g���[���[����n��
        azarashi.Flap();

        Debug.Log("�Q�[�����J�n����܂����B");
    }

    void GameOver()
    {
        state = State.GameOver;

        //�V�[���̒��̂��ׂĂ�ScrollObject�R���|�[�l���g��T���o��
        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();

        //�SScrollObject�̃X�N���[�������𖳌��ɂ���
        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        //���x�����X�V
        gameOverLabel.gameObject.SetActive(true);
        gameOverLabel.text = "GameOver";

        //���U���g�ɏo���n�C�X�R�A�ƍ���̃X�R�A��\��
        hightScoreLabel.gameObject.SetActive(true);
        hightScoreLabel.text = "HightScore : " + hightScore;
        nowScoreLabel.gameObject.SetActive(true);
        nowScoreLabel.text = "NowScore  : " + nowScore;

        //����̃X�R�A�̂ق����ߋ��̃n�C�X�R�A��������������n�C�X�R�A�X�V
        if(hightScore < nowScore)
        {
            //�X�R�A��ۑ�
            PlayerPrefs.SetInt("HIGHTSCORE", nowScore);
            PlayerPrefs.Save();
        }

        //�Q�[���ĊJ�n�{�^����\��������
        buttonEventsScript.ActiveGameRestartButton();
    }

    void Reload()
    {
        //���ݓǂݍ���ł���V�[�����ēǍ���
        SceneManager.GetActiveScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseScore()
    {
        nowScore++;
        scoreLabel.text = "Score : " + nowScore;
    }
}
