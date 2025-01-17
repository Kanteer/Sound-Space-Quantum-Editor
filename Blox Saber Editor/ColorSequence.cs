﻿using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Linq;

namespace Sound_Space_Editor
{
	class ColorSequence
	{
		private readonly Color[] _colors;
		private int _index;


		public ColorSequence()
		{
			// color 1

			string rc1 = EditorWindow.Instance.ReadLine("settings.ini", 25);
			string[] c1values = rc1.Split(',');
			int[] Color1 = Array.ConvertAll<string, int>(c1values, int.Parse);

			//color 2

			string rc2 = EditorWindow.Instance.ReadLine("settings.ini", 26);
			string[] c2values = rc2.Split(',');
			int[] Color2 = Array.ConvertAll<string, int>(c2values, int.Parse);

			_colors = new Color[] { Color.FromArgb(Color1[0], Color1[1], Color1[2]), Color.FromArgb(Color2[0], Color2[1], Color2[2]) };
		}

		public Color Next()
		{
			var color = _colors[_index];

			_index = (_index + 1) % _colors.Length;

			return color;
		}

		public void Reset()
		{
			_index = 0;
		}
	}
}