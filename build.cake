var target = Argument("target", "Default");

Task("Default")
  .Does(() =>
  {
    Information("Hallo");
  });

RunTarget(target);
