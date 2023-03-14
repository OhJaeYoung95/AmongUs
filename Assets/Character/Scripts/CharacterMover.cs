using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CharacterMover : NetworkBehaviour
{
    public bool isMoveable;

    [SyncVar]
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
