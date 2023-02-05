using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float disapearTime;

    public bool orbit;

    [SerializeField]
    private Vector3 moveDirection;
    
    [SerializeField]
    private float velocity;

    void Start(){
        if(TryGetComponent(out CharacterStats characterStats)){
            velocity = characterStats.velocity.GetValue();
        }
        
        Destroy(gameObject,disapearTime);
    }

    public void SetDirection(Vector3 dir){
        moveDirection = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(!orbit){
            transform.Rotate(new Vector3(velocity,0,0));
        }
        else{
            transform.Translate(moveDirection * velocity * Time.deltaTime, Space.World);
        }
    }
}
