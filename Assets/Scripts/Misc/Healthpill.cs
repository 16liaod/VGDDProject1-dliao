using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpill : MonoBehaviour
{
    #region editorvar
    [SerializeField]
    [Tooltip("how much hp heal")]
    private int m_healamt;
    public int healamt
    {
        get
        {
            return m_healamt;
        }
    }
    #endregion
}
