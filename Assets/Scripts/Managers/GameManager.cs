using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :  Singleton<GameManager>
{
    private GameObject player = null;
    public GameObject GetPlayer(){
        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }
        return player;
    }
}
