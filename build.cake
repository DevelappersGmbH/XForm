class BuildContext
{
  private readonly ICakeContext _context;

  public string Configuration { get; }

  public bool IsRelease => Equals(Configuration, "Release");

  public DirectoryPath OutputDir = new DirectoryPath("./artifacts");
  public FilePath SolutionFile => new FilePath("./XForm.sln");
  public FilePath ProjectFile = new FilePath("./XForm/XForm.csproj");
  public FilePath TestProjectFile => new FilePath("./XForm.Tests/XForm.Tests.csproj");

  public FilePath NugetPackageFile => _context.GetFiles("*/**/XForm.*.nupkg").FirstOrDefault();

  public FilePath SignedNugetPackageFile => _context.GetFiles("*/**/XForm.*.signed.nupkg").FirstOrDefault();

  public string NugetApiKey => _context.EnvironmentVariable("NUGET_API_KEY");

  public bool IsRunningOnBuildSystem { get; }

  public BuildContext(ICakeContext context) 
  {
    _context = context;

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

Task("CopyPackage")
  .IsDependentOn("Build")
  .Does<BuildContext>(context => {
    var signedPackageName = $"{context.NugetPackageFile.GetFilenameWithoutExtension()}.signed{context.NugetPackageFile.GetExtension()}";

    var destination = MakeAbsolute(context.OutputDir.GetFilePath(signedPackageName));
    CopyFile(context.NugetPackageFile, destination);

    Information($"Copied {context.NugetPackageFile} to {destination}");
  });

Task("PublishNugetPackage")
  .IsDependentOn("CopyPackage")
  .WithCriteria<BuildContext>((cakeContext, context) => context.IsRelease)
  .WithCriteria<BuildContext>((CakeContext, context) => !string.IsNullOrEmpty(context.NugetApiKey))
  .Does<BuildContext>(context => {
    var nugetPackageFile = context.SignedNugetPackageFile;

    if (nugetPackageFile == null) {
      Error("Package was not created or signed.");
      return;
    }

    var settings = new NuGetPushSettings {
      Source = "https://api.nuget.org/v3/index.json",
      ApiKey = context.NugetApiKey
    };

    NuGetPush(nugetPackageFile, settings);
  });

Task("Default")
  .IsDependentOn("RunTests")
  .IsDependentOn("PublishNugetPackage");

RunTarget(target);
