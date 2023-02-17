using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private Button MouseControlButton;
    [SerializeField]
    private Button KeyboardMouseControlButton;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()     // ���ӿ�����Ʈ Ȱ��ȭ��
    {
        // ���۹�Ŀ� ���� ��ư�� ��ȭ
        switch(PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    // ���۹�ưŬ����, �ش��ư�� ��ȭ
    public void SetControlMode(int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;
        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    // Settings UI �ݴ� �Լ�
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");   // open �ִϸ����� �����
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);    // ������Ʈ ��Ȱ��ȭ
        animator.ResetTrigger("close"); // Ʈ���� ���� �ʱ�ȭ
    }
}
