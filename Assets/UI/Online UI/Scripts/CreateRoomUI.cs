using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> crewImgs;

    [SerializeField]
    private List<Button> imposterCountButtons;

    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        // ũ��� material �ν��Ͻ�ȭ
        for (int i = 0; i < crewImgs.Count; i++)
        {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }
        roomData = new CreateGameRoomData { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    // �������� �� ����
    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        // �������� �� ��ư�� ������ �׵θ� Ȱ��ȭ
        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if(i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        // �������� ���� ���� �ִ��ο��� ���ѿ� �ɸ��� �ִ��ο��� ����
        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 7 : 9;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        // �ִ��ο��� ���ѿ� ���� ��ư Ȱ��ȭ, ��Ȱ��ȭ / �ؽ�Ʈ �� Ȱ��ȭ(���), ��Ȱ��ȭ(ȸ��)
        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();
            if(i < limitMaxPlayer - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    // �ִ� �ο��� ����
    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        // �ִ� �ο��� ��ư�� ������ �׵θ� Ȱ��ȭ
        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if(i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        UpdateCrewImages();
    }

    // ũ��� �̹��� ����(������Ʈ)
    private void UpdateCrewImages()
    {   // �ʱ� ũ��� ��(�Ͼ��)����
        for (int i = 0; i < crewImgs.Count; i++)
        {
            crewImgs[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while (imposterCount != 0)
        {   // �������ͼ� ��ŭ
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }
            // ������ ũ��� �������� ��(������)���� ����
            if (crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0,5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        // �ִ��ο��� ��ŭ ũ��� �̹��� Ȱ��ȭ
        for (int i = 0; i < crewImgs.Count; i++)
        {
            if(i < roomData.maxPlayerCount)
            {
                crewImgs[i].gameObject.SetActive(true);
            }
            else
            {
                crewImgs[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {
        var manager = AmongUsRoomManager.singleton;
        // �� ���� �۾� ó��
        //
        //
        manager.StartHost();
    }
}

public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}
