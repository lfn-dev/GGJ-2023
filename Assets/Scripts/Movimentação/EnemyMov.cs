using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    GameManager gameManager;
    Transform player;

    private float velocity;

    void Start(){
        gameManager = GameManager.Instance;
        player = gameManager.GetPlayer().transform;
        
        if(TryGetComponent(out CharacterStats characterStats)){
            velocity = characterStats.velocity.GetValue();
        }
    }

    void Update(){
        Vector3 newPos =  Vector3.MoveTowards(transform.position,player.position,velocity * Time.deltaTime);
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
