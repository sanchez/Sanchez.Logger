module Sanchez.Logger.Sinks.Console

open System
open Sanchez.Logger.Abstraction

let private renderColor (color: LogColouring) =
    match color with
    | Red s -> "\033[31m" + s
    | Green s -> "\033[32m" + s
    | Yellow s -> "\033[33m" + s
    | Blue s -> "\033[34m" + s
    | Magenta s -> "\033[35m" + s
    | Cyan s -> "\033[36m" + s
    
    | Faded s -> "\033[2m" + s
    | Reset -> "\033[0m"
        
let private render (symbol: LogSymbols) =
    match symbol with
    | Coloring c -> (renderColor c) + (renderColor Reset)
    | Message msg -> msg
    
let private convertKey (key: string) =
    "$" + key.ToLower()

let Sink (printFormat: string) (providers: ProviderMap) (message: string) =
    let renderedProviders =
        providers
        |> Map.map (fun _ -> render)
        
    let test =
        renderedProviders
        |> Map.fold (fun (acc: string) key provider -> acc.Replace(key |> convertKey, provider)) printFormat
        
    printfn "%s %s" test message
        
    ()