using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteRaycaster : Physics2DRaycaster
{
    protected SpriteRaycaster() {}

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList) {
        if (eventCamera == null)
            return;

        var ray = eventCamera.ScreenPointToRay(eventData.position);

        var dist = eventCamera.farClipPlane - eventCamera.nearClipPlane;

        var hits = Physics2D.GetRayIntersectionAll(ray, dist, finalEventMask);

        PreprocessHits(ref hits);

        if (hits != null && hits.Length != 0) {

            eventData.worldPosition = hits[0].point;
            eventData.worldNormal = hits[0].normal;
            for (int b = 0, bmax = hits.Length; b < bmax; ++b) {
                var result = new RaycastResult {
                    gameObject = hits[b].collider.gameObject,
                    module = this,
                    distance = 0,
                    index = resultAppendList.Count
                };
                resultAppendList.Add(result);
            }
        }
    }

    protected virtual void PreprocessHits(ref RaycastHit2D[] hits) {
        Array.Sort(hits, SpriteRaycastComparer);        
    }


    private static int SpriteRaycastComparer(RaycastHit2D lhs, RaycastHit2D rhs) {
        var lhsRenderer = lhs.collider.gameObject.GetComponent<SpriteRenderer>();
        var rhsRenderer = rhs.collider.gameObject.GetComponent<SpriteRenderer>();

        if (lhsRenderer == null) return rhsRenderer == null ? 0 : 1;
        if (rhsRenderer == null) return -1;

		var lhsSortingLayerOrder = sortingLayersOrder[lhsRenderer.sortingLayerName];
		var rhsSortingLayerOrder = sortingLayersOrder[rhsRenderer.sortingLayerName];

		if (lhsSortingLayerOrder > rhsSortingLayerOrder) return -1;
		if (lhsSortingLayerOrder < rhsSortingLayerOrder) return 1;

        if (lhsRenderer.sortingOrder > rhsRenderer.sortingOrder) return -1;
        if (lhsRenderer.sortingOrder < rhsRenderer.sortingOrder) return 1;

        return 0;
    }

	static Dictionary<string, int> sortingLayersOrder = new Dictionary<string, int>
	{
		{"Default", 0}
	};
}
