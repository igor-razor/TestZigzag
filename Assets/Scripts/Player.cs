using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject _goMain = null;
    private float _SpeedPlayer = 0.0f;
    //private float _mult = 0.0f;

    public void Finit(float speed, GameObject GOmain, float mult) { _SpeedPlayer = speed; _goMain = GOmain; tickMax = tickMax/mult; }
    private bool directPlayer = true;

    //private bool game_play = true;
    
    // Start is called before the first frame update
    //void Start()
    //{
    //
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { directPlayer = !directPlayer; }

        Vector3 v3 = gameObject.transform.position;

        if (directPlayer == true) { v3.x += _SpeedPlayer * Time.deltaTime; }
        else { v3.z += _SpeedPlayer * Time.deltaTime; }

        gameObject.transform.position = v3;

        tickTec += Time.deltaTime;
        if (tickTec >= tickMax) { gameObject.GetComponent<Rigidbody>().useGravity = true; }

        //if ((flagExitTile==true)&&(flagEnterTile == true)) { gameObject.GetComponent<Rigidbody>().useGravity = true; }
    }//void Update()

    //private GameObject goTile = null;
    
    private float tickMax = 2.0f;
    [SerializeField]
    private float tickTec = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Tile"))
        {
            //goTile = other.gameObject;
            tickTec = 0;
        }
        
        if (other.gameObject.name.Contains("Over"))
        {
            //Debug.Log("GAME OVER");
            _goMain.GetComponent<CMain>().Fgameover();
            Destroy(gameObject, 1.0f);

            gameObject.GetComponent<Player>().enabled = false;
        } 

        if (other.gameObject.name.Contains("Crystal"))
        {
            other.gameObject.GetComponent<Crystal>().Fupper();
            _goMain.GetComponent<CMain>().FscoreUp();
        }
    }//private void OnTriggerEnter(Collider other)

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Tile"))
        {            
            Destroy(other.gameObject, 2.0f);
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //goTile = null;
        }        
    }


}//public class Player : MonoBehaviour
