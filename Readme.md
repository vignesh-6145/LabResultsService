## useful commands
- To add migrations `dotnet ef migrations add InitialPatientsTable --project .\src\LabResultsService.Repository\ --startup-project .\src\LabResultsService.API\ -o Data/Migrations`
- To update migraitons to Db `dotnet ef database update --project .\src\LabResultsService.Repository\ --startup-project .\src\LabResultsService.API\`