module Sanchez.Logger.Templates.Scoped

open Sanchez.Logger
open Sanchez.Logger.Sinks.File
open Sanchez.Logger.Abstraction

let SetupDefault = Standard.SetupDefault

let SetupWithScope (scope: string) =
    let coreProviders =
        LoggerFactory.CreateFactory()
        |> LoggerFactory.AddProvider (Providers.LogLevel.Provider)
        |> LoggerFactory.AddProvider (Providers.Timestamp.Provider None)
        |> LoggerFactory.AddProvider (Providers.Scope.Provider scope)
    
    let consoleFactory =
        coreProviders
        |> LoggerFactory.AddSink (Sinks.Console.Sink "$timestamp [$loglevel] ($scope) ")
        
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