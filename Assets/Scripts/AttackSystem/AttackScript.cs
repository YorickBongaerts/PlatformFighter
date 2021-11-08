using Assets.Scripts.AttackSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    #region Variables and fields

    [SerializeField]
    private Collider[] bodyColliders = new Collider[3];
    [SerializeField]
    public Collider[] attackColliders = new Collider[10];
    
    private BaseMovement baseMovement;
    public Dictionary<string, Attack> attackDictionary = new Dictionary<string, Attack>();
    #endregion Variables and fields

    #region Unity lifecycle

    public virtual void Start()
    {
        baseMovement = gameObject.GetComponent<BaseMovement>();
    }

    #endregion Unity lifecycle

    #region Attack

    void DoAttack(string attackName)
    {
        Attack currentAttack = attackDictionary[attackName];
        Collider col = currentAttack.AttackCollider;
        col.gameObject.SetActive(true);
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("BodyBoxLayer"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent.parent == transform)
                continue;

            Debug.Log(currentAttack.AttackName + c.transform.name + c.transform.parent.parent.name);
            AddPercent(currentAttack.Percent, c.transform.parent.parent.gameObject);
            DoKnockBack(currentAttack.KnockbackDirection, currentAttack.BaseKnockback, c.transform.parent.parent.gameObject);
        }
    }

    #endregion Attack

    #region Input registration

    public void OnAPress()
    {
        DoAttack(baseMovement.state + "A");
        //SmashAttack(directionState.state, 10, 0.1f, new Vector3(1, 1, 0));
    }
    public void OnBPress()
    {
        DoAttack(baseMovement.state + "B");
    }

    #endregion Input registration

    #region Damage

    private void AddPercent(float percentToAdd, GameObject enemy)
    {
        enemy.GetComponent<BaseMovement>().percent += percentToAdd;
    }

    private void DoKnockBack(Vector3 direction, float baseKnockback, GameObject enemy)
    {
        enemy.GetComponent<BaseMovement>().AddImpact(direction, baseKnockback);
        enemy.GetComponent<BaseMovement>().ResetGravity();
        //enemy.GetComponent<Rigidbody>().AddForce(direction * baseKnockback * enemy.GetComponent<FishMovement>().percent);
    }

    #endregion Damage
}
