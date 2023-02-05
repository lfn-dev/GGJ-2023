using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [Space]
    public float invencibleTime = 0;

    private float lastTime;

    public int filhos;

    protected override void Start (){
        base.Start();
        lastTime = Time.time;
    }

    public override void AddHealth(int amount){
        if(amount < 0){
            //tomou dano
            //anim.SetTrigger("Hitted");
        }
        base.AddHealth(amount);
    }

    public override void Die()
    {
        if(filhos >= 0)
        {
            //player morreu
        }
        else
        {
            filhos - 1;
        }
    }

    public override void Hitted(int damage){
        if(Time.time - lastTime > invencibleTime){

            AddHealth(-damage);

            lastTime = Time.time;
        }
    }
}
