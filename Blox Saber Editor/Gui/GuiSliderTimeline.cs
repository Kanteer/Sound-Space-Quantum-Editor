﻿using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Sound_Space_Editor.Gui
{
	class GuiSliderTimeline : GuiSlider
	{
		public GuiSliderTimeline(float x, float y, float sx, float sy) : base(x, y, sx, sy)
		{

		}


		protected override void RenderTimeline(RectangleF rect)
		{
			// color 1

			string rc1 = EditorWindow.Instance.ReadLine(EditorWindow.Instance.settingsFile, 17);
			string[] c1values = rc1.Split(',');
			int[] Color1 = Array.ConvertAll<string, int>(c1values, int.Parse);


			base.RenderTimeline(rect);

			if (EditorWindow.Instance.MusicPlayer.TotalTime == TimeSpan.Zero)
				return;

			for (int i = 0; i < EditorWindow.Instance.Notes.Count; i++)
			{
				var note = EditorWindow.Instance.Notes[i];

				var progress = note.Ms / EditorWindow.Instance.MusicPlayer.TotalTime.TotalMilliseconds;

				var x = rect.X + progress * rect.Width;
				var y = rect.Y + rect.Height / 2f;

				GL.Color4(Color.FromArgb(Color1[0], Color1[1], Color1[2]));
				Glu.RenderQuad((int)x, y + rect.Height * 2, 1, rect.Height);
			}
		}
	}
}