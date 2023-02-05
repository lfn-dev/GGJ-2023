using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [Space]
    public float invencibleTime = 0;

    private float lastTime;

    public int filhos;
    //public GameObject[] ltsFilhos;

    protected override void Start ()
    {

        //ltsFilhos = new List<GameObject>();
        base.Start();
        lastTime = Time.time;
    }

    public override void Die()
    {
        if (filhos <= 0) 
        {
            gameObject.SetActive(false);
        //
        } 
    }
    public override void Hitted(int damage){
        if(Time.time - lastTime > invencibleTime){
            AddHealth(-damage);
            lastTime = Time.time;
        }
    }

    
}
