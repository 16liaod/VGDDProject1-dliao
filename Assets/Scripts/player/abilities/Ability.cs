using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    #region editor variable
    [SerializeField]
    [Tooltip("ability info datastructure")]
    protected AbilityInfo m_info;
    #endregion

    #region Cached Components
    protected ParticleSystem cc_ps;
    #endregion

    #region init
    private void Awake()
    {
        cc_ps = GetComponent<ParticleSystem>();
    }
    #endregion

    #region usemethods
    public abstract void Use(Vector3 spawnpos);
    #endregion
}
