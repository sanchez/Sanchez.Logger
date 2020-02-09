module Sanchez.Logger.LoggerFactory

open Sanchez.Logger.Abstraction
open Sanchez.Logger.Abstraction

let CreateFactory () =
    {
        ILoggerFactory.sinks = []
        providers = []
    }

let BuildLogger (factory: ILoggerFactory) =
    let log (level: LogLevel) (message: string) =
        factory.sinks
        |> List.iter (fun x -> x level message)
        
    let handleLog (level: LogLevel) =
        Printf.ksprintf (log level)
        
    handleLog
    
let AddSink (sink: LoggerCall) (factory: ILoggerFactory) =
    {
        sinks = sink::factory.sinks
        providers = factory.providers
    }
    
let AddProvider (provider: ProviderInfo) (factory: ILoggerFactory) =
    {
        sinks = factory.sinks
        providers = provider::factory.providers
    }