using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemySpawn 
{
    #region editorvariables
    [SerializeField]
    [Tooltip("name of enemy")]
    private string m_name;
    public string Enemyname
    {
        get
        {
            return m_name;
        }
    }

    [SerializeField]
    [Tooltip("The prefab of enemy")]
    private GameObject m_enemyGO;
    public GameObject EnemyGO
    {
        get
        {
            return m_enemyGO;
        }
    }

    [SerializeField]
    [Tooltip("seconds before spawn")]
    private float m_timetospawn;
    public float Timetospawn
    {
        get
        {
            return m_timetospawn;
        }
    }

    [SerializeField]
    [Tooltip("number of enemies to spawn. 0 = endless")]
    private int m_numenemies;
    public int Numenemiesspawn
    {
        get
        {
            return m_numenemies;
        }
    }
    #endregion
}
