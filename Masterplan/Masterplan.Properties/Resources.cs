using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Masterplan.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Masterplan.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap Area
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Area", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Aura
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Aura", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Close
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Close", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Melee
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Melee", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap MeleeBasic
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("MeleeBasic", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Purpled20
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Purpled20", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Ranged
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Ranged", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap RangedBasic
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("RangedBasic", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap Scroll
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Scroll", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
