module Sanchez.Logger.Providers.Timestamp

open System
open Sanchez.Logger.Abstraction

let Provider (format: string option) =
    let handler (level: LogLevel) =
        DateTime.UtcNow
        |> (fun dt ->
            match format with
            | Some fmt -> dt.ToString(fmt)
            | None -> dt.ToString())
        |> Message
        |> Some
    
    { ProviderInfo.name = "timestamp"; handler = handler }