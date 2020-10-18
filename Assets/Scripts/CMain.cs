using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMain : MonoBehaviour
{
    private bool game_started = false;

    public GameObject PlayerPref;
    public GameObject TilePref;
    public GameObject CrystalPref;

    public GameObject MainCamera;

    private GameObject goTILES = null;
    private GameObject GOplayer = null;
    private static float multSPEED = 2.0f;
    private float SpeedTile = 100.0f * multSPEED;
    private float SpeedPlayer = 1.0f * multSPEED;

    private Vector2Int tecTileCoord = new Vector2Int(0, 0);
    private float tileY = 0;
    //private List<GameObject> Ltiles = new List<GameObject>();
    private float crystalY = 1;

    // Start is called before the first frame update
    //void Start()
    //{        
    //
    //}

    private float tickMax = 100.0f;
    [SerializeField]
    private float tickTec = 0.0f;

    private bool directPlayer = true;

    // Update is called once per frame
    void Update()
    {
        if (game_started == false)
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                game_started = true;
                Finit();
            }

        if (game_started == true)
        {

            tickTec -= SpeedTile * Time.deltaTime;

            if (tickTec <= 0)
            {
                tickTec = tickMax;
                FgenNextTile();
            }

            Vector3 v3 = GOplayer.transform.position;
            MainCamera.transform.position = new Vector3(v3.x,MainCamera.transform.position.y,v3.z);
        }//if (game_started)
    }//void Update()

    ///////////////////////////////////////
    //private float destroyTime = 10.0f;

    private byte iblock = 0;
    private byte jblock = 0;
    private static byte blockMax = 5;
    private bool[,] Mbonus = new bool[blockMax,blockMax]; 

    private void FMbonusInit()
    {
        iblock = 0;
        jblock = 0;

        for (byte i = 0; i < blockMax; i++)
            for (byte j = 0; j < blockMax; j++)
                if (i==j)
                    Mbonus[i,j] = true;

        /*
        string str = "";
        for (byte i = 0; i < blockMax; i++)
        {
            str += "[";
            for (byte j = 0; j < blockMax; j++)
            {
                if (Mbonus[i,j]) { str += "1"; }
                if (!Mbonus[i, j]) { str += "0"; }
            }
            str += "]";
        }
        Debug.Log(str);
        */
    }

    private bool FsetBonus()
    {
        bool res = false;
        //Debug.Log(Mbonus[iblock, jblock].ToString() + " " + iblock.ToString() + " " + jblock.ToString());
        res = Mbonus[iblock, jblock];
        jblock++;                
        if (jblock == blockMax) { jblock = 0; iblock++; }
        if (iblock == blockMax) { iblock = 0; }

        //if (Random.Range(0,5) == 0) { return true; }
        return res;
    }

    private void FgenCrystal()
    {
        GameObject goCrystal = Instantiate(CrystalPref);
        goCrystal.name = "Crystal";
        goCrystal.transform.position = new Vector3(tecTileCoord.x, crystalY, tecTileCoord.y);
        goCrystal.transform.SetParent(goTILES.transform);
    }

    private void FgenNextTile()
    {
        if (Random.Range(0, 2) > 0) { tecTileCoord = new Vector2Int(tecTileCoord.x + 1, tecTileCoord.y); }
        else { tecTileCoord = new Vector2Int(tecTileCoord.x, tecTileCoord.y + 1); }

        GameObject goTile = Instantiate(TilePref);
        goTile.name = "Tile_" + tecTileCoord.ToString();
        goTile.transform.position = new Vector3(tecTileCoord.x, tileY, tecTileCoord.y);

        //Ltiles.Add(goTile);
        goTile.transform.SetParent(goTILES.transform);

        if (FsetBonus() == true) { FgenCrystal(); }
        //Destroy(goTile, destroyTime);
    }//private void FgenNextTile()

    private void Finit()
    {
        iScore = 0;
        GUIinit();
        FMbonusInit();

        tecTileCoord = new Vector2Int(0, 0);        
        //Ltiles = new List<GameObject>();

        goTILES = new GameObject();
        goTILES.name = "TILES";
        
        FgenPlace();
        
        for (int i = 0; i < 10; i++) { FgenNextTile(); }

        GOplayer = Instantiate(PlayerPref);
        GOplayer.AddComponent<Player>().Finit(SpeedPlayer,gameObject, multSPEED);
    }//private void Finit()

    private Vector3[] Mv3 = { new Vector3(-1,0,-1), new Vector3(-1, 0, 0), new Vector3(-1, 0, 1) , new Vector3(0, 0,-1), new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, -1), new Vector3(1, 0, 0), new Vector3(1, 0, 1) };

    private void FgenPlace()
    {
        for (byte i = 0; i < 9; i++)
        {
            GameObject goPlace = Instantiate(TilePref);
            goPlace.transform.SetParent(goTILES.transform);
            goPlace.transform.position = Mv3[i];
        }
    }

    public void Fgameover()
    {
        game_started = false;
        Destroy(goTILES, 0.5f);
        //foreach (GameObject go in Ltiles) { Destroy(go, 1.0f); }
    }

    private int iScore = 0;
    public void FscoreUp() { iScore++; }

    GUIStyle style = new GUIStyle();

    private void GUIinit()
    {
        style.normal.textColor = Color.yellow;
        style.fontSize = 24;
        style.fontStyle = FontStyle.Bold;
    }

    void OnGUI()
    {
        if (game_started == true) { GUI.Label(new Rect(5, 0, 100, 34), "SCORE " + iScore, style); }
        if (game_started == false) { GUI.Label(new Rect(5, 0, 100, 34), "CLICK TO START", style); }
    }//void OnGUI()

}//public class CMain : MonoBehaviour
