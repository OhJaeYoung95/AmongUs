using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Sprite> sprites;

    private bool[] crewStates = new bool[12];
    private float timer = 0.5f;
    private float distance = 11f;

    // Start is called before the first frame update
    void Start()
    {
        // 카메라안에서부터 크루원 생성
        for (int i = 0; i < 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0, distance));
        }   
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            // 0,11 사이의 랜덤값을 받아서 크루원 색 설정
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    // 떠다니는 크루원 생성함수
    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {   // 크루원 색 중복 검사
        if(!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;
            float angle = Random.Range(0f, 360f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;  // 카메라 영역 밖의 원형으로 생성되도록 설정

            //Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            // 스폰 위치에 따른 랜덤방향
            Vector3 direction;
            if (spawnPos.x >= 0 && spawnPos.y >= 0)
                direction = new Vector3(Random.Range(-1f, 0f), Random.Range(-1f, 0f), 0);
            else if(spawnPos.x < 0 && spawnPos.y >= 0)
                direction = new Vector3(Random.Range(0f, 1f), Random.Range(-1f, 0f), 0);
            else if(spawnPos.x >= 0 && spawnPos.y < 0)
                direction = new Vector3(Random.Range(-1f, 0f), Random.Range(0f, 1f), 0);
            else if(spawnPos.x < 0 && spawnPos.y < 0)
                direction = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
            else
                direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            float floatingSpeed = Random.Range(1f, 4f);
            float rotateSpeed = Random.Range(-3f, 3f);

            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();

            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction,
                floatingSpeed, rotateSpeed, Random.Range(0.5f, 1f));
        }
    }

    // 콜라이더 영역 밖을 나갈때 크루원 제거, 크루원색 초기화
    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>();
        if(crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}
