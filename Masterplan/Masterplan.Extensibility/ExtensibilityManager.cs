using Masterplan.Data;
using Masterplan.Tools.Import;
using Masterplan.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        static ExtensibilityManager()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                try
                {

                    string location = Path.Combine(Path.GetDirectoryName((s as Assembly).Location), e.Name.Split(',')[0] + ".dll");
                    if (File.Exists(location))
                        return Assembly.LoadFile(location);
                }
                catch (Exception ex) when (!Debugger.IsAttached)
                {
                    LogSystem.Trace(ex);
                }
                return null;
            };
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
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        FileInfo fileInfo = files[i];
                        this.Load(fileInfo.FullName);
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Trace(ex);
                    }
                }
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				for (int j = 0; j < directories.Length; j++)
				{
					DirectoryInfo directoryInfo2 = directories[j];
					this.Load(directoryInfo2.FullName);
				}
			}
			Session.AddIns.Sort(new Comparison<IAddIn>(compare_addins));
		}

		private void LoadFile(Assembly assembly)
		{
			try
			{
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    string name = assembly.ManifestModule.Name;
                    LogSystem.Trace("The add-in '" + name + "' did not properly load; contact the developer for an updated version.");
                    Exception[] loaderExceptions = ex.LoaderExceptions;
                    for (int j = 0; j < loaderExceptions.Length; j++)
                    {
                        Exception value = loaderExceptions[j];
                        Console.WriteLine(value);
                    }
                    types = ex.Types;
                }

                Type[] array = types;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i];
                    if (type == null)
                        continue;
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
                    else if (this.IsProvider<IHeroProvider>(type))
                    {
                        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                        if (constructor != null)
                        {
                            var addIn = constructor.Invoke(null) as IHeroProvider;
                            if (addIn != null)
                            {
                                this.Install(addIn);
                            }
                        }
                    }
				}
			}
			catch (Exception ex2)
			{
				LogSystem.Trace(ex2);
			}
		}

        private bool IsProvider<T>(Type t)
        {
            Type[] interfaces = t.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                Type type = interfaces[i];
                if (type != null && type == typeof(T))
                {
                    return true;
                }
            }
            return false;
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
			bool success = addin.Initialise(this);
			if (success)
			{
				Session.AddIns.Add(addin);
			}
		}

        private void Install(IHeroProvider addIn)
        {
            AppImport.Providers.Add(addIn.ProviderName, addIn);
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
