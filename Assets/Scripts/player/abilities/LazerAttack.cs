using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : Ability
{
    public override void Use(Vector3 spawnpos)
    {
        RaycastHit hit;
        float newLength = m_info.Range;
        if (Physics.SphereCast(spawnpos, 0.5f, transform.forward, out hit, m_info.Range))
        {
            newLength = (hit.point - spawnpos).magnitude;
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().DecreaseHealth(m_info.Power);
            }
        }
        var emittershape = cc_ps.shape;
        emittershape.length = newLength;
        cc_ps.Play();
    }
}
