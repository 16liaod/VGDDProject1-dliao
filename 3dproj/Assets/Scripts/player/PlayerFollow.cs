using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    #region editor variables
    [SerializeField]
    [Tooltip("playerfollow")]
    private Transform m_playertransform;

    [SerializeField]
    [Tooltip("player offset for camera")]
    private Vector3 m_offset;

    [SerializeField]
    [Tooltip("how fast camera rotate around char")]
    private float m_rotatespeed = 10;
    #endregion

    #region mainupdate
    private void LateUpdate()
    {
        Vector3 newpos = m_offset + m_playertransform.position;

        transform.position = Vector3.Slerp(transform.position, newpos, 1);

        float rotateamount = m_rotatespeed * Input.GetAxis("Mouse X");
        transform.RotateAround(m_playertransform.position, Vector3.up, rotateamount);

        m_offset = transform.position - m_playertransform.position;
    }
    #endregion
}
