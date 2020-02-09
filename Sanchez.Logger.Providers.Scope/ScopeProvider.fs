module Sanchez.Logger.Providers.Scope

open Sanchez.Logger.Abstraction

let Provider (scope: string) =
    let handler (level: LogLevel) =
        scope
        |> Message
        |> Some
        
    { ProviderInfo.name = "scope"; handler = handler }