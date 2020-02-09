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
        let providers =
            factory.providers
            |> List.map (fun provider -> (provider.name, provider.handler level))
            |> List.filter (snd >> Option.isSome)
            |> List.map (fun (name, value) -> (name, value |> Option.get))
            |> Map.ofList
            
        factory.sinks
        |> List.iter (fun x -> x providers message)
        
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