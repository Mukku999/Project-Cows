﻿// Project Cows -- GearShift Games
// Written by D. Sinclair, 2016
//            D. Divers, 2016
// ================
// Vehicle.cs

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Project_Cows.Source.System;

namespace Project_Cows.Source.Application.Entity {
    class Vehicle : Entity {
        // Class for the player vehicles
        // ================

        // Variables
        private float m_velocity;
        private float m_steeringValue;
        private bool m_braking;

        private const float MAXSPEED = 5.0f;
        private const float ACCELERATION_RATE = 0.05f;
        private const float DECELERATION_RATE = -0.15f;
		private const float STEERING_SENSITIVITY = 10.0f;

        public Vector2 Forward, Right, Velocity;

        public float sliding;
        public int turn;
        public float slide;
      
        // Methods
		public Vehicle(ContentManager content_, Texture2D texture_, Vector2 position_, float m_rotation_) : base(content_, texture_, position_, m_rotation_) {
            // Vehicle constructor
            // ================
            m_rotation = m_rotation_;
            m_velocity = 0.0f;
            m_steeringValue = 0.0f;
            m_braking = false;
        }

        public void Update(float steeringValue_, bool braking_) {
            // Updates the vehicle
            // ================
        
            m_steeringValue = steeringValue_;
            m_braking = braking_;

            if (!m_braking)
            {
                m_velocity *= 1.02f;
                m_velocity += 0.01f;
            }
            else
            {
                sliding -= 0.02f;
                sliding *= 1.02f;
            }
            
            //if turning right
            if(steeringValue_ > 0.2)
            {
                if (m_braking)
                {
                    slide += 0.1f * (m_velocity / 10);
                    if (m_velocity > 2.0f)
                        SetRotationRadians(GetRotationRadians() + (0.02f + (-sliding / 60)));
                    if (m_velocity < 2.0f && m_velocity > 0.5f)
                        SetRotationRadians(GetRotationRadians() + 0.03f);
                }
                else
                    SetRotationRadians(GetRotationRadians() + (0.01f + (-sliding / 60)) * (m_velocity / 5));
            }
            //if turning left
            if (steeringValue_ < -0.2)
            {
                if (m_braking)
                {
                    slide -= 0.1f * (m_velocity / 10);
                    if (m_velocity > 2.0f)
                        SetRotationRadians(GetRotationRadians() - (0.02f + (-sliding / 60)));
                    if (m_velocity < 2.0f && m_velocity > 0.5f)
                        SetRotationRadians(GetRotationRadians() - 0.03f);
                }
                else
                    SetRotationRadians(GetRotationRadians() - (0.01f + (-sliding / 60)) * (m_velocity / 5));
            }

            if (m_velocity > MAXSPEED)
                m_velocity = MAXSPEED;
            m_velocity *= 0.99f;
            sliding *= 0.99f;

            //if (sliding > 0.01f || sliding < -0.01f)
            //   m_velocity *= 0.99f;
            if (sliding > 0.3f)
                sliding = 0.3f;
            if (sliding < -0.3f)
                sliding = -0.3f;
            if (slide > 1)
                slide = 1.0f;
            if (slide < -1)
                slide = -1.0f;

            m_position.X += m_velocity * (float)Math.Cos(GetRotationRadians() + (slide * sliding));
            m_position.Y += m_velocity * (float)Math.Sin(GetRotationRadians() + (slide * sliding));
            //this.SetRotationDegrees(m_rotation);
            Debug.AddText(new DebugText(GetRotationRadians().ToString(), new Vector2(500, 90)));
            //reset values to false, to check the next update

            m_braking = false;
			// Update position_ and m_rotation of the vehicle's sprite
			UpdateCollider();
        }
    }
}
