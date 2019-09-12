using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    public static ScoreManagement singleton;
    #region privatevar
    private int m_Curscore;
    #endregion
    #region init
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }else if (singleton != this)
        {
            Destroy(gameObject);
        }
        m_Curscore = 0;
    }
    #endregion
    #region score meth
    public void Increasescore(int amount)
    {
        m_Curscore += amount;
    }

    private void updatehighscore()
    {
        if (!PlayerPrefs.HasKey("HS"))
        {
            PlayerPrefs.SetInt("HS", m_Curscore);
        }

        int hs = PlayerPrefs.GetInt("HS");
        if(hs < m_Curscore)
        {
            PlayerPrefs.SetInt("HS", m_Curscore);
        }
    }
    #endregion

    #region destroy
    private void OnDisable()
    {
        updatehighscore();
    }
    #endregion
}
