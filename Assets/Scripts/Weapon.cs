using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    const int colliderBuffer = 32;
    
    public float detectionArea;
    public Transform holder;
    public float aimSpeed = 0.5f;
    public Transform spriteTransform;
    
    private Transform target = null;
    private Collider2D[] inSight = new Collider2D[colliderBuffer];
    private Vector3 spriteScale;
    private Vector3 altScale;
    private Quaternion defaultRotation;

    private GameManager gameManager;

    void Start(){
        gameManager = GameManager.Instance;
        
        defaultRotation = holder.transform.rotation;
        spriteScale = spriteTransform.localScale;
        altScale = new Vector3(spriteScale.x, spriteScale.y * -1, spriteScale.z);
    }

    void FixedUpdate(){
        float collQt = Physics2D.OverlapCircleNonAlloc(
            transform.position, 
            detectionArea, 
            inSight,
            LayerMask.GetMask("Targets")
        );

        if( collQt > 0){
            float smallest = float.MaxValue;
            
            for (int i = 0; i < collQt; i++){
                if(inSight[i].gameObject.TryGetComponent(out CharacterStats characterStats)){
                    float dist = (transform.position - inSight[i].transform.position).sqrMagnitude;
                    if(dist < smallest){
                        smallest = dist;
                        target = inSight[i].transform;
                    }
                }
            }
        }
        else{
            target = null;
        }
    }

    void Update(){
        if(target != null){
            holder.right = target.position - transform.position;

            //flipping sprite
            float rotZ = holder.rotation.eulerAngles.z;
            if (rotZ > 90 && rotZ < 270){
                spriteTransform.localScale = altScale;
            }else{
                spriteTransform.localScale = spriteScale;
            }
        }
        else{
            holder.rotation = defaultRotation;
            spriteTransform.localScale = spriteScale;
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        //reachable area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionArea);
    }
    #endif

}
