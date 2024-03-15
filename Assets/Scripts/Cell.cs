using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }
    public Cake cake { get; set; }
    public GiftBox giftbox { get; set; }
    public Candy candy { get; set; }

    public bool empty => cake && giftbox == null;
    public bool occupied => cake || giftbox || candy != null;
    public bool occupiedByCake => cake != null;
    public bool occupiedByBox => giftbox != null;
}
