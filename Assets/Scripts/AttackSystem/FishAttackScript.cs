using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AttackSystem
{
    class FishAttackScript : AttackScript
    {
        public override void Start()
        {
            base.Start();

            Attack rightA = new Attack("rightA", attackColliders[0], 0.1f, Vector3.right, 10);
            Attack leftA = new Attack("leftA", attackColliders[1], 0.1f, Vector3.left, 10);
            Attack upA = new Attack("upA", attackColliders[2], 0.1f, Vector3.up, 10);
            Attack downA = new Attack("downA", attackColliders[3], 0.1f, Vector3.down, 10);
            Attack neutralA = new Attack("neutralA", attackColliders[4], 0.1f, Vector3.up + Vector3.right, 10);
            attackDictionary.Add(rightA.AttackName, rightA);
            attackDictionary.Add(leftA.AttackName, leftA);
            attackDictionary.Add(upA.AttackName, upA);
            attackDictionary.Add(downA.AttackName, downA);
            attackDictionary.Add(neutralA.AttackName, neutralA);

            Attack rightB = new Attack("rightB", attackColliders[5], 0.1f, Vector3.left, 10);
            Attack leftB = new Attack("leftB", attackColliders[6], 0.1f, Vector3.right, 10);
            Attack upB = new Attack("upB", attackColliders[7], 0.1f, Vector3.down, 10);
            Attack downB = new Attack("downB", attackColliders[8], 0.1f, Vector3.up, 10);
            Attack neutralB = new Attack("neutralB", attackColliders[9], 0.1f, Vector3.up + Vector3.left, 10);
            attackDictionary.Add(rightB.AttackName, rightB);
            attackDictionary.Add(leftB.AttackName, leftB);
            attackDictionary.Add(upB.AttackName, upB);
            attackDictionary.Add(downB.AttackName, downB);
            attackDictionary.Add(neutralB.AttackName, neutralB);
        }
    }
}
