using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovPlayer : MonoBehaviour
{
    public float velocidade;
    public Animator anim;
    private Vector2 move;


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moverPlayer();
    }

   

    public void moverPlayer()
    {
        Vector3 movment = new Vector3(move.x, 0f, move.y);

        transform.Translate(movment * velocidade * Time.deltaTime, Space.World);
    }
}
