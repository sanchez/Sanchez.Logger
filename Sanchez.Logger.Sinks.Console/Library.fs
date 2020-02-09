module Sanchez.Logger.Sinks.Console

open Sanchez.Logger.Abstraction
open Sanchez.Logger.Abstraction

let Sink (level: LogLevel) (message: string) =
    let appendStatus (status: string) =
        sprintf "%s %s" status
        
    let status =
        match level with
        | Critical -> "Critical"
        | Error -> "Error"
        | Warning -> "Warning"
        | Success -> "Success"
        | Info -> "Info"
        | Debug -> "Debug"
        
    printfn "[%s] %s" status message
        
    ()