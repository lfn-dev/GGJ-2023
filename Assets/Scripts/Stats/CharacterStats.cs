using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat health;//how much damage the character can receive before die
    public Stat damage;//damage deal
    public Stat velocity;//movement speed

    protected GameManager gameManager;

    void Awake(){
        gameManager = GameManager.Instance;
    }

    protected virtual void Start(){
        health.ResetValue();
        damage.ResetValue();
        velocity.ResetValue();
    }

    public virtual void AddHealth(int amount){
        Debug.Log(transform.name + " heal by " + amount);

        health.AddValue(amount);
        
        if(health.GetValue() <= health.GetMin()){
            Die();
        }
    }

    public virtual void Die(){
        //this method will be overwritten
        Destroy(gameManager);
    }

    public virtual void Hitted (int damage){
        //Receber dano
        AddHealth(-damage);
    }

    void OnCollisionEnter(Collision col){
        if(!col.transform.CompareTag(transform.tag)){
            if(col.gameObject.TryGetComponent(out CharacterStats characterStats)){
                Hitted(characterStats.damage.GetValue());
            }
        }
    }
}
