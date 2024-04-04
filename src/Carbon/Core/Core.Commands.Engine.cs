﻿using Facepunch;
using Newtonsoft.Json;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Core;

public partial class CorePlugin : CarbonPlugin
{
	[ConsoleCommand("shutdown", "Completely unloads Carbon from the game, rendering it fully vanilla. WARNING: This is for testing purposes only.")]
	[AuthLevel(2)]
	private void Shutdown(ConsoleSystem.Arg arg)
	{
		Community.Runtime.Uninitialize();
	}

	[ConsoleCommand("help", "Returns a brief introduction to Carbon.")]
	[AuthLevel(2)]
	private void Help(ConsoleSystem.Arg arg)
	{
		arg.ReplyWith($"To get started, run the `c.find c.` or `c.find carbon` to list all Carbon commands.\n" +
			$"To list all currently loaded plugins, execute `c.plugins`.\n" +
			$"For more information, please visit https://docs.carbonmod.gg or join the Discord server at https://discord.gg/carbonmod\n" +
			$"You're currently running {Community.Runtime.Analytics.InformationalVersion}.");
	}

	[ConsoleCommand("version", "Version information of the Carbon build and Rust.")]
	private void VersionCall(ConsoleSystem.Arg arg)
	{
		var analytics = Community.Runtime.Analytics;

		if (arg.IsServerside)
		{
			arg.ReplyWith($"Carbon" +
#if MINIMAL
					$" Minimal" +
#endif
				$" {analytics.Version}/{analytics.Platform}/{analytics.Protocol} [{Build.Git.Branch}] [{Build.Git.Tag}] on Rust {BuildInfo.Current.Build.Number}/{Rust.Protocol.printable} ({BuildInfo.Current.BuildDate})");
		}
		else
		{
			arg.ReplyWith($"Carbon" +
#if MINIMAL
					$" Minimal" +
#endif
				$" <color=#d14419>{analytics.Version}/{analytics.Platform}/{analytics.Protocol}</color> [{Build.Git.Branch}] [{Build.Git.Tag}] on Rust <color=#d14419>{BuildInfo.Current.Build.Number}/{Rust.Protocol.printable}</color> ({BuildInfo.Current.BuildDate}).");

		}
	}

	[ConsoleCommand("build", "Information about the currently running Carbon build.")]
	private void BuildCall(ConsoleSystem.Arg arg)
	{
		arg.ReplyWith(Community.Runtime.Analytics.InformationalVersion);
	}

	[ConsoleCommand("protocol", "Protocol information used by the hook system of the Carbon build.")]
	private void Protocol(ConsoleSystem.Arg arg)
	{
		arg.ReplyWith(Community.Runtime.Analytics.Protocol);
	}

	[ConsoleCommand("commit", "Information about the Git commit of this build.")]
	private void Commit(ConsoleSystem.Arg arg)
	{
		var builder = PoolEx.GetStringBuilder();
		var added = Build.Git.Changes.Count(x => x.Type == Build.Git.AssetChange.ChangeTypes.Added);
		var modified = Build.Git.Changes.Count(x => x.Type == Build.Git.AssetChange.ChangeTypes.Modified);
		var deleted = Build.Git.Changes.Count(x => x.Type == Build.Git.AssetChange.ChangeTypes.Deleted);

		builder.AppendLine($"  Branch:  {Build.Git.Branch}");
		builder.AppendLine($"  Author:  {Build.Git.Author}");
		builder.AppendLine($" Comment:  {Build.Git.Comment}");
		builder.AppendLine($"    Date:  {Build.Git.Date}");
		builder.AppendLine($"     Tag:  {Build.Git.Tag}");
		builder.AppendLine($"    Hash:  {Build.Git.HashShort} ({Build.Git.HashLong})");
		builder.AppendLine($"     Url:  {Build.Git.Url}");
		builder.AppendLine($"   Debug:  {Build.IsDebug}");
		builder.AppendLine($" Changes:  {added} added, {modified} modified, {deleted} deleted");

		arg.ReplyWith(builder.ToString());
		PoolEx.FreeStringBuilder(ref builder);
	}
}
