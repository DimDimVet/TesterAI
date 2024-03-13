using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private MoveAI moveAI;
    void Start()
    {
        moveAI = new MoveAI(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
