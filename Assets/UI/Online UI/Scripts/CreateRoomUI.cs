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

    // 크루원 이미지 설정(업데이트)
    private void UpdateCrewImages()
    {
        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while (imposterCount != 0)
        {   // 임포스터수 만큼
            if(idx >= roomData.imposterCount)
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
}

public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}
