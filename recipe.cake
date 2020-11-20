#load nuget:?package=Cake.Recipe&version=2.1.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
  context: Context,
  buildSystem: BuildSystem,
  sourceDirectoryPath: "./src",
  title: "Topshelf.LightCore",
  masterBranchName: "main",
  repositoryOwner: "nils-org",
  shouldRunDotNetCorePack: true,
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