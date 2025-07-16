using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    //�ݒ�{�^��
    public GameObject settingButton;
    //�Q�[���^�C�����~���Ă��邩�ǂ���
    bool isStop = false;

    //�Q�[���J�n�{�^��&�Q�[���ĊJ�n�{�^��
    public GameObject gameStartOrRestartButton;
    //�Q�[�����J�n���ꂽ���ǂ���
    bool isGameStart = false;

    public Sprite restartButtonSprite;

    public AudioClip pushSoundClip;
    AudioSource pushSoundSource;

    void Start()
    {
        //AudioSource�R���|�[�l���g�̎擾
        pushSoundSource = GetComponent<AudioSource>();
    }

    //�ݒ�{�^���������ꂽ��Ă΂��֐�
    public void PushSettingButton()
    {
        //�Q�[���^�C����~����������A
        if(isStop)
        {
            //�Q�[���^�C���𓮂���
            Time.timeScale = 1f;
            //��~���Ă��Ȃ�����ɂ���
            isStop = !isStop;
        }
        //�Q�[���^�C���ғ�����������A
        else
        {
            //�Q�[���^�C�����~�߂�
            Time.timeScale = 0f;
            //��~���Ă��锻��ɂ���
            isStop = !isStop;
        }

        //�v�b�V�����ʉ��Đ�
        pushSoundSource.PlayOneShot(pushSoundClip);
    }

    //�Q�[���J�n�{�^���������ꂽ��Ă΂��֐�
    public void PushGameStartButton()
    {
        isGameStart = true;
        gameStartOrRestartButton.SetActive(false);
        //�X�v���C�g���ĊJ�n�{�^���̃X�v���C�g�ɕύX���Ă���
        gameStartOrRestartButton.GetComponent<Image>().sprite = restartButtonSprite;
    }

    //�Q�[���J�n���ꂽ���ǂ������擾����Q�b�^�[
    public bool GetIsGameStart() { return isGameStart; }

    //�Q�[���ĊJ�n�{�^����\��������֐�
    public void ActiveGameRestartButton()
    {
        gameStartOrRestartButton.SetActive(true);
        isGameStart = false;
    }
}
