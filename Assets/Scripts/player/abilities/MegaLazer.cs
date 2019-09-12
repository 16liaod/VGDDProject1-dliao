using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaLazer : Ability
{
    public override void Use(Vector3 spawnpos)
    {
        RaycastHit[] hits = Physics.SphereCastAll(spawnpos, 1f, transform.forward, m_info.Range);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().DecreaseHealth(m_info.Power);
            }
        }
        var emittershape = cc_ps.shape;
        emittershape.length = m_info.Range;
        cc_ps.Play();
    }
}
