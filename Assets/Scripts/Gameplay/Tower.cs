using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    protected float range = 3;
    [SerializeField]
    protected LayerMask mask;
    [SerializeField]
    protected TargetPriority priority = TargetPriority.Closer;

    protected enum TargetPriority { LowerHealth , Healthier , Stronger , Weaker , Closer , Farther , Faster , Slower}


    protected void FindTarget ( float range ) {
        Collider2D[] colliderList = Physics2D.OverlapCircleAll (transform.position , range , mask);
        Creep target = new Creep ();
        for (int i = 0 ; i < colliderList.Length ; i++) {
            Creep newTarget = colliderList[i].GetComponent<Creep> ();
            if (newTarget == null)
                continue;

            switch (priority) {
                case TargetPriority.LowerHealth:
                    if (newTarget.HealthPercent () < target.HealthPercent ()) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Healthier:
                    if (newTarget.HealthPercent () > target.HealthPercent ()) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Stronger:
                    if (newTarget.health.max > target.health.max) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Weaker:
                    if (newTarget.health.max < target.health.max) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Closer:
                    if (Vector3.Distance(newTarget.transform.position, this.transform.position) < Vector3.Distance (target.transform.position , this.transform.position)) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Farther:
                    if (Vector3.Distance (newTarget.transform.position , this.transform.position) > Vector3.Distance (target.transform.position , this.transform.position)) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Faster:
                    if (newTarget.GetCurrentSpeed() > target.GetCurrentSpeed ()) {
                        target = newTarget;
                    }
                    break;

                case TargetPriority.Slower:
                    if (newTarget.GetCurrentSpeed () < target.GetCurrentSpeed ()) {
                        target = newTarget;
                    }
                    break;

                default:
                    priority = TargetPriority.LowerHealth;
                    break;
            }
        }
    }
}
