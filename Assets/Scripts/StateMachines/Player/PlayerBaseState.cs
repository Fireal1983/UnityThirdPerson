using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State //? By making this abstract we do not need to meet the implementation reqs. We are telling the compiler that the inheriting class will do this
{

    protected PlayerStateMachine stateMachine;  //? protected : only classes that inherit can access


    //! Constructor
    public PlayerBaseState(PlayerStateMachine stateMachine) //? Get reference to the PlayerStateMachine via constructor
    {
        this.stateMachine = stateMachine;
    }   


}


//? NOTES
//  1)  I inherit from state machine
//  2)  I'm calling my own method switchState, I'm setting the state as PlayerFreeLook and sending myself as the reference for which state machine to use 