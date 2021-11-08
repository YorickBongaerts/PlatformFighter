using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AttackSystem
{
    public struct Attack
    {
        public string AttackName;
        public Collider AttackCollider;
        public float Percent;
        public Vector3 KnockbackDirection;
        public float BaseKnockback;

        public Attack(string attackName, Collider attackCollider, float percent, Vector3 knockbackDirection, float baseKnockback)
        {
            AttackName = attackName;
            AttackCollider = attackCollider;
            Percent = percent;
            KnockbackDirection = knockbackDirection;
            BaseKnockback = baseKnockback;
        }
    }
}
