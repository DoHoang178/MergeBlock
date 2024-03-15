using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    public Cell cell { get; private set; }
    
    public void Spawn(Cell cell)
    {
        this.cell = cell;
        this.cell.cake = this;
        transform.position = cell.transform.position;
    }
    public void MoveTo(Cell cell)
    {
        if (this.cell != null)
        {
            this.cell.cake = null;
        }
        this.cell = cell;
        this.cell.cake = this;
        StartCoroutine(Animate(cell.transform.position, false));
    }

    public void Merge (Cell cell)
    {
        if(this.cell != null)
        {
            this.cell.cake = null;
        }
        this.cell = null;
        StartCoroutine(Animate(cell.transform.position, true));

    }

    private IEnumerator Animate(Vector3 to, bool merging)
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

        if (merging)
        {
            Destroy(gameObject);
        }
    }

}
