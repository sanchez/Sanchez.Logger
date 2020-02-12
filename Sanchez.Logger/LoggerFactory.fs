module Sanchez.Logger.LoggerFactory

open Sanchez.Logger.Abstraction

let CreateFactory () =
    {
        ILoggerFactory.sinks = []
        providers = []
        filters = []
        childFactory = None
    }

let rec BuildLogger (factory: ILoggerFactory) (level: LogLevel) (message: string) =
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
    if (shouldLog || (List.isEmpty factory.filters)) then
        factory.sinks
        |> List.iter (fun x -> x providers message)
        
    match factory.childFactory with
    | Some f -> BuildLogger f level message
    | None -> ()
    
let AddSink (sink: LoggerCall) (factory: ILoggerFactory) =
    { factory with sinks = sink::factory.sinks }
    
let AddProvider (provider: ProviderInfo) (factory: ILoggerFactory) =
    { factory with providers = provider::factory.providers }
    
let AddFilter (filter: Filter) (factory: ILoggerFactory) =
    { factory with filters = filter::factory.filters }
    
let rec AddChild (childFactory: ILoggerFactory) (factory: ILoggerFactory) =
    match factory.childFactory with
    | Some f -> { factory with childFactory = (Some (AddChild childFactory f)) }
    | None -> { factory with childFactory = (Some childFactory) }