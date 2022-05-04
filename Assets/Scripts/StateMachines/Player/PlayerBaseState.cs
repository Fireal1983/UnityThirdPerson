using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State //? By making this abstract we do not need to meet the implementation reqs. We are telling the compiler that the inheriting class will do this
{

    protected PlayerStateMachine stateMachine;  //? protected : only classes that inherit can access

    public PlayerBaseState(PlayerStateMachine stateMachine) //? Get reference to the PlayerStateMachine via constructor
    {
        this.stateMachine = stateMachine;
    }   // Essentially we swap this code for the inheriting classes implementation of State - Enter(), Tick() etc.


}
