class BuildContext
{
  public string Configuration { get; }

  public DirectoryPath OutputDir = new DirectoryPath("./artifacts");
  public FilePath SolutionFile => new FilePath("./XForm.sln");
  public FilePath ProjectFile = new FilePath("./XForm/XForm.csproj");
  public FilePath TestProjectFile => new FilePath("./XForm.Tests/XForm.Tests.csproj");

  public bool IsRunningOnBuildSystem { get; }

  public BuildContext(ICakeContext context) 
  {
    IsRunningOnBuildSystem = context.TravisCI().IsRunningOnTravisCI;
    Configuration = context.Argument("configuration", "Release");
  }
  
  public MSBuildSettings DefaultBuildSettings => new MSBuildSettings 
  {
    Configuration = Configuration
  };
}

var target = Argument("target", "Default");

Setup<BuildContext>(context => 
{
  return new BuildContext(Context);
});

Task("Clean")
  .Does<BuildContext>(context =>
  {
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");

    CleanDirectory(context.OutputDir);
  });

Task("Restore")
  .Does<BuildContext>(context => {
    var settings = context.DefaultBuildSettings.WithTarget("Restore");

    MSBuild(context.ProjectFile, settings);
    MSBuild(context.TestProjectFile, settings);
  });

Task("Build")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore")
  .Does<BuildContext>(context => {
    var settings = context.DefaultBuildSettings.WithTarget("Build");
  
    MSBuild(context.ProjectFile, settings);
    MSBuild(context.TestProjectFile, settings);
  });

Task("RunTests")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore")
  .IsDependentOn("Build")
  .Does<BuildContext>(context => {
    var testDir = new DirectoryPath(context.OutputDir + "/Tests/");
    
    EnsureDirectoryExists(testDir);

    var testProjectFile = context.TestProjectFile;
    var testXmlFile = new FilePath(testDir + testProjectFile.GetFilenameWithoutExtension().ToString() + ".xml");

    var settings = new DotNetCoreTestSettings
    {
      Configuration = context.Configuration,
      Logger = $"xunit;LogFilePath={MakeAbsolute(testXmlFile).FullPath}",
      NoRestore = true,
      NoBuild = true
    };

    DotNetCoreTest(testProjectFile.ToString(), settings);
  });

Task("Default")
  .IsDependentOn("RunTests");

RunTarget(target);
