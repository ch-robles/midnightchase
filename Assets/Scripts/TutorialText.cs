using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : MonoBehaviour
{
    // [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] GameObject one, two, three, four, five, six;

    int textCounter = 0;

    /*IEnumerator Start(){
        if (Manager.instance.tutorial){
            
            yield return new WaitForSeconds(4);

        } else {
            yield return null;
        }
    }*/

    void Start(){
        one.SetActive(false);
        two.SetActive(false);
        three.SetActive(false);
        four.SetActive(false);
        five.SetActive(false);
    }

    void Update() {
        if (Manager.instance.tutorial && (Manager.instance.GetGameState() != GameStates.countDown)){
            if (textCounter < 1){
                // mainText.text = "Move mouse to look around.";
                // Debug.Log("Getting input axis: " + Input.GetAxis("Mouse X"));
                one.SetActive(true);
            } 
            
            if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) && textCounter < 1) {
                // Debug.Log("Getting input axis: " + Input.GetAxis("Mouse X"));
                // mainText.text = "Press WASD to move around the map.";
                one.SetActive(false);
                two.SetActive(true);
                textCounter++;
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && textCounter < 2) {
                // Debug.Log("Getting input axis: " + Input.GetAxis("Mouse X"));
                // mainText.text = "Press M to open your map.";
                two.SetActive(false);
                three.SetActive(true);
                textCounter++;
            }

            if (Input.GetKey(KeyCode.M) && textCounter < 3) {
                // Debug.Log("Getting input axis: " + Input.GetAxis("Mouse X"));
                // mainText.text = "Objective: Head to the objective before time runs out or before the tikbalang catches you.";
                three.SetActive(false);
                four.SetActive(true);
                textCounter++;
            }

            if ((Input.mouseScrollDelta.y != 0) && textCounter < 4) {
                // Debug.Log("Getting input axis: " + Input.GetAxis("Mouse X"));
                // mainText.text = "Objective: Head to the objective before time runs out or before the tikbalang catches you.";
                four.SetActive(false);
                five.SetActive(true);
                textCounter++;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && textCounter < 5) {
                // Debug.Log("Getting input axis: " + Input.GetAxis("Mouse X"));
                // mainText.text = "Objective: Head to the objective before time runs out or before the tikbalang catches you.";
                five.SetActive(false);
                six.SetActive(true);
                textCounter++;
            }

            if (Manager.instance.GetGameState() == GameStates.raceOver){
                one.SetActive(false);
                two.SetActive(false);
                three.SetActive(false);
                four.SetActive(false);
                five.SetActive(false);
            }
        }
    }
}
