using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : Ability
{
    public override void Use(Vector3 spawnpos)
    {
        RaycastHit[] hits = Physics.SphereCastAll(spawnpos, 25f, Vector3.down, 0);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().DecreaseHealth(m_info.Power);
            }
        }
        var emittershape = cc_ps.shape;
        emittershape.length = 50;
        cc_ps.Play();
    }
}
