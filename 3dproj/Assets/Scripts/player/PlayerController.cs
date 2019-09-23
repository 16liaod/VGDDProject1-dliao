using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    #region Editor Variables
    [SerializeField]
    [Tooltip("How fast the player can move")]
    private float m_speed;

    [SerializeField]
    [Tooltip("The transform of the camera following the player")]
    private Transform m_cameratransform;

    [SerializeField]
    [Tooltip("list of attacks and info")]
    private PlayerAttackInfo[] m_attacks;

    [SerializeField]
    [Tooltip("player maxhp")]
    private int m_maxhealth;

    [SerializeField]
    [Tooltip("the hud script")]
    private HudController m_hud;
    #endregion

    #region Cached References
    private Animator cr_Anim;
    private Renderer cr_Renderer;
    #endregion

    #region Cached Components
    private Rigidbody cc_rb;
    #endregion

    #region Private Variables
    // the current movedirection of the player. Does not include magnitude
    private Vector2 p_velocity;

    //cant take action while frz
    private float p_frozentime;

    //default color
    private Color p_defaultcolor;

    //player float hp current
    private float p_curhp;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_velocity = Vector2.zero;
        cc_rb = GetComponent<Rigidbody>();
        cr_Anim = GetComponent<Animator>();
        cr_Renderer = GetComponentInChildren<Renderer>();
        p_defaultcolor = cr_Renderer.material.color;

        p_frozentime = 0;
        p_curhp = m_maxhealth;

        for (int i = 0; i < m_attacks.Length; i++)
        {
            PlayerAttackInfo attack = m_attacks[i];
            attack.cooldown = 0;

            if (attack.winduptime > attack.frozentime)
            {
                Debug.LogError(attack.Attackname + "has a windup time longer than the amount of time that the player is frozen for");
            }
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region MainUpdate
    private void Update()
    {
        if (p_frozentime > 0)
        {
            p_velocity = Vector2.zero;
            p_frozentime -= Time.deltaTime;
            return;
        }
        else
        {
            p_frozentime = 0;
        }

        for (int i = 0; i <m_attacks.Length; i++)
        {
            PlayerAttackInfo attack = m_attacks[i];
            if (attack.Isready())
            {
                if (Input.GetButtonDown(attack.Button)){
                    p_frozentime = attack.frozentime;
                    decreasehealth(attack.hpcost);
                    StartCoroutine(UseAttack(attack));
                    break;
                }
            } else if (attack.cooldown > 0)
            {
                attack.cooldown -= Time.deltaTime;
                if (attack == m_attacks[1])
                {
                    m_hud.UpdateCD(1.0f * attack.cooldown / 2.5f);
                }
            }
        }

        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        //update the Anim
        cr_Anim.SetFloat("Speed", Mathf.Clamp01(Mathf.Abs(forward) + Mathf.Abs(right)));

        //updates velocity
        float moveThreshold = 0.3f;
        if (forward > 0 && forward < moveThreshold)
        {
            forward = 0;
        } else if (forward < 0 && forward > -moveThreshold)
        {
            forward = 0;
        }
        if (right > 0 && right < moveThreshold)
        {
            right = 0;
        } else if (right < 0 && right > -moveThreshold)
        {
            right = 0;
        }
        p_velocity.Set(right, forward);
    }

    private void FixedUpdate()
    {
        //update the current position of player
        cc_rb.MovePosition(cc_rb.position + m_speed * Time.fixedDeltaTime * transform.forward * p_velocity.magnitude);

        //update rotation of the player
        cc_rb.angularVelocity = Vector3.zero;

        if (p_velocity.sqrMagnitude > 0)
        {
            float angletorotcam = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, p_velocity);
            Vector3 camForward = m_cameratransform.forward;
            Vector3 newrot = new Vector3(Mathf.Cos(angletorotcam) * camForward.x - Mathf.Sin(angletorotcam) * camForward.z , 0 ,
                Mathf.Cos(angletorotcam)* camForward.z + Mathf.Sin(angletorotcam) * camForward.x);
            float theta = Vector3.SignedAngle(transform.forward, newrot, Vector3.up);
            cc_rb.rotation = Quaternion.Slerp(cc_rb.rotation, cc_rb.rotation * Quaternion.Euler(0, theta, 0), 0.2f);
        }
    }
    #endregion

    #region health/death
    public void decreasehealth(float amount)
    {
        p_curhp -= amount;
        m_hud.UpdateHealth(1.0f * p_curhp / m_maxhealth);
        if (p_curhp < 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Increasehp(int amount)
    {
        p_curhp += amount;
        if (p_curhp > m_maxhealth)
        {
            p_curhp = m_maxhealth;
        }
        m_hud.UpdateHealth(1.0f * p_curhp / m_maxhealth);
    }
    #endregion

    #region Attack Methods
    private IEnumerator UseAttack(PlayerAttackInfo attack)
    {
        cc_rb.rotation = Quaternion.Euler(0, m_cameratransform.eulerAngles.y, 0);
        cr_Anim.SetTrigger(attack.Triggername);
        IEnumerator toColor = Changecolor(attack.abilitycolor, 10);
        StartCoroutine(toColor);
        yield return new WaitForSeconds(attack.winduptime);

        Vector3 offset = transform.forward * attack.offset.z + transform.right * attack.offset.x + transform.up * attack.offset.y;
        GameObject go = Instantiate(attack.abilityGo, transform.position + offset, cc_rb.rotation);
        go.GetComponent<Ability>().Use(transform.position + offset);

        StopCoroutine(toColor);
        StartCoroutine(Changecolor(p_defaultcolor, 50));
        yield return new WaitForSeconds(attack.cooldown);

        attack.ResetCooldown();
    }
    #endregion

    #region Misc Methods
    private IEnumerator Changecolor(Color newcolor, float speed)
    {
        Color curcolor = cr_Renderer.material.color;
        while(curcolor != newcolor)
        {
            curcolor = Color.Lerp(curcolor, newcolor, speed / 100);
            cr_Renderer.material.color = curcolor;
            yield return null;
        }
    }
    #endregion

    #region collision methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthPill"))
        {
            Increasehp(other.GetComponent<Healthpill>().healamt);
            Destroy(other.gameObject);
        }
    }
    #endregion
}
