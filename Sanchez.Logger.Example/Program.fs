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
        |> LoggerFactory.AddSink (Sinks.Console.Sink "$timestamp [$loglevel] ($scope)")
        |> LoggerFactory.AddSink (Sinks.File.Sink fileConfiguration)
        |> LoggerFactory.AddSink (Sinks.File.Sink file2Configuration)
        |> LoggerFactory.BuildLogger
        
    factory Debug "Hello World"
    
    printfn "Hello World from F#!"
    0 // return an integer exit code
