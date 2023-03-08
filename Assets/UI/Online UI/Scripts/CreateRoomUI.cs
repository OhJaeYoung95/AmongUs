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
        // 크루원 material 인스턴스화
        for (int i = 0; i < crewImgs.Count; i++)
        {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }
        roomData = new CreateGameRoomData { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    // 임포스터 수 설정
    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        // 임포스터 수 버튼을 눌러서 테두리 활성화
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

        // 임포스터 수에 따른 최대인원수 제한에 걸리면 최대인원수 설정
        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 7 : 9;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        // 최대인원수 제한에 따른 버튼 활성화, 비활성화 / 텍스트 색 활성화(흰색), 비활성화(회색)
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

    // 최대 인원수 설정
    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        // 최대 인원수 버튼을 눌러서 테두리 활성화
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

    // 크루원 이미지 설정(업데이트)
    private void UpdateCrewImages()
    {   // 초기 크루원 색(하얀색)설정
        for (int i = 0; i < crewImgs.Count; i++)
        {
            crewImgs[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while (imposterCount != 0)
        {   // 임포스터수 만큼
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }
            // 랜덤한 크루원 임포스터 색(빨간색)으로 설정
            if (crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0,5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        // 최대인원수 만큼 크루원 이미지 활성화
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
        // 방 설정 작업 처리
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
