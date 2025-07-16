using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float minHeight;     //�ő卂��
    public float maxHeight;     //�ő啝
    public GameObject pivot;    //��_

    //�I�u�W�F�N�g����������Ă��珉�߂̃t���[���ɌĂ΂��֐�
    void Start()
    {
        //�J�n���Ɍ��Ԃ̍�����ύX
        ChangeHeight();
    }

    void ChangeHeight()
    {
        //�����_���ȍ����𐶐����Đݒ�
        float height = Random.Range(minHeight, maxHeight);
        pivot.transform.localPosition = new Vector3(0.0f, height, 0.0f);
    }

    //ScrollObject�X�N���v�g����̃��b�Z�[�W���󂯎���č�����ύX
    void OnScrollEnd()
    {
        ChangeHeight();
    }
}
