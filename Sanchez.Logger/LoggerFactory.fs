module Sanchez.Logger.LoggerFactory

open Sanchez.Logger.Abstraction
open Sanchez.Logger.Abstraction

let CreateFactory () =
    { ILoggerFactory.sinks = [] }

let BuildLogger (factory: ILoggerFactory) =
    let log (level: LogLevel) (message: string) =
        factory.sinks
        |> List.iter (fun x -> x level message)
        
    let handleLog (level: LogLevel) =
        Printf.ksprintf (log level)
        
    handleLog