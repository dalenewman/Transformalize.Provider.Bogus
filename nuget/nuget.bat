nuget pack Transformalize.Provider.Bogus.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Bogus.Autofac.nuspec -OutputDirectory "c:\temp\modules"

REM nuget push "c:\temp\modules\Transformalize.Provider.Bogus.0.10.0-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Bogus.Autofac.0.10.0-beta.nupkg" -source https://api.nuget.org/v3/index.json


