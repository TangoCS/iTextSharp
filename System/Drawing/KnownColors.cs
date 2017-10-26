using System;

namespace iTextSharp.Drawing
{
	internal static class KnownColors
	{
		internal static uint[] ArgbValues;

		static KnownColors()
		{
			KnownColors.ArgbValues = new uint[]
			{
				0u,
				4292137160u,
				4278211811u,
				4294967295u,
				4286611584u,
				4293716440u,
				4289505433u,
				4285624164u,
				4294045666u,
				4294967295u,
				4278190080u,
				4278210200u,
				4289505433u,
				4281428677u,
				4294967295u,
				4278190208u,
				4292137160u,
				4286224095u,
				4292404472u,
				4294967265u,
				4278190080u,
				4294967295u,
				4278190080u,
				4292137160u,
				4294967295u,
				4278190080u,
				4278190080u,
				16777215u,
				4293982463u,
				4294634455u,
				4278255615u,
				4286578644u,
				4293984255u,
				4294309340u,
				4294960324u,
				4278190080u,
				4294962125u,
				4278190335u,
				4287245282u,
				4289014314u,
				4292786311u,
				4284456608u,
				4286578432u,
				4291979550u,
				4294934352u,
				4284782061u,
				4294965468u,
				4292613180u,
				4278255615u,
				4278190219u,
				4278225803u,
				4290283019u,
				4289309097u,
				4278215680u,
				4290623339u,
				4287299723u,
				4283788079u,
				4294937600u,
				4288230092u,
				4287299584u,
				4293498490u,
				4287609995u,
				4282924427u,
				4281290575u,
				4278243025u,
				4287889619u,
				4294907027u,
				4278239231u,
				4285098345u,
				4280193279u,
				4289864226u,
				4294966000u,
				4280453922u,
				4294902015u,
				4292664540u,
				4294506751u,
				4294956800u,
				4292519200u,
				4286611584u,
				4278222848u,
				4289593135u,
				4293984240u,
				4294928820u,
				4291648604u,
				4283105410u,
				4294967280u,
				4293977740u,
				4293322490u,
				4294963445u,
				4286381056u,
				4294965965u,
				4289583334u,
				4293951616u,
				4292935679u,
				4294638290u,
				4292072403u,
				4287688336u,
				4294948545u,
				4294942842u,
				4280332970u,
				4287090426u,
				4286023833u,
				4289774814u,
				4294967264u,
				4278255360u,
				4281519410u,
				4294635750u,
				4294902015u,
				4286578688u,
				4284927402u,
				4278190285u,
				4290401747u,
				4287852763u,
				4282168177u,
				4286277870u,
				4278254234u,
				4282962380u,
				4291237253u,
				4279834992u,
				4294311930u,
				4294960353u,
				4294960309u,
				4294958765u,
				4278190208u,
				4294833638u,
				4286611456u,
				4285238819u,
				4294944000u,
				4294919424u,
				4292505814u,
				4293847210u,
				4288215960u,
				4289720046u,
				4292571283u,
				4294963157u,
				4294957753u,
				4291659071u,
				4294951115u,
				4292714717u,
				4289781990u,
				4286578816u,
				4294901760u,
				4290547599u,
				4282477025u,
				4287317267u,
				4294606962u,
				4294222944u,
				4281240407u,
				4294964718u,
				4288696877u,
				4290822336u,
				4287090411u,
				4285160141u,
				4285563024u,
				4294966010u,
				4278255487u,
				4282811060u,
				4291998860u,
				4278222976u,
				4292394968u,
				4294927175u,
				4282441936u,
				4293821166u,
				4294303411u,
				4294967295u,
				4294309365u,
				4294967040u,
				4288335154u,
				4293716440u,
				4294967295u,
				4289505433u,
				4282226175u,
				4288526827u,
				4293716440u,
				4281428677u
			};
			bool flag = GDIPlus.RunningOnWindows();
			if (flag)
			{
				KnownColors.RetrieveWindowsSystemColors();
			}
		}

		private static uint GetSysColor(GetSysColorIndex index)
		{
			uint num = GDIPlus.Win32GetSysColor(index);
			return 4278190080u | (num & 255u) << 16 | (num & 65280u) | num >> 16;
		}

