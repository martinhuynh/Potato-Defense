using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    [SerializeField]
    public TileType state;

    [SerializeField]
    public int hp;

    [SerializeField]
    public TileBase[] tiles;

    [SerializeField]
    public bool destroyable;

}
