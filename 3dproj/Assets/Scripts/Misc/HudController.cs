using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    #region editorvar
    [SerializeField]
    [Tooltip("the part of the health that dec")]
    private RectTransform m_healthbar;

    [SerializeField]
    [Tooltip("attack cooldown megalazer")]
    private RectTransform m_cdbar;
    #endregion

    #region privatevar
    private float p_healthbarorigwidth;
    private float p_cdbarorigwidth;
    #endregion

    #region init
    private void Awake()
    {
        p_healthbarorigwidth = m_healthbar.sizeDelta.x;
        p_cdbarorigwidth = m_cdbar.sizeDelta.x;
    }
    #endregion

    #region Update Health Bar
    public void UpdateHealth(float percent)
    {
        m_healthbar.sizeDelta = new Vector2(p_healthbarorigwidth * percent, m_healthbar.sizeDelta.y);
    }
    public void UpdateCD(float percent)
    {
        m_cdbar.sizeDelta = new Vector2(p_cdbarorigwidth * percent, m_healthbar.sizeDelta.y);
    }
    #endregion
}
