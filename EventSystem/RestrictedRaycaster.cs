using UnityEngine;
using System.Collections.Generic;

public class RestrictedRaycaster : SpriteRaycaster
{
    public List<GameObject> HittableObjects;
    public bool RestrictionEnabled = true;

    protected RestrictedRaycaster() {
        HittableObjects = new List<GameObject>();
    }
 
    protected override void PreprocessHits(ref RaycastHit2D[] hits) {
        base.PreprocessHits(ref hits);

        if(!RestrictionEnabled) return;

        if (HittableObjects == null || HittableObjects.Count == 0) {
            hits = null;
            return;
        }

        if (hits.Length > 0) {
            foreach (var ho in HittableObjects) {
                var hitTransform = hits[0].collider.transform;

                if(hitTransform.Equals(ho.transform) || (hitTransform.IsChildOf(ho.transform) && !hitTransform.CompareTag("block"))) return; 
            }

            hits = null;
        }
    }
}
