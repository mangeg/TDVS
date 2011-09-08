using System;
using TDVS.Common.Resources;
using TDVS.EntitySystem;

namespace TDVS.Game.Components.UI
{
	/// <summary>
	/// Sections for <see cref="UIBorderComponent"/>
	/// </summary>
	[Flags]
	public enum BorderSections
	{
		None = 0x0,
		TopLeft = 0x1,
		TopRight = 0x2,
		BottomLeft = 0x4,
		BottomRight = 0x8,
		Top = 0x10,
		Bottom = 0x20,
		Left = 0x40,
		Right = 0x80,
		All = TopLeft | TopRight | BottomLeft | BottomRight | Top | Bottom | Left | Right,
		Corners = TopLeft | TopRight | BottomLeft | BottomRight,
		Sides = Top | Bottom | Left | Right,
	}

	public class UIBorderComponent : IComponent
	{
		public ResourceID TextureID { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public BorderSections Sections { get; set; }

		public UIBorderComponent()
		{
			Reset();
		}

		public void Reset()
		{
			TextureID = ResourceID.Zero;
			Height = 0;
			Width = 0;
			Sections = BorderSections.All;
		}
	}
}
