using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private List<SpriteRenderer> entities = new List<SpriteRenderer>();
    private readonly Comparer<SpriteRenderer> comparer = new ZIndexComparer();

    private void Update()
    {
        AdjustLayersByZIndex();
    }

    public void RegisterEntity(SpriteRenderer entity)
    {
        entities.Add(entity);
    }

    private void AdjustLayersByZIndex()
    {
        entities.Sort(comparer);
        var index = 0;
        entities.ForEach(entity =>
        {
            entity.sortingOrder = index++;
        });
    }

    private class ZIndexComparer : Comparer<SpriteRenderer>
    {
        public override int Compare(SpriteRenderer s1, SpriteRenderer s2)
        {
            var x = s1.transform;
            var y = s2.transform;
            if (x.position.y > y.position.y)
            {
                return -1;
            }
            else if (x.position.y < y.position.y)
            {
                return 1;
            }

            if (x.position.x > y.position.x)
            {
                return -1;
            }
            else if (x.position.x < y.position.x)
            {
                return 1;
            }

            if (s1.sortingOrder > s2.sortingOrder)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
