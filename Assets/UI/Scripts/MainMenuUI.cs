using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnClickOnlineButton()
    {
        Debug.Log("Click Online");
    }

    public void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    // 에디터 상에서 플레이 종료
#else
        Application.Quit();     // 빌드된 상태라면 어플리케이션 종료
#endif
    }
}
