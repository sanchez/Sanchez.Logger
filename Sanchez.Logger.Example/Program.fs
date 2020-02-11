open Sanchez.Logger
open Sanchez.Logger.Abstraction
open Sanchez.Logger.Sinks.File

[<EntryPoint>]
let main argv =
    let fileConfiguration = { FileConfiguration.fileLocation = None }
    let file2Configuration = { FileConfiguration.fileLocation = "log.txt" |> Some }
    
    let factory =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddProvider (Providers.Scope.Provider "Example")
        |> LoggerFactory.AddProvider (Providers.Timestamp.Provider None)
        |> LoggerFactory.AddProvider (Providers.LogLevel.Provider)
        |> LoggerFactory.AddSink (Sinks.Console.Sink "$timestamp [$loglevel] ($scope) ")
        |> LoggerFactory.AddSink (Sinks.File.Sink fileConfiguration)
        |> LoggerFactory.AddSink (Sinks.File.Sink file2Configuration)
        |> LoggerFactory.BuildLogger
        
    factory Debug "Hello World"
    factory Info "This is an info level"
    factory Success "This is a success level"
    factory Warning "This is a warning level"
    factory Error "This is an error level"
    factory Critical "This is a critical error level"
    
    0 // return an integer exit code
