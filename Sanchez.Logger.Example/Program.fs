open Sanchez.Logger
open Sanchez.Logger.Abstraction
open Sanchez.Logger.Sinks.File

let exampleChildFunc (logger: Logger) =
    logger Debug "hello there"

[<EntryPoint>]
let main argv =
    let fileConfiguration = { FileConfiguration.fileLocation = None }
    let file2Configuration = { FileConfiguration.fileLocation = "log.txt" |> Some }
    let file3Configuration = { FileConfiguration.fileLocation = "critical.txt" |> Some }
    
    let specialFactory =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddProvider (Providers.LogLevel.Provider)
        |> LoggerFactory.AddSink (Sinks.File.Sink file3Configuration)
        |> LoggerFactory.AddFilter (Filters.LogLevel.Filter Warning)
    
    let factory =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddProvider (Providers.Scope.Provider "Example")
        |> LoggerFactory.AddProvider (Providers.Timestamp.Provider None)
        |> LoggerFactory.AddProvider (Providers.LogLevel.Provider)
        |> LoggerFactory.AddSink (Sinks.Console.Sink "$timestamp [$loglevel] ($scope) ")
        |> LoggerFactory.AddSink (Sinks.File.Sink fileConfiguration)
        |> LoggerFactory.AddSink (Sinks.File.Sink file2Configuration)
        
    let logger =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddChild specialFactory
        |> LoggerFactory.AddChild factory
        |> LoggerFactory.BuildLogger
        
    logger Debug "Hello World"
    logger Info "This is an info level"
    logger Success "This is a success level"
    logger Warning "This is a warning level"
    logger Error "This is an error level"
    sprintf "%d - %s" 5 "this is a critical error level" |> logger Critical
    
    0 // return an integer exit code
