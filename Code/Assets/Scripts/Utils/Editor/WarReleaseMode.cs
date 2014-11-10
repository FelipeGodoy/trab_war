using UnityEngine;
using UnityEditor;

public class WarReleaseMode
{
	const string localMode= "LOCAL_MODE";

	[MenuItem("War/Release mode/online")]
	public static void SetOnlineMode()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebPlayer, string.Empty);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, string.Empty);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, string.Empty);
	}
	
	[MenuItem("War/Release mode/online", true)]
	public static bool CanChooseOnline()
	{
		string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebPlayer);
		return symbols.Contains(localMode);
	}
	
	[MenuItem("War/Release mode/offline")]
	public static void SetOfflineMode()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebPlayer, localMode);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, localMode);
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, localMode);
	}
	
	[MenuItem("War/Release mode/offline", true)]
	public static bool CanChooseOffline()
	{
		string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebPlayer);
		return !symbols.Contains(localMode);
	}
}