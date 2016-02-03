﻿// Project Cows -- GearShift Games
// Written by D. Sinclair, 2016
// ================
// Debug.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Project_Cows.Source.System.Graphics;
using Project_Cows.Source.System.Graphics.Sprites;

namespace Project_Cows.Source.System {
	public static class Debug {
		// Class for displaying a debug screen over the top of the game
		// ================

		// Variables
		private static List<Sprite> m_sprites = new List<Sprite>();
		private static List<DebugText> m_text = new List<DebugText>();

		// Methods
		public static void Render(ref GraphicsHandler graphicsHandler_) {
			// Render the debug screen
			// ================
			if(Settings.m_debug) {
				graphicsHandler_.StartDrawing();

				// Render debug sprites (collision boxes, etc.)
				foreach(Sprite s in m_sprites) {
					graphicsHandler_.DrawSprite(s);
				}

				// Render debug text
				foreach(DebugText dt in m_text) {
					graphicsHandler_.DrawText(dt);
				}

				graphicsHandler_.DrawText("Debug Screen", new Vector2(10.0f, 10.0f), Color.Red);
				graphicsHandler_.DrawText("Debug Screen", new Vector2(11.0f, 10.0f), Color.Red);

				graphicsHandler_.StopDrawing();
			}

			m_sprites.Clear();
			m_text.Clear();
		}

		public static void AddSprite(Sprite sprite_) {
			// Add a sprite to the debug screen
			// ================

			m_sprites.Add(sprite_);
		}

		public static void AddText(DebugText text_) {
			// Add a sprite to the debug screen
			// ================

			m_text.Add(text_);
		}
	}

	public class DebugText {
		// Class for storing debug text
		// ================

		// Variables
		private string m_text;
		private Vector2 m_position;
		private Color m_colour;

		// Methods
		public DebugText(string text_, Vector2 position_) {
			// DebugText constructor
			// ================

			m_text = text_;
			m_position = position_;
			m_colour = Color.Red;
		}

		public DebugText(string text_, Vector2 position_, Color colour_) {
			// DebugText constructor
			// ================

			m_text = text_;
			m_position = position_;
			m_colour = colour_;
		}

		// Getters
		public string GetText() { return m_text; }

		public Vector2 GetPosition() { return m_position; }

		public Color GetColor() { return m_colour; }
	}
}