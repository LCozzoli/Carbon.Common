﻿/*
 *
 * Copyright (c) 2022-2024 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Core;

[Serializable]
public class Defines
{
	public static void Initialize()
	{
		GetRootFolder();
		GetConfigsFolder();
		GetModulesFolder();
		GetDataFolder();
		GetScriptsFolder();
		GetExtensionsFolder();
		GetLogsFolder();
		GetLangFolder();

		try
		{
			OsEx.Folder.DeleteContents(GetTempFolder());
		}
		catch (Exception ex)
		{
			Logger.Warn($"Failed clearing up the temporary folder. ({ex.Message})\n{ex.StackTrace}");
		}

		Logger.Log("Loaded folders");
	}

	internal static string _customRootFolder;
	internal static string _customScriptFolder;
	internal static string _customConfigFolder;
	internal static string _customDataFolder;
	internal static string _customLangFolder;
	internal static string _customModuleFolder;
	internal static string _customExtensionsFolder;
	internal static string _customHarmonyFolder;
	internal static string _customLogsFolder;
	internal static bool _commandLineInitialized;

	internal static void _initializeCommandLine()
	{
		if (_commandLineInitialized) return;
		_commandLineInitialized = true;

		_customRootFolder = CommandLineEx.GetArgumentResult("-carbon.rootdir");
		_customScriptFolder = CommandLineEx.GetArgumentResult("-carbon.scriptdir");
		_customConfigFolder = CommandLineEx.GetArgumentResult("-carbon.configdir");
		_customDataFolder = CommandLineEx.GetArgumentResult("-carbon.datadir");
		_customLangFolder = CommandLineEx.GetArgumentResult("-carbon.langdir");
		_customModuleFolder = CommandLineEx.GetArgumentResult("-carbon.moduledir");
		_customExtensionsFolder = CommandLineEx.GetArgumentResult("-carbon.extdir");
		_customLogsFolder = CommandLineEx.GetArgumentResult("-carbon.logdir");
		_customHarmonyFolder = CommandLineEx.GetArgumentResult("-carbon.harmonydir");
	}

	public static string GetConfigFile()
	{
		_initializeCommandLine();
		return Path.Combine(GetRootFolder(), "config.json");
	}
	public static string GetClientConfigFile()
	{
		_initializeCommandLine();
		return Path.Combine(GetRootFolder(), "config.client.json");
	}
	public static string GetMonoProfilerConfigFile()
	{
		_initializeCommandLine();
		return Path.Combine(GetRootFolder(), "config.profiler.json");
	}
	public static string GetCarbonAutoFile()
	{
		_initializeCommandLine();
		return Path.Combine(GetRootFolder(), "config.auto.json");
	}

	public static string GetRootFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customRootFolder) ? Path.Combine($"{Application.dataPath}/..", "carbon") : _customRootFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetCompilerFolder()
	{
		string folder = Path.Combine($"{GetRootFolder()}", "compiler");
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetLibFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customModuleFolder) ? Path.Combine(GetManagedFolder(), "lib") : _customModuleFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetConfigsFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customConfigFolder) ? Path.Combine(GetRootFolder(), "configs") : _customConfigFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetModulesFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customModuleFolder) ? Path.Combine(GetRootFolder(), "modules") : _customModuleFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetManagedModulesFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(Path.Combine(GetManagedFolder(), "modules"));
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetDataFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customDataFolder) ? Path.Combine(GetRootFolder(), "data") : _customDataFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetScriptsFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customScriptFolder) ? Path.Combine(GetRootFolder(), "plugins") : _customScriptFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetScriptBackupFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(Path.Combine(GetScriptsFolder(), "backups"));
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetScriptDebugFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(Path.Combine(GetScriptsFolder(), "debug"));
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetZipDevFolder()
	{
		var folder = Path.Combine(GetScriptsFolder(), "cszip_dev");
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetExtensionsFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customExtensionsFolder) ? Path.Combine(GetRootFolder(), "extensions") : _customExtensionsFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetHarmonyFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customHarmonyFolder) ? Path.Combine(GetRootFolder(), "harmony") : _customHarmonyFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetManagedFolder()
	{
		_initializeCommandLine();
		var folder = Path.Combine($"{GetRootFolder()}", "managed");
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetLogsFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customLogsFolder) ? Path.Combine(GetRootFolder(), "logs") : _customLogsFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetProfilesFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(Path.Combine(GetLogsFolder(), "profiler"));
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetLangFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(string.IsNullOrEmpty(_customLangFolder) ? Path.Combine(GetRootFolder(), "lang") : _customLangFolder);
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetTempFolder()
	{
		_initializeCommandLine();
		var folder = Path.Combine($"{GetRootFolder()}", "temp");
		Directory.CreateDirectory(folder);

		return folder;
	}
	public static string GetRustRootFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(Path.Combine(Path.Combine(Application.dataPath, "..")));

		return folder;
	}
	public static string GetRustManagedFolder()
	{
		_initializeCommandLine();
		var folder = Path.GetFullPath(Path.Combine(Path.Combine(Application.dataPath, "Managed")));

		return folder;
	}
}
