using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Input
{
    class ControllerMovementResolver
    {

        public enum DirectionEnum
        {
            Up, Down
        }

        public bool IsMoving { get => this.isMoving; protected set => this.isMoving = value; }
        public float Speed { get => this.speed; protected set => this.speed = value; }

        public DirectionEnum Direction { get => this.direction; protected set => this.direction = value; }


        // contoller to watch
        private OVRInput.Controller controller;

        // queu for speed detection
        private Queue<Vector3> positionQueue = new Queue<Vector3>();
        private int positionQueueLength = 5;

        // moving tolerance - above Controller is set to moving
        private float moveOffset = 0.05f;

        private bool isMoving = false;
        private float speed;
        private DirectionEnum direction = DirectionEnum.Down;


        public ControllerMovementResolver(OVRInput.Controller controller)
        {
            this.controller = controller;
        }

        public void Update()
        {
            positionQueue.Enqueue(OVRInput.GetLocalControllerPosition(this.controller));
            if (positionQueue.Count >= positionQueueLength)
            {

                Vector3? lastPosition = null;
                var value = 0f;
                foreach (var position in positionQueue)
                {
                    if (lastPosition != null)
                    {
                        if (lastPosition.Value.y > position.y)
                        {
                            value += Math.Abs(lastPosition.Value.y - position.y);
                        }
                        else
                        {
                            value += Math.Abs(position.y - lastPosition.Value.y);
                        }

                    }
                    lastPosition = position;
                }

                positionQueue.Dequeue();
                isMoving = value > moveOffset;
                speed = value;
            }

            var positionArray = positionQueue.ToArray();
            direction = positionArray[0].y > positionArray[1].y ? DirectionEnum.Up : DirectionEnum.Down;
        }

    }
}
