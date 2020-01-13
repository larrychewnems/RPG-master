using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBtn : MonoBehaviour
{
    public RPG.Control.PlayerController playerController;
    public Sprite buttonImageOn;
    public Sprite buttonImageOff;

    [SerializeField] bool inAttackMode = false;

    private void Awake() {
        playerController.inAttackMode = inAttackMode;
    }

    public void setAttackMode(){
        inAttackMode=!inAttackMode;
        if(inAttackMode){
            GetComponent<Image>().sprite = buttonImageOn;
        } else {GetComponent<Image>().sprite = buttonImageOff;}

        playerController.inAttackMode = inAttackMode;
    }
}
