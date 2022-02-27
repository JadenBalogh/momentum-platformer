using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.TryGetComponent<Player>(out Player player))
        {
            GameManager.EndGame(true);
        }
    }
}
