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
        Debug.Log(transform.name + " died.");
    }

    public virtual void Hitted (int damage){
        //Receber dano
        AddHealth(-damage);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(!collision.transform.CompareTag(transform.tag)){
            CharacterStats characterStats = collision.gameObject.GetComponent<CharacterStats>();
            if(characterStats){
                Hitted(characterStats.damage.GetValue());
            }
        }
    }
}
