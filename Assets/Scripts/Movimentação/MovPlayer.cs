using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovPlayer : MonoBehaviour
{
    public float velocidade;
    public Animator anim;
    public SpriteRenderer player;
    private Vector2 move;
    private bool olhandoDir = true;


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
        Flip();
        anim.SetFloat("Move", move.x);
        anim.SetFloat("MoveY", move.y);
    }

   

    public void moverPlayer()
    {
        Vector3 movment = new Vector3(move.x, 0f, move.y);
        transform.Translate(movment * velocidade * Time.deltaTime, Space.World);
    }

    public void Flip()
    {
        if (Input.GetKeyDown("d"))
        {
            player.flipX;
        }
    }
}
