using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTapIvent : MonoBehaviour
{
    //�^�b�v���ꂽ���ǂ���
    bool isTap = false;

    //�n�ʂ��^�b�v���ꂽ�Ƃ��A
    public void OnClicActor()
    {
        //�^�b�v���ꂽ����ɂ���
        isTap = true;

        //�f�o�b�N
        Debug.Log("�n�ʂ��^�b�v����܂����B");
    }

    //�^�b�v���ꂽ���ǂ�����ݒ肷��Z�b�^�[
    public void SetIsTap(bool istap) { isTap = istap; }
    //�^�b�v���ꂽ���ǂ������擾����Q�b�^�[
    public bool GetIsTap() { return isTap; }
}