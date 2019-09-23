using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region editorvars
    [SerializeField]
    [Tooltip("health int")]
    private int m_maxhealth;

    [SerializeField]
    [Tooltip("movespeed")]
    private float m_speed;

    [SerializeField]
    [Tooltip("dpf")]
    private float m_dmg;

    [SerializeField]
    [Tooltip("ondeatheffect")]
    private ParticleSystem m_deatheffect;

    [SerializeField]
    [Tooltip("drop rate of heal")]
    private float m_droprate;

    [SerializeField]
    [Tooltip("type of pill")]
    private GameObject m_healthpill;

    [SerializeField]
    [Tooltip("points for poof")]
    private int m_score;
    #endregion

    #region privatevar
    private float p_curhealth;
    #endregion

    #region cached components
    private Rigidbody cc_rb;
    #endregion

    #region cached references
    private Transform cr_player;
    #endregion

    #region init
    private void Awake()
    {
        p_curhealth = m_maxhealth;

        cc_rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cr_player = FindObjectOfType<PlayerController>().transform;
    }
    #endregion

    #region main updates
    private void FixedUpdate()
    {
        Vector3 dir = cr_player.position - transform.position;
        dir.Normalize();
        cc_rb.MovePosition(cc_rb.position + dir * m_speed * Time.fixedDeltaTime);
    }
    #endregion

    #region collision methods
    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().decreasehealth(m_dmg);
        }
    }
    #endregion

    #region health methods
    public void DecreaseHealth(float amount)
    {
        p_curhealth -= amount;
        if (p_curhealth <= 0)
        {
            ScoreManagement.singleton.Increasescore(m_score);
            if (Random.value < m_droprate)
            {
                Instantiate(m_healthpill, transform.position, Quaternion.identity);
            }
            Instantiate(m_deatheffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    #endregion
}
