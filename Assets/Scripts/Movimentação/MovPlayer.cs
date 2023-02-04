using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovPlayer : MonoBehaviour
{
    public float velocidade;
    public Animator anim;
    public GameObject player;
    private Vector2 move;
    public bool olhandoDir;

    void Start()
    {
        olhandoDir = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        moverPlayer();
        Flip();
          
        anim.SetFloat("MoveX", move.x);
        anim.SetFloat("MoveY", move.y);
    }

   

    public void moverPlayer()
    {
        Vector3 movment = new Vector3(move.x, 0f, move.y);
        transform.Translate(movment * velocidade * Time.deltaTime, Space.World);
    }

    public void Flip()
    {
        if (move.x > 0)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        if (move.x < 0)
        {
            gameObject.transform.localScale = new Vector3(0.5f , 0.5f , -0.5f);
        }

    }
}
