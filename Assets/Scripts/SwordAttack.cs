using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] GameObject SwordHitBox;
    [SerializeField] GameObject SwordHitBoxRev;
    private PolygonCollider2D SwordCollider;
    private PolygonCollider2D SwordColliderRev;

    private void Start()
    {
        SwordCollider = SwordHitBox.GetComponent<PolygonCollider2D>();
        SwordColliderRev = SwordHitBoxRev.GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (SwordCollider.enabled)
        {
            var contacts = new Collider2D[32];
            this.SwordCollider.GetContacts(contacts);
            foreach (var col in contacts)
            {

                if (col != null && col.gameObject != null && col.gameObject.tag == "skeleton")
                {
                    var doer = col.gameObject.GetComponent<SkeletonController>();
                    doer.SkeletonHit(this.gameObject.GetComponent<HeroController>().weapon);
                    doer.SkeletonKnock(this.gameObject);
                }
            }
        }

        if (SwordColliderRev.enabled)
        {
            var contacts = new Collider2D[32];
            this.SwordColliderRev.GetContacts(contacts);
            foreach (var col in contacts)
            {

                if (col != null && col.gameObject != null && col.gameObject.tag == "skeleton")
                {
                    var doer = col.gameObject.GetComponent<SkeletonController>();
                    doer.SkeletonHit(this.gameObject.GetComponent<HeroController>().weapon);
                    doer.SkeletonKnock(this.gameObject);
                }
            }
        }
        SwordCollider.enabled = false;
        SwordColliderRev.enabled = false;
    }

    public void Enable()
    {
        SwordCollider.enabled = true;
    }

    public void EnableRev()
    {
        SwordColliderRev.enabled = true;
    }
}
