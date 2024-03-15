using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public Cell cell { get; private set; }

    public void Spawn(Cell cell)
    {
        this.cell = cell;
        this.cell.candy = this;
        transform.position = cell.transform.position;
    }
}
