using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [Space]
    public float invencibleTime = 0;

    private float lastTime;

    protected override void Start (){
        base.Start();
        lastTime = Time.time;
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        //
    }

    public override void Hitted(int amount){
        if(Time.time - lastTime > invencibleTime){
            AddHealth(-amount);
            lastTime = Time.time;
        }
    }

    
}
