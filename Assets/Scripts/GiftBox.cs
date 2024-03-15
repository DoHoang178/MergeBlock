using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour
{
    public Cell cell { get; private set; }

    public void Spawn(Cell cell)
    {
        this.cell = cell;
        this.cell.giftbox = this;
        transform.position = cell.transform.position;
    }
    public void MoveTo(Cell cell)
    {
        if (this.cell != null)
        {
            this.cell.giftbox = null;
        }
        this.cell = cell;
        this.cell.giftbox = this;
        StartCoroutine(Animate(cell.transform.position));
    }

    private IEnumerator Animate(Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;
    }
}