		private static void RetrieveWindowsSystemColors()
		{
			KnownColors.ArgbValues[1] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_ACTIVEBORDER);
			KnownColors.ArgbValues[2] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_ACTIVECAPTION);
			KnownColors.ArgbValues[3] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_CAPTIONTEXT);
			KnownColors.ArgbValues[4] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_APPWORKSPACE);
			KnownColors.ArgbValues[5] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNFACE);
			KnownColors.ArgbValues[6] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNSHADOW);
			KnownColors.ArgbValues[7] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_3DDKSHADOW);
			KnownColors.ArgbValues[8] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_3DLIGHT);
			KnownColors.ArgbValues[9] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNHIGHLIGHT);
			KnownColors.ArgbValues[10] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNTEXT);
			KnownColors.ArgbValues[11] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BACKGROUND);
			KnownColors.ArgbValues[12] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_GRAYTEXT);
			KnownColors.ArgbValues[13] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_HIGHLIGHT);
			KnownColors.ArgbValues[14] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_HIGHLIGHTTEXT);
			KnownColors.ArgbValues[15] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_HOTLIGHT);
			KnownColors.ArgbValues[16] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INACTIVEBORDER);
			KnownColors.ArgbValues[17] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INACTIVECAPTION);
			KnownColors.ArgbValues[18] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INACTIVECAPTIONTEXT);
			KnownColors.ArgbValues[19] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INFOBK);
			KnownColors.ArgbValues[20] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_INFOTEXT);
			KnownColors.ArgbValues[21] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENU);
			KnownColors.ArgbValues[22] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENUTEXT);
			KnownColors.ArgbValues[23] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_SCROLLBAR);
			KnownColors.ArgbValues[24] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_WINDOW);
			KnownColors.ArgbValues[25] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_WINDOWFRAME);
			KnownColors.ArgbValues[26] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_WINDOWTEXT);
			KnownColors.ArgbValues[168] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNFACE);
			KnownColors.ArgbValues[169] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNHIGHLIGHT);
			KnownColors.ArgbValues[170] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_BTNSHADOW);
			KnownColors.ArgbValues[171] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_GRADIENTACTIVECAPTION);
			KnownColors.ArgbValues[172] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_GRADIENTINACTIVECAPTION);
			KnownColors.ArgbValues[173] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENUBAR);
			KnownColors.ArgbValues[174] = KnownColors.GetSysColor(GetSysColorIndex.COLOR_MENUHIGHLIGHT);
		}

		public static Color FromKnownColor(KnownColor kc)
		{
			short num = (short)kc;
			bool flag = num <= 0 || (int)num >= KnownColors.ArgbValues.Length;
			Color result;
			if (flag)
			{
				result = Color.FromArgb(0, 0, 0, 0);
				result.state |= 4;
			}
			else
			{
				result = default(Color);
				result.state = 7;
				bool flag2 = num < 27 || num > 169;
				if (flag2)
				{
					result.state |= 8;
				}
				result.Value = (long)((ulong)KnownColors.ArgbValues[(int)num]);
			}
			result.knownColor = num;
			return result;
		}

		public static string GetName(short kc)
		{
			string result;
			switch (kc)
			{
			case 1:
				result = "ActiveBorder";
				break;
			case 2:
				result = "ActiveCaption";
				break;
			case 3:
				result = "ActiveCaptionText";
				break;
			case 4:
				result = "AppWorkspace";
				break;
			case 5:
				result = "Control";
				break;
			case 6:
				result = "ControlDark";
				break;
			case 7:
				result = "ControlDarkDark";
				break;
			case 8:
				result = "ControlLight";
				break;
			case 9:
				result = "ControlLightLight";
				break;
			case 10:
				result = "ControlText";
				break;
			case 11:
				result = "Desktop";
				break;
			case 12:
				result = "GrayText";
				break;
			case 13:
				result = "Highlight";
				break;
			case 14:
				result = "HighlightText";
				break;
			case 15:
				result = "HotTrack";
				break;
			case 16:
				result = "InactiveBorder";
				break;
			case 17:
				result = "InactiveCaption";
				break;
			case 18:
				result = "InactiveCaptionText";
				break;
			case 19:
				result = "Info";
				break;
			case 20:
				result = "InfoText";
				break;
			case 21:
				result = "Menu";
				break;
			case 22:
				result = "MenuText";
				break;
			case 23:
				result = "ScrollBar";
				break;
			case 24:
				result = "Window";
				break;
			case 25:
				result = "WindowFrame";
				break;
			case 26:
				result = "WindowText";
				break;
			case 27:
				result = "Transparent";
				break;
			case 28:
				result = "AliceBlue";
				break;
			case 29:
				result = "AntiqueWhite";
				break;
			case 30:
				result = "Aqua";
				break;
			case 31:
				result = "Aquamarine";
				break;
			case 32:
				result = "Azure";
				break;
			case 33:
				result = "Beige";
				break;
			case 34:
				result = "Bisque";
				break;
			case 35:
				result = "Black";
				break;
			case 36:
				result = "BlanchedAlmond";
				break;
			case 37:
				result = "Blue";
				break;
			case 38:
				result = "BlueViolet";
				break;
			case 39:
				result = "Brown";
				break;
			case 40:
				result = "BurlyWood";
				break;
			case 41:
				result = "CadetBlue";
				break;
			case 42:
				result = "Chartreuse";
				break;
			case 43:
				result = "Chocolate";
				break;
			case 44:
				result = "Coral";
				break;
			case 45:
				result = "CornflowerBlue";
				break;
			case 46:
				result = "Cornsilk";
				break;
			case 47:
				result = "Crimson";
				break;
			case 48:
				result = "Cyan";
				break;
			case 49:
				result = "DarkBlue";
				break;
			case 50:
				result = "DarkCyan";
				break;
			case 51:
				result = "DarkGoldenrod";
				break;
			case 52:
				result = "DarkGray";
				break;
			case 53:
				result = "DarkGreen";
				break;
			case 54:
				result = "DarkKhaki";
				break;
			case 55:
				result = "DarkMagenta";
				break;
			case 56:
				result = "DarkOliveGreen";
				break;
			case 57:
				result = "DarkOrange";
				break;
			case 58:
				result = "DarkOrchid";
				break;
			case 59:
				result = "DarkRed";
				break;
			case 60:
				result = "DarkSalmon";
				break;
			case 61:
				result = "DarkSeaGreen";
				break;
			case 62:
				result = "DarkSlateBlue";
				break;
			case 63:
				result = "DarkSlateGray";
				break;
			case 64:
				result = "DarkTurquoise";
				break;
			case 65:
				result = "DarkViolet";
				break;
			case 66:
				result = "DeepPink";
				break;
			case 67:
				result = "DeepSkyBlue";
				break;
			case 68:
				result = "DimGray";
				break;
			case 69:
				result = "DodgerBlue";
				break;
			case 70:
				result = "Firebrick";
				break;
			case 71:
				result = "FloralWhite";
				break;
			case 72:
				result = "ForestGreen";
				break;
			case 73:
				result = "Fuchsia";
				break;
			case 74:
				result = "Gainsboro";
				break;
			case 75:
				result = "GhostWhite";
				break;
			case 76:
				result = "Gold";
				break;
			case 77:
				result = "Goldenrod";
				break;
			case 78:
				result = "Gray";
				break;
			case 79:
				result = "Green";
				break;
			case 80:
				result = "GreenYellow";
				break;
			case 81:
				result = "Honeydew";
				break;
			case 82:
				result = "HotPink";
				break;
			case 83:
				result = "IndianRed";
				break;
			case 84:
				result = "Indigo";
				break;
			case 85:
				result = "Ivory";
				break;
			case 86:
				result = "Khaki";
				break;
			case 87:
				result = "Lavender";
				break;
			case 88:
				result = "LavenderBlush";
				break;
			case 89:
				result = "LawnGreen";
				break;
			case 90:
				result = "LemonChiffon";
				break;
			case 91:
				result = "LightBlue";
				break;
			case 92:
				result = "LightCoral";
				break;
			case 93:
				result = "LightCyan";
				break;
			case 94:
				result = "LightGoldenrodYellow";
				break;
			case 95:
				result = "LightGray";
				break;
			case 96:
				result = "LightGreen";
				break;
			case 97:
				result = "LightPink";
				break;
			case 98:
				result = "LightSalmon";
				break;
			case 99:
				result = "LightSeaGreen";
				break;
			case 100:
				result = "LightSkyBlue";
				break;
			case 101:
				result = "LightSlateGray";
				break;
			case 102:
				result = "LightSteelBlue";
				break;
			case 103:
				result = "LightYellow";
				break;
			case 104:
				result = "Lime";
				break;
			case 105:
				result = "LimeGreen";
				break;
			case 106:
				result = "Linen";
				break;
			case 107:
				result = "Magenta";
				break;
			case 108:
				result = "Maroon";
				break;
			case 109:
				result = "MediumAquamarine";
				break;
			case 110:
				result = "MediumBlue";
				break;
			case 111:
				result = "MediumOrchid";
				break;
			case 112:
				result = "MediumPurple";
				break;
			case 113:
				result = "MediumSeaGreen";
				break;
			case 114:
				result = "MediumSlateBlue";
				break;
			case 115:
				result = "MediumSpringGreen";
				break;
			case 116:
				result = "MediumTurquoise";
				break;
			case 117:
				result = "MediumVioletRed";
				break;
			case 118:
				result = "MidnightBlue";
				break;
			case 119:
				result = "MintCream";
				break;
			case 120:
				result = "MistyRose";
				break;
			case 121:
				result = "Moccasin";
				break;
			case 122:
				result = "NavajoWhite";
				break;
			case 123:
				result = "Navy";
				break;
			case 124:
				result = "OldLace";
				break;
			case 125:
				result = "Olive";
				break;
			case 126:
				result = "OliveDrab";
				break;
			case 127:
				result = "Orange";
				break;
			case 128:
				result = "OrangeRed";
				break;
			case 129:
				result = "Orchid";
				break;
			case 130:
				result = "PaleGoldenrod";
				break;
			case 131:
				result = "PaleGreen";
				break;
			case 132:
				result = "PaleTurquoise";
				break;
			case 133:
				result = "PaleVioletRed";
				break;
			case 134:
				result = "PapayaWhip";
				break;
			case 135:
				result = "PeachPuff";
				break;
			case 136:
				result = "Peru";
				break;
			case 137:
				result = "Pink";
				break;
			case 138:
				result = "Plum";
				break;
			case 139:
				result = "PowderBlue";
				break;
			case 140:
				result = "Purple";
				break;
			case 141:
				result = "Red";
				break;
			case 142:
				result = "RosyBrown";
				break;
			case 143:
				result = "RoyalBlue";
				break;
			case 144:
				result = "SaddleBrown";
				break;
			case 145:
				result = "Salmon";
				break;
			case 146:
				result = "SandyBrown";
				break;
			case 147:
				result = "SeaGreen";
				break;
			case 148:
				result = "SeaShell";
				break;
			case 149:
				result = "Sienna";
				break;
			case 150:
				result = "Silver";
				break;
			case 151:
				result = "SkyBlue";
				break;
			case 152:
				result = "SlateBlue";
				break;
			case 153:
				result = "SlateGray";
				break;
			case 154:
				result = "Snow";
				break;
			case 155:
				result = "SpringGreen";
				break;
			case 156:
				result = "SteelBlue";
				break;
			case 157:
				result = "Tan";
				break;
			case 158:
				result = "Teal";
				break;
			case 159:
				result = "Thistle";
				break;
			case 160:
				result = "Tomato";
				break;
			case 161:
				result = "Turquoise";
				break;
			case 162:
				result = "Violet";
				break;
			case 163:
				result = "Wheat";
				break;
			case 164:
				result = "White";
				break;
			case 165:
				result = "WhiteSmoke";
				break;
			case 166:
				result = "Yellow";
				break;
			case 167:
				result = "YellowGreen";
				break;
			case 168:
				result = "ButtonFace";
				break;
			case 169:
				result = "ButtonHighlight";
				break;
			case 170:
				result = "ButtonShadow";
				break;
			case 171:
				result = "GradientActiveCaption";
				break;
			case 172:
				result = "GradientInactiveCaption";
				break;
			case 173:
				result = "MenuBar";
				break;
			case 174:
				result = "MenuHighlight";
				break;
			default:
				result = string.Empty;
				break;
			}
			return result;
		}

		public static string GetName(KnownColor kc)
		{
			return KnownColors.GetName((short)kc);
		}

		public static Color FindColorMatch(Color c)
		{
			uint num = (uint)c.ToArgb();
			Color result;
			for (int i = 27; i < 167; i++)
			{
				bool flag = num == KnownColors.ArgbValues[i];
				if (flag)
				{
					result = KnownColors.FromKnownColor((KnownColor)i);
					return result;
				}
			}
			result = Color.Empty;
			return result;
		}

		public static void Update(int knownColor, int color)
		{
			KnownColors.ArgbValues[knownColor] = (uint)color;
		}
	}
}
