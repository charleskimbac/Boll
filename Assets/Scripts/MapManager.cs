using UnityEngine;
//=using System.Collections.Generic;

//= means it's not needed anymore
public class MapManager : MonoBehaviour {
    private int random;

    public GameObject startTrack;
    public GameObject straight;
    public GameObject turnR;
    public GameObject turnL;
    private GameObject newTrack;
    private Quaternion rotate; //just make a new one every time
    
    private Vector3 coord;

    private char facing;

    public Player player;

    //=private List<Vector3> allCoords = new List<Vector3>();



    // Start is called before the first frame update
    void Start(){
        coord = GameObject.Find("start").transform.position;
        facing = 'N';
        //=allCoords.Add(coord);

        //  V testing V
        //random = 0;
        //Instantiate(straight, coord + new Vector3(0,0,20), rotate);
    }

    public void resetMap(){
        GameObject[] tracks = GameObject.FindGameObjectsWithTag("track");
        foreach(GameObject temp in tracks) {
            GameObject.Destroy(temp);
        }

        player.transform.position = new Vector3(0, .8f, 0);
        Instantiate(startTrack, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        coord = new Vector3(0, 0, 0);
        facing = 'N';
    }

    private void calcFacing(string temp){
        if (temp.Equals("right")){
            if (facing == 'N'){
                facing = 'E';
                Debug.Log("now e");
            }
            else if (facing == 'E'){
                facing = 'S';
                Debug.Log("now S");
            }
            else if (facing == 'S'){
                facing = 'W';
                Debug.Log("now w");
            }
            else if (facing == 'W'){
                facing = 'N';
                Debug.Log("now n");
            }
        }
        else {
            if (facing == 'N'){
                facing = 'W';
                Debug.Log("now w");
            }
            else if (facing == 'E'){
                facing = 'N';
                Debug.Log("now n");
            }
            else if (facing == 'S'){
                facing = 'E';
                Debug.Log("now e");
            }
            else if (facing == 'W'){
                facing = 'S';
                Debug.Log("now s");
            }
        }
    }

    private void checkSameCoord(Vector3 temp){
        Collider[] sameCoord = Physics.OverlapSphere(temp, 9.8f); //9.8 is absolute smallest
        foreach (Collider temp1 in sameCoord)
        {
            if (temp1.tag != "Player" && temp1.tag != "ground"){
                Debug.Log("CSC - destroyed " + temp1.name);
                Destroy(temp1.gameObject.transform.parent.gameObject);
            }
        }
    }
    
    private void spawnStr() {
        newTrack = Instantiate(straight, coord, rotate);
        coord = newTrack.transform.position;
    }

    private void spawnTurnR() {
        newTrack = Instantiate(turnR, coord, rotate);
        coord = newTrack.transform.position;
        calcFacing("right");
    }

    private void spawnTurnL() {
        newTrack = Instantiate(turnL, coord, rotate);
        coord = newTrack.transform.position;
        calcFacing("left");
    }
    
    //Z=North, X=East
    //0 = straight;


    public void onCollision(Collider other) {
        if (other.tag.Equals("trigger")){
            GameObject[] temp = GameObject.FindGameObjectsWithTag("trigger");
            foreach (GameObject temp2 in temp){
                Debug.Log("OTE - destroyed " + temp2.name);
                Destroy(temp2);
            }
            calcNext();
        }
    }

    private void changeCoordRotate(int a, int b, int c){
        coord = coord + new Vector3(a, 0, c);
        rotate = Quaternion.Euler(0, b, 0);
    }

    private void calcNext(){
        random = Random.Range(0, 3);
        if (random == 0) {
            if (facing == 'N') {
                changeCoordRotate(0, 0, 20);
                checkSameCoord(coord);             
                spawnStr();
            }

            else if (facing == 'S') {
                changeCoordRotate(0, 180, -20);
                checkSameCoord(coord);
                spawnStr();
            }

            else if (facing == 'E') {
                changeCoordRotate(20, 90, 0);
                checkSameCoord(coord);
                spawnStr();
            }

            else if (facing == 'W') {
                changeCoordRotate(-20, 270, 0);  
                checkSameCoord(coord);            
                spawnStr();
            }
        }

        if (random == 1) {                                      //turnR
            if (facing == 'N') {
                changeCoordRotate(0, 0, 20);  
                checkSameCoord(coord);
                spawnTurnR();
            }

            else if (facing == 'S') {
                changeCoordRotate(0, 180, -20);
                checkSameCoord(coord);      
                spawnTurnR();
            }

            else if (facing == 'E') {
                changeCoordRotate(20, 90, 0);
                checkSameCoord(coord);
                spawnTurnR();
            }

            else if (facing == 'W') {
                changeCoordRotate(-20, 270, 0);
                checkSameCoord(coord);
                spawnTurnR();
            }
        }

        if (random == 2) {                                      //turnL
            if (facing == 'N') {
                changeCoordRotate(0, 0, 20); 
                checkSameCoord(coord);
                spawnTurnL();
            }

            else if (facing == 'S') {
                changeCoordRotate(0, 180, -20); 
                checkSameCoord(coord);
                spawnTurnL();
            }

            else if (facing == 'E') {
                changeCoordRotate(20, 90, 0);
                checkSameCoord(coord);
                spawnTurnL();
            }

            else if (facing == 'W') {
                changeCoordRotate(-20, 270, 0);
                checkSameCoord(coord);
                spawnTurnL();
            }
        }
    }
}
