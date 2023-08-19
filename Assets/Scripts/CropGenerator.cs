using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropGenerator : MonoBehaviour
{

    //스폰 관련 변수
    //산삼 출현할 위치를 담은 배열
    public Transform[] points = null;
    public bool[] isCropExists = new bool[12];
    public Sprite[] sprites = new Sprite[5];
    //산삼 프리팹을 할당할 변수
    public GameObject CropPrefab;
    //산삼을 미리 생성해 저장할 리스트 자료형
    public List<GameObject> CropPool = new List<GameObject>();
    //산삼 발생시킬 주기
    public float SpwanTime = 1.0f;
    //산삼 최대 발생 개수
    public int maxCrop = 12;
    //스폰 관련 변수
    int cropIdx = 0;
    //게임 종료 여부 변수
    public bool isGameOver = false;

    bool cycle = false;
    Coroutine coroutine = null;

    public static CropGenerator Inst = null;
    void Awake()
    {
        Inst = this;
        for (int i = 0; i < maxCrop; i++)
        {
            isCropExists[i] = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //산삼을 생성해 오브젝트 풀에 저장
        for (int i = 0; i < maxCrop; i++)
        {
            //산삼 프리팹을 생성
            GameObject Crop = (GameObject)Instantiate(CropPrefab);
            Crop.GetComponent<SpriteRenderer>().sprite = sprites[cropIdx];

            //생성한 산삼을 비활성화
            Crop.SetActive(false);
            //생성한  산삼을 오브젝트 풀에 추가
            CropPool.Add(Crop);
            cropIdx++;
            cropIdx %= 5;

        }

        points = GameObject.Find("CropRoot").GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length > 0 && coroutine == null)
        {
            //산삼 생성 코루틴 함수 호출
            coroutine = StartCoroutine(CreateCrop());
        }
        if (cycle && coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
            cycle = false;
        }
    }

    IEnumerator CreateCrop()
    {
        //yield return new WaitForSeconds(SpwanTime);

        //게임이 끝났을때 코루틴 종료
        if (isGameOver) yield break;

        //오브젝트 풀의 처음부터 끝까지 순회
        foreach (GameObject Crop in CropPool)
        {
            if (!Crop.activeSelf)
            {
                while (true)
                {
                    int idx = Random.Range(1, points.Length);
                    if (!isCropExists[idx - 1])
                    {
                        Crop.transform.position = points[idx].position;
                        isCropExists[idx - 1] = true;
                        break;
                    }
                }
                yield return new WaitForSeconds(SpwanTime);
                Crop.SetActive(true);
            }
        }
        yield return new WaitForSeconds(5.0f);
        cycle = true;
        for (int i = 0; i < maxCrop; i++)
        {
            isCropExists[i] = false;
        }
    }
}
