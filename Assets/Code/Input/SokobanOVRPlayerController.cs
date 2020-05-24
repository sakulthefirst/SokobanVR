using Assets.Code.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class SokobanOVRPlayerController : OVRPlayerController
{

    // from parent need rename for serialization
    private float MoveScale1 = 1.0f;
    private float MoveScaleMultiplier1 = 1.0f;
   
    // how often input gets updated
    private float updateRate = 0.100f;
    private float timer = 0;

    // is controller is currently moving
    private bool isMoving = false;

    // right and left controller
    private ControllerMovementResolver leftMovementResolver = new ControllerMovementResolver(OVRInput.Controller.LTouch);
    private ControllerMovementResolver rightMovementResolver = new ControllerMovementResolver(OVRInput.Controller.RTouch);

    // how often same direction is tolerated
    private int sameDirectionCount = 0;
    private int sameDirectionIgnoreOffset = 3;

    public override void UpdateMovement()
    {
        // do nothing
    }

    protected override void UpdateController()
    {
        base.UpdateController();

        // get Controller Position
        timer += Time.deltaTime;
        if (timer >= updateRate)
        {
            timer = 0;
            leftMovementResolver.Update();
            rightMovementResolver.Update();

            if(rightMovementResolver.Direction == leftMovementResolver.Direction)
            {
                sameDirectionCount++;
            }
            else
            {
                sameDirectionCount = 0;
            }


            isMoving = leftMovementResolver.IsMoving && rightMovementResolver.IsMoving && (sameDirectionCount < sameDirectionIgnoreOffset);
            var speed = (leftMovementResolver.Speed + rightMovementResolver.Speed) / 2; 
            MoveScaleMultiplier1 = speed * 2 + 1;
        }
        if (isMoving)
        {

            Quaternion ort = transform.rotation;
            Vector3 ortEuler = ort.eulerAngles;
            ortEuler.z = ortEuler.x = 0f;
            ort = Quaternion.Euler(ortEuler);
            float moveInfluence = Acceleration * 0.1f * MoveScale1 * MoveScaleMultiplier1;
            var move = ort * (transform.lossyScale.z * moveInfluence * Vector3.forward);
            Controller.Move(move);
        }
    }
}

