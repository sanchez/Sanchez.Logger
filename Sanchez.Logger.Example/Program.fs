open Sanchez.Logger
open Sanchez.Logger.Abstraction
open Sanchez.Logger.Sinks.File

let exampleChildFunc (logger: Logger) =
    logger Debug "hello there"

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
    sprintf "%d - %s" 5 "this is a critical error level" |> factory Critical
    
    0 // return an integer exit code
