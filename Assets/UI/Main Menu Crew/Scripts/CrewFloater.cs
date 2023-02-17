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
        // ī�޶�ȿ������� ũ��� ����
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
            // 0,11 ������ �������� �޾Ƽ� ũ��� �� ����
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    // ���ٴϴ� ũ��� �����Լ�
    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {   // ũ��� �� �ߺ� �˻�
        if(!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;
            float angle = Random.Range(0f, 360f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;  // ī�޶� ���� ���� �������� �����ǵ��� ����

            //Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            // ���� ��ġ�� ���� ��������
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

    // �ݶ��̴� ���� ���� ������ ũ��� ����, ũ����� �ʱ�ȭ
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
