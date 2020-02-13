module Sanchez.Logger.Templates.Standard

open Sanchez.Logger
open Sanchez.Logger.Sinks.File
open Sanchez.Logger.Abstraction

let SetupDefault () =
    let coreProviders =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddProvider (Providers.LogLevel.Provider)
        |> LoggerFactory.AddProvider (Providers.Timestamp.Provider None)
        
    let consoleFactory =
        coreProviders
        |> LoggerFactory.AddSink (Sinks.Console.Sink "$timestamp [$loglevel] ")
        
    let fileConfiguration = { FileConfiguration.fileLocation = "log.txt" |> Some }
        
    let fileFactory =
        coreProviders
        |> LoggerFactory.AddFilter (Filters.LogLevel.Filter Warning)
        |> LoggerFactory.AddSink (Sinks.File.Sink fileConfiguration)
        
    let logger =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddChild consoleFactory
        |> LoggerFactory.AddChild fileFactory
        |> LoggerFactory.BuildLogger
    
    logger