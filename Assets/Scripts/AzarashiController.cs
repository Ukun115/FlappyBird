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

    //Awake�֐��̓I�u�W�F�N�g���������ꂽ�u�ԂɌĂ΂�邽�߁A
    //�R���|�[�l���g�̎擾��S�ẴI�u�W�F�N�g��Start�֐���葁���i�K�ōs����B
    //������Start�֐����g�킸��Awake�֐����g���Ă���B
    void Awake()
    {
        //Rigidbody2D�R���|�[�l���g�̎擾
        rb2d = GetComponent<Rigidbody2D>();
        //Animator�R���|�[�l���g�̎擾
        animator = sprite.GetComponent<Animator>();

        //AudioSource�R���|�[�l���g�̎擾
        jumpSoundSource = GetComponent<AudioSource>();
        deathSoundSource = GetComponent<AudioSource>();

        //for(int groundNum = 0;groundNum < totalGroundNum;groundNum++)
        //{
        //    groundTapIventScript[groundNum] = GroundObjectArray[groundNum].GetComponent<GroundTapIvent>();
        //}
    }

    //�X�V�֐�
    void Update()
    {
        ////�ō��d�x�ɒB���Ă��Ȃ��ꍇ�Ɍ���^�b�v�̓��͂��󂯕t����
        //for (int groundNum = 0; groundNum < totalGroundNum; groundNum++)
        //{
        //    if (groundTapIventScript[groundNum].GetIsTap() && transform.position.y < maxHeight)
        //    {
        //        Flap(groundNum);
        //    }
        //}

        //���N���b�N�������ꂽ��A
        if (Input.GetMouseButtonDown(0))
        {
            //��ʉ�����������A
            if (Screen.height / 4 > Input.mousePosition.y)
            {
                //�W�����v�I
                Flap();
            }
        }

            //�p�x�𔽉f
            ApplyAngle();

        //angle�������ȏゾ������A�A�j���[�^�[��flap�t���O��true�ɂ���
        animator.SetBool("flap", angle >= 0.0f);
    }

    public void Flap()//(int groundNum = 0)
    {
        //���񂾂�͂΂����Ȃ�
        if (isDead) return;

        //�d�͂������Ă��Ȃ��Ƃ��͑��삵�Ȃ�
        if (rb2d.isKinematic) return;

        //Velocity�𒼐ڏ��������ď�����ɉ���
            rb2d.velocity = new Vector2(0.0f, flapVelocity);

        //�W�����v���ʉ��Đ�
        jumpSoundSource.PlayOneShot(jumpSoundClip);

        ////�n�ʂ��^�b�v���ꂽ�Ƃ�������𖳂���
        //groundTapIventScript[groundNum].SetIsTap(false);
    }

    void ApplyAngle()
    {
        //���݂̑��x�A���Α��x����i��ł���p�x�����߂�
        float targetAngle;

        //���S�������ɉ�������
        if(isDead)
        {
            targetAngle = -90.0f;
        }
        else
        {
            targetAngle =
                Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
        }

        //��]�A�j���[�V�������X���[�W���O
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //Rotation�̔��f
        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        //�����ɂԂ������玀�S�t���O�𗧂Ă�
        isDead = true;

        //���S���ʉ��Đ�
        deathSoundSource.PlayOneShot(deathSoundClip);
    }

    public void SetSteerActive(bool active)
    {
        //Rigidbody�̃I���A�I�t��؂�ւ���
        rb2d.isKinematic = !active;
    }
}
