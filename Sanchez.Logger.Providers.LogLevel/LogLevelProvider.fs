module Sanchez.Logger.Providers.LogLevel

open Sanchez.Logger.Abstraction

let Provider =
    let handler (level: LogLevel) =
        match level with
        | Critical -> "CRT" |> Red
        | Error -> "ERR" |> Red
        | Warning -> "WRN" |> Yellow
        | Success -> "SCS" |> Green
        | Info -> "INF" |> Blue
        | Debug -> "DBG" |> Faded
        |> Coloring
        |> Some
        
    { ProviderInfo.name = "loglevel"; handler = handler }