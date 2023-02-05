using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat health;//how much damage the character can receive before die
    public Stat damage;//damage deal
    public Stat velocity;//movement speed
    public string ignoreTag = "self";

    protected virtual void Start(){
        health.ResetValue();
        damage.ResetValue();
        velocity.ResetValue();

        if(ignoreTag == "self"){
            ignoreTag = transform.tag;
        }
    }

    public virtual void AddHealth(int amount){

        health.AddValue(amount);
        
        if(health.GetValue() <= health.GetMin()){
            Die();
        }
    }

    public virtual void Die(){
        //this method will be overwritten
        Destroy(gameObject);
    }

    public virtual void Hitted (int damage){
        //Receber dano
        AddHealth(-damage);
    }

    void OnTriggerEnter(Collider col){
        if(!col.transform.CompareTag(ignoreTag)){
            if(col.gameObject.TryGetComponent(out CharacterStats characterStats)){
                Hitted(characterStats.damage.GetValue());
            }
        }
    }
}
