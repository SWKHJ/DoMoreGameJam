using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static Define;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 9f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }


    Rigidbody Rigidbodyrb;
    GameObject collideObject;

    // crop num currently player holds
    public int[] cropNum = new int[] { 0, 0, 0, 0, 0 };
    bool isFull = false;
    // to store total sum of cropNum
    int cropNumSum = 0;
    //�÷��̾ ���� ������ ��� ���� �ʰ��ߴ��� Ȯ��

    //Meal ���� ����
    bool SaveMeal = false;
    public RawImage Meal_Image = null;
    //Booster ���� ����


    //�ִϸ��̼� ���� ����
    Animator animator;
    //�ִϸ��̼� ���� ����

    public bool StateWork = false;

    public static PlayerController Inst;
    void Awake()
    {
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Sound_Mgr.Instance.PlayBGM("4. game bgm");
        Application.targetFrameRate = 60;
        //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ ����
        SaveMeal = false;
        Rigidbodyrb = GetComponent<Rigidbody>();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputcheck();
        //FlipSprite();
    }
    void inputcheck()
    {
        //�¿� ���� �Լ�
        int key = 0;
        //�¿� ���� �Լ�
        if (Input.GetKey(KeyCode.W))
        {   // y�� 1.6 x�� -7 y�� -4.7 x�� 7
            transform.position += (new Vector3(0, moveSpeed * Time.deltaTime, 0));
            Vector3 pos = transform.position;
            if (pos.y >= 1.6f) pos.y = 1.6f;
            transform.position = pos;
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (new Vector3(0, -moveSpeed * Time.deltaTime, 0));
            Vector3 pos = transform.position;
            if (pos.y <= -4.7f) pos.y = -4.7f;
            transform.position = pos;
            if (transform.position.y >= -4.7f)
                animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            key = -1;
            transform.position += (new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            Vector3 pos = transform.position;
            if (pos.x <= -7.0f) pos.x = -7.0f;
            transform.position = pos;
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            key = 1;
            transform.position += (new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            Vector3 pos = transform.position;
            if (pos.x >= 7.0f) pos.x = 7.0f;
            transform.position = pos;
            animator.SetBool("Walk", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Walk", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sound_Mgr.Instance.PlayEffSound("5. sam_get", 1.0f);
            animator.SetBool("Work", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Work", false);
        }

        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

    }


    void OnTriggerEnter(Collider other)
    {

        collideObject = other.gameObject;

        if (other.tag == "CowMeal")
        {
            if (SaveMeal == true)
                return;
            else
            {
                SaveMeal = true;
                Meal_Image.gameObject.SetActive(SaveMeal);
            }
        }
        if (other.tag == "Cow")
        {
            if (SaveMeal == false)
                return;
            else
            {
                Sound_Mgr.Instance.PlayEffSound("10. cow_howl", 1.0f);
                SaveMeal = false;
                Meal_Image.gameObject.SetActive(SaveMeal);
                CowManger.Inst.EatItem();
            }

        }


    }// void OnTriggerEnter(Collider other)
    public bool checkIsFull()
    {
        for (int i = 0; i < cropNum.Length; i++)
        {
            cropNumSum += cropNum[i];
        }
        if (cropNumSum >= 20)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
        cropNumSum = 0;
        return isFull;
    }//public bool checkIsFull()

    public void ClearCropNum()
    {
        for (int i = 0; i < cropNum.Length; i++)
        {
            cropNum[i] = 0;
        }
    }// public void ClearCropNum()
    public void increaseCropNum(int cropIdx)
    {
        cropNum[cropIdx]++;
    }
}