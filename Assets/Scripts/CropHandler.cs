using Spine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.EventSystems;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class CropHandler : MonoBehaviour
{


    PlayerController playerController;
    CropGenerator cropGenerator;

    int totalCropTypes = 5;
    //tell is crop exists in this object
    bool isCropExists = false;
    //spawntime of this crop
    int spawnTime = 0;
    //index of current crop in this tile
    [SerializeField] int cropIdx = 0;
    //Property for cropIdx
    public int CropIdx
    {
        get { return cropIdx; }
        set { cropIdx = value; }
    }

    //Spawn Time of each Crop Type
    int[,] cropSpawnTime = new int[2, 5] { { 1, 1, 1, 2, 3 }, { 1, 2, 3, 4, 7 } };

    public static CropHandler Inst = null;
    Coroutine spawnCoroutine;

    void Awake()
    {
        Inst = this;
        playerController = FindObjectOfType<PlayerController>();
        cropGenerator = FindObjectOfType<CropGenerator>();
    }
    //when tile triggered with player
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (gameObject.activeSelf)
            {
                if (playerController.checkIsFull() == false)
                {
                    Sound_Mgr.Instance.PlayEffSound("6. oat_get", 1.0f);
                    //Debug.Log("Index : " + cropIdx + " NUM : " + playerController.cropNum[cropIdx]);
                    playerController.increaseCropNum(cropIdx);
                    //Debug.Log("Index : " + cropIdx + " NUM : " + playerController.cropNum[cropIdx]);
                    //SetNextCrop();
                    CropDie();

                }
            }
        }
    }
    void SetNextCrop()
    {
        //������ �ڶ� Crop ����
        cropIdx = Random.Range(0, totalCropTypes);
        //������ �ڶ� Crop�� Spawn Time ����
        int spawnMinTime = cropSpawnTime[0, cropIdx];
        int spawnMaxTime = cropSpawnTime[1, cropIdx];
        spawnTime = Random.Range(spawnMinTime, spawnMaxTime + 1);
        spawnCrop();
    }
    //Spawn Coroutine ����
    public void spawnCrop()
    {
        spawnCoroutine = StartCoroutine(WaitAndLoad(spawnTime));
    }
    IEnumerator WaitAndLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        isCropExists = true;
    }

    public void CropDie()
    {
        /*if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }*/
        //������Ʈ Ǯ�� ȯ����Ű�� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(this.PushObjectPool());
    }//void CropDie()

    //��� ĳ��
    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(1.0f);

        //��︦ ��Ȱ��ȭ 
        gameObject.SetActive(false);
    }

}
