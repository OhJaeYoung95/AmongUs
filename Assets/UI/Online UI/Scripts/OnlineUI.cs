using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class OnlineUI : MonoBehaviour
{
    [SerializeField]
    private InputField nicknameInputField;
    [SerializeField]
    private GameObject createRoomUI;

    // �游��� ��ư
    public void OnClickCreateRoomButton()
    {   // �г��� �Է��� ���
        if(nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else    // �г��� �Է����� ���� ���
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void OnClickEnterGameRoomButton()
    {
        // �г��� �Է��� ���
        if (nicknameInputField.text != "")
        {
            var manager = AmongUsRoomManager.singleton;
            manager.StartClient();
        }
        else    // �г��� �Է����� ���� ���
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
