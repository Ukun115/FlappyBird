using System.Collections;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        //�Q�[���J�n����GameController��Find���Ă���
        gameController = GameObject.FindWithTag("GameController");
    }

    //�g���K�[����Exit������N���A�Ƃ݂Ȃ�
    private void OnTriggerExit2D(Collider2D other)
    {
        gameController.SendMessage("IncreaseScore");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
