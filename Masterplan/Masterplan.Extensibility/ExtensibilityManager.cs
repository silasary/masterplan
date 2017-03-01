using Masterplan.Data;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utils;

namespace Masterplan.Extensibility
{
	internal class ExtensibilityManager : IApplication
	{
		private MainForm fMainForm;

		public Project Project
		{
			get
			{
				return Session.Project;
			}
			set
			{
				Session.Project = value;
			}
		}

		public PlotPoint SelectedPoint
		{
			get
			{
				return this.fMainForm.PlotView.SelectedPoint;
			}
		}

		public Encounter CurrentEncounter
		{
			get
			{
				return Session.CurrentEncounter;
			}
		}

		public string ProjectFile
		{
			get
			{
				return Session.FileName;
			}
			set
			{
				Session.FileName = value;
			}
		}

		public bool ProjectModified
		{
			get
			{
				return Session.Modified;
			}
			set
			{
				Session.Modified = value;
			}
		}

		public List<Library> Libraries
		{
			get
			{
				return Session.Libraries;
			}
		}

		public List<IAddIn> AddIns
		{
			get
			{
				return Session.AddIns;
			}
		}

		public ExtensibilityManager(MainForm main_form)
		{
			this.fMainForm = main_form;
            this.Load(Path.Combine(Application.StartupPath, "AddIns"));
            this.Load(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Masterplan", "Addins"));
		}

		public void Load(string path)
		{
			if (File.Exists(path))
			{
				Assembly assembly = Assembly.LoadFile(path);
				if (assembly != null)
				{
					this.LoadFile(assembly);
				}
			}
			else if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles("*.dll");
				FileInfo[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					FileInfo fileInfo = array[i];
					this.Load(fileInfo.FullName);
				}
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				DirectoryInfo[] array2 = directories;
				for (int j = 0; j < array2.Length; j++)
				{
					DirectoryInfo directoryInfo2 = array2[j];
					this.Load(directoryInfo2.FullName);
				}
			}
			Session.AddIns.Sort(new Comparison<IAddIn>(compare_addins));
		}

		private void LoadFile(Assembly assembly)
		{
			try
			{
				Type[] types = assembly.GetTypes();
				Type[] array = types;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i];
					if (this.IsAddin(type))
					{
						ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
						if (constructor != null)
						{
							IAddIn addIn = constructor.Invoke(null) as IAddIn;
							if (addIn != null)
							{
								this.Install(addIn);
							}
						}
					}
				}
			}
			catch (ReflectionTypeLoadException ex)
			{
				string name = assembly.ManifestModule.Name;
				LogSystem.Trace("The add-in '" + name + "' could not be loaded; contact the developer for an updated version.");
				Exception[] loaderExceptions = ex.LoaderExceptions;
				for (int j = 0; j < loaderExceptions.Length; j++)
				{
					Exception value = loaderExceptions[j];
					Console.WriteLine(value);
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Trace(ex2);
			}
		}

		private bool IsAddin(Type t)
		{
			Type[] interfaces = t.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				Type type = interfaces[i];
				if (type != null && type == typeof(IAddIn))
				{
					return true;
				}
			}
			return false;
		}

		private void Install(IAddIn addin)
		{
			bool flag = addin.Initialise(this);
			if (flag)
			{
				Session.AddIns.Add(addin);
			}
		}

		public void UpdateView()
		{
			this.fMainForm.UpdateView();
		}

		public void SaveLibrary(Library lib)
		{
			string libraryFilename = Session.GetLibraryFilename(lib);
			Serialisation<Library>.Save(libraryFilename, lib, SerialisationMode.Binary);
		}

		private static int compare_addins(IAddIn x, IAddIn y)
		{
			return x.Name.CompareTo(y.Name);
		}
    }
}
