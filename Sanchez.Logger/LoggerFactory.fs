module Sanchez.Logger.LoggerFactory

open Sanchez.Logger.Abstraction
open Sanchez.Logger.Abstraction

let CreateFactory () =
    {
        ILoggerFactory.sinks = []
        providers = []
        filters = []
    }

let BuildLogger (factory: ILoggerFactory) (level: LogLevel) (message: string) =
    let providers =
        factory.providers
        |> List.map (fun provider -> (provider.name, provider.handler level))
        |> List.filter (snd >> Option.isSome)
        |> List.map (fun (name, value) -> (name, value |> Option.get))
        |> Map.ofList
        
    let shouldLog =
        factory.filters
        |> List.map (fun x -> x level)
        |> List.filter not
        |> List.isEmpty
        |> not
        
    if shouldLog then
        factory.sinks
        |> List.iter (fun x -> x providers message)
    
let AddSink (sink: LoggerCall) (factory: ILoggerFactory) =
    { factory with sinks = sink::factory.sinks }
    
let AddProvider (provider: ProviderInfo) (factory: ILoggerFactory) =
    { factory with providers = provider::factory.providers }
    
let AddFilter (filter: Filter) (factory: ILoggerFactory) =
    { factory with filters = filter::factory.filters }