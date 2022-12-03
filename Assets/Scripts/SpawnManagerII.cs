using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerII : MonoBehaviour
{
    public PieceType type;
    private Piece currentPiece;

    public void Spawn()
    {
        currentPiece = LevelManager.Instance.GetPiece(type, 0); //get a new piece
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}
