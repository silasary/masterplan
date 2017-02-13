using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Masterplan.UI
{
	internal partial class AboutBox : Form
	{
		public string AssemblyTitle
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (customAttributes.Length > 0)
				{
					AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
					if (assemblyTitleAttribute.Title != "")
					{
						return assemblyTitleAttribute.Title;
					}
				}
				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)customAttributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (customAttributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
			}
		}

		public AboutBox()
		{
			this.InitializeComponent();
			this.Text = string.Format("About {0}", this.AssemblyProduct);
			this.labelProductName.Text = this.AssemblyProduct;
			this.labelVersion.Text = string.Format("Version {0}", this.AssemblyVersion);
			this.labelCompanyName.Text = this.AssemblyCompany;
			this.textBoxCopyright.Text = this.AssemblyCopyright;
		}
	}
}
