#load nuget:?package=Cake.Recipe&version=3.0.1

var standardNotificationMessage = "Version {0} of {1} has just been released, it will be available here https://www.nuget.org/packages/{1}, once package indexing is complete.";

Environment.SetVariableNames();

BuildParameters.SetParameters(
  context: Context,
  buildSystem: BuildSystem,
  sourceDirectoryPath: "./src",
  title: "Topshelf.LightCore",
  masterBranchName: "main",
  repositoryOwner: "nils-org",
  shouldRunDotNetCorePack: true,
  twitterMessage: standardNotificationMessage,
  shouldUseDeterministicBuilds: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

// copy logo into docs
Task("copy-res-logo")
  .IsDependeeOf("Publish-Documentation")
  .IsDependeeOf("Preview-Documentation")
  .IsDependeeOf("Force-Publish-Documentation")
  .Does(() => {
    CopyDirectory(Directory("res"), Directory("docs/input/res"));
});

Build.RunDotNetCore();