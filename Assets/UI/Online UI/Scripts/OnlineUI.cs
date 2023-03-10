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

    // 방만들기 버튼
    public void OnClickCreateRoomButton()
    {   // 닉네임 입력한 경우
        if(nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else    // 닉네임 입력하지 않은 경우
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }

    public void OnClickEnterGameRoomButton()
    {
        // 닉네임 입력한 경우
        if (nicknameInputField.text != "")
        {
            var manager = AmongUsRoomManager.singleton;
            manager.StartClient();
        }
        else    // 닉네임 입력하지 않은 경우
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }
}
