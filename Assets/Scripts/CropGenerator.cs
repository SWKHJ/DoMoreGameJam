using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropGenerator : MonoBehaviour
{

    //���� ���� ����
    //��� ������ ��ġ�� ���� �迭
    public Transform[] points = null;
    public bool[] isCropExists = new bool[12];
    public Sprite[] sprites = new Sprite[5];
    //��� �������� �Ҵ��� ����
    public GameObject CropPrefab;
    //����� �̸� ������ ������ ����Ʈ �ڷ���
    public List<GameObject> CropPool = new List<GameObject>();
    //��� �߻���ų �ֱ�
    public float SpwanTime = 1.0f;
    //��� �ִ� �߻� ����
    public int maxCrop = 12;
    //���� ���� ����
    int cropIdx = 0;
    //���� ���� ���� ����
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
        //����� ������ ������Ʈ Ǯ�� ����
        for (int i = 0; i < maxCrop; i++)
        {
            //��� �������� ����
            GameObject Crop = (GameObject)Instantiate(CropPrefab);
            Crop.GetComponent<SpriteRenderer>().sprite = sprites[cropIdx];

            //������ ����� ��Ȱ��ȭ
            Crop.SetActive(false);
            //������  ����� ������Ʈ Ǯ�� �߰�
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
            //��� ���� �ڷ�ƾ �Լ� ȣ��
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

        //������ �������� �ڷ�ƾ ����
        if (isGameOver) yield break;

        //������Ʈ Ǯ�� ó������ ������ ��ȸ
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
