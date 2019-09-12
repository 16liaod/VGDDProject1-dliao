using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerAttackInfo
{
    #region editer var
    [SerializeField]
    [Tooltip("name for the attack")]
    private string m_name;
    public string Attackname
    {
        get
        {
            return m_name;
        }
    }

    [SerializeField]
    [Tooltip("button for the attack")]
    private string m_button;
    public string Button
    {
        get
        {
            return m_button;

        }
    }

    [SerializeField]
    [Tooltip("trigger string to use to activate this attack in the animator")]
    private string m_triggername;
    public string Triggername
    {
        get
        {
            return m_triggername;
        }
    }

    [SerializeField]
    [Tooltip("The prefab game object for abil")]
    private GameObject m_abilityGO;
    public GameObject abilityGo
    {
        get
        {
            return m_abilityGO;
        }
    }

    [SerializeField]
    [Tooltip("where the attack pops up in relation to player")]
    private Vector3 m_offset;
    public Vector3 offset
    {
        get
        {
            return m_offset;
        }
    }

    [SerializeField]
    [Tooltip("windup time")]
    private float m_winduptime;
    public float winduptime
    {
        get
        {
            return m_winduptime;
        }
    }

    [SerializeField]
    [Tooltip("channel time")]
    private float m_frozentime;
    public float frozentime
    {
        get
        {
            return m_frozentime;
        }
    }

    [SerializeField]
    [Tooltip("cooldown time")]
    private float m_cooldown;

    [SerializeField]
    [Tooltip("hp cost")]
    private int m_hpcost;
    public int hpcost
    {
        get
        {
            return m_hpcost;
        }
    }

    [SerializeField]
    [Tooltip("color of abil")]
    private Color m_color;
    public Color abilitycolor
    {
        get
        {
            return m_color;
        }
    }
    #endregion

    #region Public Variables
    public float cooldown
    {
        get;
        set;
    }
    #endregion

    #region Cooldown Methods
    public void ResetCooldown()
    {
        cooldown = m_cooldown;
    }

    public bool Isready()
    {
        return cooldown <= 0;
    }
    #endregion
}
