using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Clear_Mgr : MonoBehaviour
{
    public Button TitleGo_Btn = null;
    public Text BestScore = null;
    public Text CurScore = null;
    int m_BestScore = 0;
    int m_CurScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        Sound_Mgr.Instance.PlayBGM("9. end scene bgm");
        m_BestScore = PlayerPrefs.GetInt("BestScore");
        m_CurScore = PlayerPrefs.GetInt("CurScore");
        isTotalScoreBigger();
        if (TitleGo_Btn != null)
            TitleGo_Btn.onClick.AddListener(() => {
                SceneManager.LoadScene("TitleScene");
            });
        BestScore.text = m_BestScore.ToString();
        CurScore.text = m_CurScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void isTotalScoreBigger()
    {
        if (m_BestScore <= m_CurScore)
        {
            m_BestScore = m_CurScore;
        }
    }
}
