nuget pack Transformalize.Provider.Bogus.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Bogus.Autofac.nuspec -OutputDirectory "c:\temp\modules"

nuget push "c:\temp\modules\Transformalize.Provider.Bogus.0.11.1-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
nuget push "c:\temp\modules\Transformalize.Provider.Bogus.Autofac.0.11.1-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json


