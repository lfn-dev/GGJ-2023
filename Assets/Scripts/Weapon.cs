using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    const int colliderBuffer = 32;
    
    public float detectionArea;
    public Transform holder;
    public Transform spriteTransform;
    
    
    private Transform target = null;
    private Collider[] inSight = new Collider[colliderBuffer];
    private Vector3 spriteScale;
    private Vector3 altScale;
    private Quaternion defaultRotation;

    private GameManager gameManager;
    private Transform player;

    void Start(){
        gameManager = GameManager.Instance;
        
        defaultRotation = holder.transform.rotation;
        spriteScale = spriteTransform.localScale;
        altScale = new Vector3(spriteScale.x * -1, spriteScale.y, spriteScale.z);
        player = gameManager.GetPlayer().transform;
    }

    void FixedUpdate(){
        float collQt = Physics.OverlapSphereNonAlloc(
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

    void LateUpdate(){
        if(target != null){

            if(transform.position.x < target.position.x){
                if(player.localScale.x > 0){
                    holder.rotation = defaultRotation;
                    //spriteTransform.localScale = spriteScale;
                }
                else{
                    holder.eulerAngles = defaultRotation.eulerAngles + new Vector3(0,180,0);
                    //spriteTransform.localScale = altScale;
                }
            }
            else{
                if(player.localScale.x < 0){
                    holder.rotation = defaultRotation;
                    //spriteTransform.localScale = spriteScale;
                }
                else{
                    holder.eulerAngles = defaultRotation.eulerAngles + new Vector3(0,180,0);
                    //spriteTransform.localScale = altScale;
                }
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
