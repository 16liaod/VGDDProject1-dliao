using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    #region editorvar
    [SerializeField]
    [Tooltip("text component storing hs")]
    private Text m_highscores;
    #endregion
    #region privatevar
    private string m_defaulthstext;
    #endregion
    #region init
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        m_defaulthstext = m_highscores.text;
    }

    private void Start()
    {
        Updatehs();
    }
    #endregion

    #region Playbuttonmethods
    public void Playarena()
    {
        SceneManager.LoadScene("Arena");
    }
    #endregion

    #region otherbuttonmethod
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
    #region highscoremeth
    private void Updatehs()
    {
        if (PlayerPrefs.HasKey("HS"))
        {
            m_highscores.text = m_defaulthstext.Replace("%s", PlayerPrefs.GetInt("HS").ToString());
        }
        else
        {
            PlayerPrefs.SetInt("HS", 0);
            m_highscores.text = m_defaulthstext.Replace("%s", "0");
        }
    }

    public void Reseths()
    {
        PlayerPrefs.SetInt("HS", 0);
        Updatehs();
    }
    #endregion
}
