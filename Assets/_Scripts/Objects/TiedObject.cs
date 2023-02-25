using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiedObject : MonoBehaviour
{
    private TiedObjectManager tiedManager;

    private void Start() => tiedManager = GetComponentInParent<TiedObjectManager>();

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        tiedManager.TouchingPlayer(_collision);
    }

}
