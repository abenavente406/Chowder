using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Chowder.Prototype.Levels.Maps
{
    public enum MovementType
    {
        HORIZONTAL_LEFT,
        HORIZONTAL_RIGHT,
        VERTICAL_UP,
        VERTICAL_DOWN
    }

    public class MovingPlatform : Platform
    {
        public const float TOPSPEED = 4;

        private MovementType movement = MovementType.HORIZONTAL_LEFT;

        private Point startPos;
        private Point destinationPos;
        private Vector2 startVec;
        private Vector2 destinationVec;
        private float speed = 1.5F;
        private bool reverse = false;
        private Vector2 velocity;

        public override Vector2 Position
        {
            get { return pos; }
            set
            {
                pos = Vector2.Clamp(value, new Vector2(-destinationPos.X, -destinationPos.Y),
                    new Vector2(destinationPos.X, destinationPos.Y));
            }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = Vector2.Clamp(value, new Vector2(-TOPSPEED, -TOPSPEED), new Vector2(TOPSPEED, TOPSPEED)); }
        }

        public MovingPlatform(Point startpos, Point sizeInTiles, int amountOfBlocks, MovementType movementType)
            : base(new Vector2(startpos.X * 48, startpos.Y * 48), sizeInTiles)
        {
            this.startPos = startpos;
            this.movement = movementType;
            switch (movementType)
            {
                case MovementType.HORIZONTAL_LEFT:
                    destinationPos = new Point(startpos.X - amountOfBlocks, startpos.Y);
                    break;
                case MovementType.HORIZONTAL_RIGHT:
                    destinationPos = new Point(startpos.X + amountOfBlocks, startpos.Y);
                    break;
                case MovementType.VERTICAL_DOWN:
                    destinationPos = new Point(startpos.X, startpos.Y + amountOfBlocks);
                    break;
                case MovementType.VERTICAL_UP:
                    destinationPos = new Point(startpos.X, startpos.Y - amountOfBlocks);
                    break;
            }

            startVec = new Vector2(startpos.X * 48,
                startpos.Y * 48);
            destinationVec = new Vector2(destinationPos.X * 48,
                destinationPos.Y * 48);
        }

        public override void Update(GameTime gameTime)
        {
            float epsilon = .05f;

            switch (movement)
            {
                case MovementType.HORIZONTAL_LEFT:
                    if (reverse)
                    {
                        if (Math.Abs((startVec - pos).X) <= epsilon)
                            reverse = false;
                        velocity.X = speed;
                    }
                    else
                    {
                        if (Math.Abs((destinationVec - pos).X) <= epsilon)
                            reverse = true;
                        velocity.X = -speed;
                    }
                    break;
                case MovementType.HORIZONTAL_RIGHT:
                    if (reverse)
                    {
                        if (Math.Abs((startVec - pos).X) <= epsilon)
                            reverse = false;
                        velocity.X = -speed;
                    }
                    else
                    {
                        if (Math.Abs((destinationVec - pos).X) <= epsilon)
                            reverse = true;
                        velocity.X = speed;
                    }
                    break;
                case MovementType.VERTICAL_DOWN:
                    if (reverse)
                    {
                        if (Math.Abs((startVec - pos).Y) <= epsilon)
                            reverse = false;
                        velocity.Y = -speed;
                    }
                    else
                    {
                        if (Math.Abs((destinationVec - pos).Y) <= epsilon)
                            reverse = true;
                        velocity.Y = speed;
                    }
                    break;
                case MovementType.VERTICAL_UP:
                    if (reverse)
                    {
                        if (Math.Abs((startVec - pos).Y) <= epsilon)
                            reverse = false;
                        velocity.Y = speed;
                    }
                    else
                    {
                        if (Math.Abs((destinationVec - pos).Y) <= epsilon)
                            reverse = true;
                        velocity.Y = -speed;
                    }
                    break;
            }
            pos += velocity;
        }
    }
}
